using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //This script is created to communicate witht the animator by passing through the conditions in other scripts of child objects, might take on other work later 

    public GrapplingGun tongueGrab;
    public GrapplingGun tongueReel;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(tongueGrab.grappleRope.enabled || tongueReel.grappleRope.enabled)
        {
            animator.SetBool("OpenMouth", true);
        }
        if (!tongueGrab.grappleRope.enabled && !tongueReel.grappleRope.enabled)
        {
            animator.SetBool("OpenMouth", false);
        }      
    }
}
