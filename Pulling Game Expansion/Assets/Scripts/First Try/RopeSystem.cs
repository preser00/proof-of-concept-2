using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RopeSystem : MonoBehaviour
{
    #region Variable Declaration
    public GameObject ropeHingeAnchor;
    public DistanceJoint2D ropeJoint;
    public Transform crosshair;
    public SpriteRenderer crosshairSprite;
    public PlayerMovement playerMovement;

    private bool _ropeAttached;
    private Vector2 _playerPosition;
    private Rigidbody2D _ropeHingeAnchorRb;
    private SpriteRenderer _ropeHingeAnchorSprite;

    #region Rope Draw Variable Declaration
    public LineRenderer ropeRenderer;
    public LayerMask ropeLayerMask; //allows interaction with only desired physics layer
    private float ropeMaxCastDistance = 20f;
    private List<Vector2> ropePositions = new List<Vector2>();
    #endregion
    private bool distanceSet;
    #endregion
    void Awake()
    {
        //Assigning variables with their proper target
        ropeJoint.enabled = false;
        _playerPosition = transform.position; //set it to current position of the player
        _ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        _ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)); //Important: capture where the mouse cursor is in the world space
        var facingDirection = worldMousePosition - transform.position; //determines the "facing" direction
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x); //get the angle of aiming from the facing direction
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

        _playerPosition = transform.position; //update player position

        if (!_ropeAttached)
        {
            SetCrosshairPosition(aimAngle); //if no rope is Attached, show the reticle
        }
        else
        {
            crosshairSprite.enabled = false; //if rope is Attached, disable the reticle
        }

        HandleInput(aimDirection);
        UpdateRopePositions();
    }

    private void SetCrosshairPosition(float aimAngle) //a method used to display where the player's aiming
    {
        if (!crosshairSprite.enabled)
        {
            crosshairSprite.enabled = true;
        }

        var x = transform.position.x + 2f * Mathf.Cos(aimAngle); //the Xposition of reticle is the position of that of the aimAngle
        var y = transform.position.y + 2f * Mathf.Sin(aimAngle); //the Yposition of reticle is the position of that of the aimAngle

        var crossHairPosition = new Vector3(x, y, 0); //set destination for the reticle 
        crosshair.transform.position = crossHairPosition; //move reticle to destination
    }

    private void HandleInput(Vector2 aimDirection) //a method used to register mouse input and fire a rope!
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse Press");
            if (_ropeAttached) return;
            ropeRenderer.enabled = true; // Start rendering the rope

            var hit = Physics2D.Raycast(_playerPosition, aimDirection, ropeMaxCastDistance, ropeLayerMask); //cast a line from player to aimed direction

            if (hit.collider != null) //if it hit something
            {
                _ropeAttached = true; //then rope is attached
                if (!ropePositions.Contains(hit.point)) //check if rope already attached
                {
                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse); //a small force to hop off the ground
                    ropePositions.Add(hit.point); //remember the position of point attached

                    ropeJoint.distance = Vector2.Distance(_playerPosition, hit.point); //set the distantJoint distance as the player's position to the attached point
                    ropeJoint.enabled = true; //start the connection!
                    _ropeHingeAnchorSprite.enabled = true; //draw the tip of the tongue
                }
            }
            else
            {
                ropeRenderer.enabled = false;
                _ropeAttached = false;
                ropeJoint.enabled = false;
            }
        }

        if (Input.GetMouseButton(1))
        {
            ResetRope();
        }
    }

    private void ResetRope() //a method used to reset all rope things back to default
    {
        ropeJoint.enabled = false;
        _ropeAttached = false;
        playerMovement.isSwinging = false;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.position);
        ropePositions.Clear();
        _ropeHingeAnchorSprite.enabled = false;
    }

    private void UpdateRopePositions()
    {
        // 1
        if (!_ropeAttached)
        {
            return;
        }

        // 2
        ropeRenderer.positionCount = ropePositions.Count + 1;

        // 3
        for (var i = ropeRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != ropeRenderer.positionCount - 1) // if not the Last point of line renderer
            {
                ropeRenderer.SetPosition(i, ropePositions[i]);

                // 4
                if (i == ropePositions.Count - 1 || ropePositions.Count == 1)
                {
                    var ropePosition = ropePositions[ropePositions.Count - 1];
                    if (ropePositions.Count == 1)
                    {
                        _ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                    else
                    {
                        _ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                }
                // 5
                else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                {
                    var ropePosition = ropePositions.Last();
                    _ropeHingeAnchorRb.transform.position = ropePosition;
                    if (!distanceSet)
                    {
                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        distanceSet = true;
                    }
                }
            }
            else
            {
                // 6
                ropeRenderer.SetPosition(i, transform.position);
            }
        }
    }
}
