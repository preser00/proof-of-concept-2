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
    public AudioSource audioSource;
    public GameObject deathSpeaker;
    private bool dead = false;
    private bool speakerSpawned = false;
    public float timer = 0f;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (dead && !speakerSpawned)
        {
            Instantiate<GameObject>(deathSpeaker);
            Destroy(gameObject);
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Superior" || collision.gameObject.tag == "Respawn")
        {           
            dead = true;
            GameManager.gameOver = true;
        }
    }
}
