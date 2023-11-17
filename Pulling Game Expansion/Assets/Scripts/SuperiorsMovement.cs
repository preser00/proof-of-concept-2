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

    private float leftBound;
    private float rightBound;
    public float boundNumber;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        waitCounter = waitTime;
        walkCounter = walkTime;
        leftBound = this.transform.position.x - boundNumber;
        rightBound = this.transform.position.x + boundNumber;
    }

    // Update is called once per frame
    void Update()
    {
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
}
