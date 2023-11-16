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

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        waitCounter = waitTime;
        walkCounter = walkTime;
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
        moveSpeed = Random.Range(2, 4);
        WalkDirection = Random.Range(0, 2);
        isWalking = true;
        walkCounter = walkTime;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Boundary")
        {
            moveSpeed *= -1;
            walkCounter = walkTime/2;
        }
    }
}
