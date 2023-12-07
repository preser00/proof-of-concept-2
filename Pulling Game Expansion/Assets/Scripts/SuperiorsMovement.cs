using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SuperiorsMovement : MonoBehaviour
{

    public float moveSpeed;

    private bool isWalking;

    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;

    private int WalkDirection;

    private Rigidbody2D rb;
    private ReelablePlatform m_reelProperties;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Collider2D m_collider;
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_deathClip;

    private float leftBound;
    private float rightBound;
    public float boundNumber;

    private bool dead = false;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_reelProperties = gameObject.GetComponent<ReelablePlatform>();
        waitCounter = waitTime;
        walkCounter = walkTime;
        leftBound = this.transform.position.x - boundNumber;
        rightBound = this.transform.position.x + boundNumber;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.gameOver)
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = Random.Range(1, 3);
        }

        //if the NPC is walking
        if (isWalking)
            {
                //walk counter begins to count down
                walkCounter -= Time.deltaTime;

                //moves left or right
                switch (WalkDirection)
                {
                    case 0:
                        rb.velocity = new Vector2(-moveSpeed, 0);
                        break;
                    case 1:
                        rb.velocity = new Vector2(moveSpeed, 0);
                        break;
                    case 3:
                        break;
                }
                
                //if the superior reaches it's bound, turn around and continue walking, but for half the time
            if(transform.position.x <= leftBound)
            {
                WalkDirection = 1;
            }
            if (transform.position.x >= rightBound)
            {
                WalkDirection = 0;
            }
            if (m_reelProperties.beingReeled)
            {
                WalkDirection = 3;
            }


            //when walk counter hits 0, NPC stops walking and wait counter is set
            if (walkCounter < 0)
                {
                    isWalking = false;
                    waitCounter = waitTime;
                }

            }
            else //if the NPC is not walking, speed becomes 0 and wait timer begins to count down
            {
                waitCounter -= Time.deltaTime;
                rb.velocity = Vector2.zero;

                //when wait timer hits 0, NPC moves again - random speed and random direction
                if (waitCounter < 0)
                {
                    ChooseDirection();
                }

            }
    }

    public void ChooseDirection()
    {
        moveSpeed = Random.Range(1, 3);
        WalkDirection = Random.Range(0, 2);
        isWalking = true;
        walkCounter = walkTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Superior" || collision.gameObject.tag == "Respawn")
        {
            GameManager.superiorsDefeated += 1;
            dead = true;
            m_spriteRenderer.enabled = false;
            m_collider.enabled = false;
            
            m_audioSource.clip = m_deathClip;
            m_audioSource.loop = false;
            Debug.Log("AudioClip Loaded!");
            m_audioSource.Play();

            if(dead && !m_audioSource.isPlaying)
            {
                Destroy(gameObject);
            }           
        }
    }

}
