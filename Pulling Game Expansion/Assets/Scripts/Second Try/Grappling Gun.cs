using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Scripts Ref:")]
    public GrappleRope grappleRope;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber1 = 9;
    [SerializeField] private int grappableLayerNumber2 = 8;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;
    public Transform reticlePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Object Ref:")]
    public GameObject m_reticle;
    public GameObject anchorReel;
    

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    public enum ReelType
    {
        Transform_Reel,
        Physics_Reel
    }
    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [Header("Reeling:")]
    [SerializeField] private bool reelToPlayer= false;
    [SerializeField] private ReelType reelType = ReelType.Physics_Reel;
    public float reelSpeed = 1;
    

    [Header("Button of Fire")]
    public KeyCode keyCode;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;
    [HideInInspector] public GameObject grappleVictim;
    [HideInInspector] public Transform victimPoint;
    [HideInInspector] bool keepReeling = true;
    private void Start()
    {

        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;

    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {            
            SetGrapplePoint();
            m_reticle.SetActive(false);
        }
        else if (Input.GetKey(keyCode))
        {           
            if (grappleRope.enabled)
            {
                RotateGun(grapplePoint, false);
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
              
                RotateGun(mousePos, true);
            }

            if (launchToPoint && grappleRope.isGrappling)
            {
                grapplePoint = anchorReel.transform.position;
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistnace;
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                }
            }

            if(reelToPlayer && grappleRope.isGrappling && keepReeling && grappleVictim.layer == 8)
            {
                grapplePoint = anchorReel.transform.position;
                if (reelType == ReelType.Transform_Reel)
                {
                    Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistnace;
                    victimPoint.position = Vector2.Lerp(victimPoint.position, targetPos, Time.deltaTime * reelSpeed);                    
                }
            }
        }
        else if (Input.GetKeyUp(keyCode))
        {
            grappleRope.enabled = false;           
            m_springJoint2D.connectedBody = null;
            if (launchToPoint) { m_springJoint2D.enabled = false; }   //This IF is added to prevent "losing your grip" when finish using Reeling tongue while grappled         
            m_rigidbody.gravityScale = 1;
            m_reticle.SetActive(true);
            keepReeling = true;
            if(anchorReel != null)
            {
                Destroy(anchorReel);
            }
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);
        }
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        //SetAimPoint();
    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            if (_hit.transform.gameObject.layer == grappableLayerNumber1|| _hit.transform.gameObject.layer == grappableLayerNumber2 || grappleToAll)
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                {
                    grapplePoint = _hit.point;
                    grappleVictim = _hit.transform.gameObject;
                    GameObject anchorGO = new GameObject("Anchor Game Object");
                    anchorGO.transform.SetParent(grappleVictim.transform);
                    anchorGO.transform.position = grapplePoint;
                    anchorReel = anchorGO;
                    grapplePoint = anchorGO.transform.position;
                    /*if(grappleVictim.layer == 8 || grappleVictim.layer == 9)
                    {
                        GameObject anchorGO = new GameObject("Anchor Game Object");
                        anchorGO.transform.SetParent(grappleVictim.transform);
                        anchorGO.transform.position = grapplePoint;
                        anchorReel = anchorGO;                       
                        grapplePoint = anchorGO.transform.position;
                    }   */
                    victimPoint = grappleVictim.transform;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                }
            }
        }
    }

    /*void SetAimPoint() //exactly the same as SetGrapplePoint but use at the end of RotateGun method to determine reticle position
    {
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            if (_hit.transform.gameObject.layer == grappableLayerNumber1 || _hit.transform.gameObject.layer == grappableLayerNumber2 || grappleToAll)
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                {
                    reticlePoint.position = _hit.point;              
                }
            }
        }
    }*/

    public void Grapple()
    {
        m_springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance && !reelToPlayer)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint && !reelToPlayer)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else if(!reelToPlayer)
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    m_springJoint2D.distance = distanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeed;
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.gravityScale = 0;
                    m_rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
        else
        {
            switch (reelType)
            {
                case ReelType.Physics_Reel:
                    
                    break;
                case ReelType.Transform_Reel:
                
                    break;
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        keepReeling = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }

}
