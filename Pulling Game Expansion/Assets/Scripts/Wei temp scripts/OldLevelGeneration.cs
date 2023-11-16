using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/playlist?list=PLBIb_auVtBwClCk31hwqH0yqrbnE8oI75

public class LevelGenerationOld : MonoBehaviour
{
    public GameObject player;
    public GameObject exit; 

    public Transform[] startingPositions;
    public GameObject[] rooms; //index 0 -> LR, index 1 -> LRB, index 2 -> LRT, index 3 -> LRTB

    private int direction;
    public float moveAmount;

    private float timeBtwnRoom;
    public float startTimeBtwnRoom = 0.25f;

    //type these in unity 
    public float minX;
    public float maxX;
    public float minY;
    public bool doneGenerating; //let spawnrooms script access it

    public LayerMask room;

    private int downCounter; //for edge case of moving down consecutively

    private void OnEnable()
    {
        doneGenerating = false; 

        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;

        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6); 
    }

    private void Update()
    {
        if(timeBtwnRoom <= 0 && doneGenerating == false)
        {
            Move();
            timeBtwnRoom = startTimeBtwnRoom;
        }
        else
        {
            timeBtwnRoom -= Time.deltaTime; 
        }
    }

    // this function is for generating a 'correct' path, we don't need it 
    private void Move()
    {
        if (direction == 1 || direction == 2) 
        {
            if(transform.position.x < maxX) //moving RIGHT confirmed
            {
                downCounter = 0; 
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity); //any room is fine bc all rooms have l/r openings

                //ANTI BACKTRACKING 
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2; 
                }
                else if(direction == 4)
                {
                    direction = 5; 
                }
            }
            else //else move DOWN 
            {
                direction = 5;
            }
            
        }
        else if(direction == 3 || direction == 4) 
        {
            if(transform.position.x > minX) //moving LEFT confirmed
            {
                downCounter = 0; 

                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6); //ANTI BACKTRACKING
            }
            else //else move DOWN 
            {
                direction = 5; 
            }
            
        }
        else if(direction == 5) 
        {
            downCounter++; 

            if(transform.position.y > minY) //moving DOWN confirmed
            {
                //make sure prev room has DOWN opening
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if(roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    //so a room w/ top opening doesn't get destroyed 
                    if(downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction(); 
                        Instantiate(rooms[3], transform.position, Quaternion.identity); 
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);

                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4); //only rooms w/ top openings
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);

            }
            else
            {
                // STOP LEVEL GENERATION & SPAWN PLAYER/EXIT
                doneGenerating = true;

                Vector3 playerOffset = new Vector3(-4, 0, 0);
                Instantiate(player, startingPositions[0].position + playerOffset, Quaternion.identity);

                Instantiate(exit, gameObject.transform.position + Vector3.forward*5, Quaternion.identity); 
            }
        }
        
    }
}
