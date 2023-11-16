using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/playlist?list=PLBIb_auVtBwClCk31hwqH0yqrbnE8oI75

public class LevelGeneration : MonoBehaviour
{

    public List<Transform> activeRoomPositions;
    public GameObject[] rooms; //index 0 -> Empty, 1 -> N/A ATM

    public LayerMask room;

    public bool doneGenerating; //unused, just to keep compile from yelling at me 

    public void Start()
    {
        //get the position of the starting room & append it into activeroompos array 
        GameObject startingRoom = GameObject.FindGameObjectWithTag("Spawner");
        activeRoomPositions.Add(startingRoom.transform);

        //spawn an empty room below just so player doesn't fall into a void 
        CreateNewRoom("below"); 

    }

    public void CreateNewRoom(string aboveOrBelow)
    {
        Debug.Log("create new room");

        int roomType = Random.Range(0, activeRoomPositions.Count);

        if(aboveOrBelow == "below") // the only case where i create a room below is at the beginning, so i just use aRP list directly
        {
            Debug.Log("create room below"); 
            Instantiate(rooms[roomType], new Vector3(0, activeRoomPositions[0].position.y - 17), Quaternion.identity);
            //rooms[0].GetComponent<Renderer>().bounds.size.y, 0
        }
        else if(aboveOrBelow == "above")
        {
            Debug.Log("create room above"); 
        }
        else
        {
            Debug.Log("room creation failed"); 
        }
    }

    //function setCards_Row()
    //{
    //    var nextPosition = 0;
    //    var gap = 0.5;
    //    for (var i = 0; i < num_of_Cards; i++)
    //    {
    //        Instantiate(prefab, Vector3(nextPosition, 0, 0), Quaternion.identity);
    //        nextPosition += (prefab.renderer.bounds.size.x + gap);
    //    }
    //}

    // this function is for generating a 'correct' path, we don't need it 
    //private void Move()
    //{
    //    if (direction == 1 || direction == 2) 
    //    {
    //        if(transform.position.x < maxX) //moving RIGHT confirmed
    //        {
    //            downCounter = 0; 
    //            Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
    //            transform.position = newPos;

    //            int rand = Random.Range(0, rooms.Length);
    //            Instantiate(rooms[rand], transform.position, Quaternion.identity); //any room is fine bc all rooms have l/r openings

    //            //ANTI BACKTRACKING 
    //            direction = Random.Range(1, 6);
    //            if (direction == 3)
    //            {
    //                direction = 2; 
    //            }
    //            else if(direction == 4)
    //            {
    //                direction = 5; 
    //            }
    //        }
    //        else //else move DOWN 
    //        {
    //            direction = 5;
    //        }

    //    }
    //    else if(direction == 3 || direction == 4) 
    //    {
    //        if(transform.position.x > minX) //moving LEFT confirmed
    //        {
    //            downCounter = 0; 

    //            Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
    //            transform.position = newPos;

    //            int rand = Random.Range(0, rooms.Length);
    //            Instantiate(rooms[rand], transform.position, Quaternion.identity);

    //            direction = Random.Range(3, 6); //ANTI BACKTRACKING
    //        }
    //        else //else move DOWN 
    //        {
    //            direction = 5; 
    //        }

    //    }
    //    else if(direction == 5) 
    //    {
    //        downCounter++; 

    //        if(transform.position.y > minY) //moving DOWN confirmed
    //        {
    //            //make sure prev room has DOWN opening
    //            Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
    //            if(roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
    //            {
    //                //so a room w/ top opening doesn't get destroyed 
    //                if(downCounter >= 2)
    //                {
    //                    roomDetection.GetComponent<RoomType>().RoomDestruction(); 
    //                    Instantiate(rooms[3], transform.position, Quaternion.identity); 
    //                }
    //                else
    //                {
    //                    roomDetection.GetComponent<RoomType>().RoomDestruction();

    //                    int randBottomRoom = Random.Range(1, 4);
    //                    if (randBottomRoom == 2)
    //                    {
    //                        randBottomRoom = 1;
    //                    }
    //                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);

    //                }
    //            }

    //            Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
    //            transform.position = newPos;

    //            int rand = Random.Range(2, 4); //only rooms w/ top openings
    //            Instantiate(rooms[rand], transform.position, Quaternion.identity);

    //            direction = Random.Range(1, 6);

    //        }
    //        else
    //        {
    //            // STOP LEVEL GENERATION & SPAWN PLAYER/EXIT
    //            doneGenerating = true;

    //            Vector3 playerOffset = new Vector3(-4, 0, 0);
    //            Instantiate(player, startingPositions[0].position + playerOffset, Quaternion.identity);

    //            Instantiate(exit, gameObject.transform.position + Vector3.forward*5, Quaternion.identity); 
    //        }
    //    }

    //}
}
