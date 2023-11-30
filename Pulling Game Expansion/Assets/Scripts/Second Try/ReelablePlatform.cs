using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelablePlatform : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public GrapplingGun grappleGun;
    public GrappleRope grappleRope;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Being Reeled:")]
    [SerializeField] private bool beingReeled = false;
    public float ReelingCoefficient =1f;
    // Start is called before the first frame update
    void Start()
    {
        m_springJoint2D.enabled = false;
        grappleGun = GameObject.Find("ReelingTongue").GetComponent<GrapplingGun>();
        grappleRope = GameObject.Find("ReelingTongue").GetComponentInChildren<GrappleRope>();
    }

    // Update is called once per frame
    private void Update()
    {
    if(grappleGun.grappleVictim == gameObject && grappleRope.isGrappling)
        {
            m_springJoint2D.connectedAnchor = grappleGun.firePoint.position;

            Vector2 distanceVector = grappleGun.firePoint.position - grappleGun.gunHolder.position;

            m_springJoint2D.distance = distanceVector.magnitude;
            m_springJoint2D.frequency = grappleGun.reelSpeed / ReelingCoefficient;
            m_springJoint2D.enabled = true;
            beingReeled = true;
        }
        else
        {
            beingReeled = false;
            m_springJoint2D.enabled = false;
        }
        if (!beingReeled)
        {
            Debug.Log(m_rigidbody.velocity);
          if(m_rigidbody.velocity != new Vector2(0, 0)) 
          {
                m_rigidbody.velocity *= 0.99f;
          }          
        }
    }    
}
