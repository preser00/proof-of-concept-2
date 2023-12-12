using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

//https://www.youtube.com/playlist?list=PLBIb_auVtBwClCk31hwqH0yqrbnE8oI75

public class LevelGeneration : MonoBehaviour
{

    public List<GameObject> activeRooms;
    public List<Transform> activeRoomPositions;

    public GameObject[] roomTypes; //index 0 -> Empty, 1 -> N/A ATM

    public int currentRoomIndex;

    public float roomHeight; //assign in inspector 

    public LayerMask room;

    public bool doneGenerating; //unused, just to keep compile from yelling at me 

    public void Start()
    {

        ////get the position of the starting room & append it into activeroompos array 
        //GameObject startingRoom = GameObject.FindGameObjectWithTag("Spawner");
        //activeRooms.Add(startingRoom); 
        //activeRoomPositions.Add(startingRoom.transform); 

        Debug.Log(activeRoomPositions[0]);
        currentRoomIndex = 0; 

    }

    public void CreateNewRoom(string aboveOrBelow)
    {
        Debug.Log("create new room");

        int randRoomType = Random.Range(0, roomTypes.Length);

        Debug.Log("randroomtype = " + randRoomType);
        Debug.Log("roomtype count = " + roomTypes.Length);

        Debug.Log("active room position # = " + activeRoomPositions.Count); 


        if(aboveOrBelow == "below") // currently not using this, below room is manually placed into scene 
        {
            Debug.Log("create room below"); 
            GameObject newRoom = Instantiate(roomTypes[randRoomType], new Vector3(0, activeRoomPositions[0].position.y - roomHeight), Quaternion.identity);
            
            activeRooms.Add(newRoom);
            activeRoomPositions.Add(newRoom.transform);
        }
        else if(aboveOrBelow == "above")
        {
            Debug.Log("create room above");
            GameObject newRoom = Instantiate(roomTypes[randRoomType], new Vector3(0, activeRoomPositions[currentRoomIndex].position.y + roomHeight), Quaternion.identity);

            activeRooms.Add(newRoom); 
            activeRoomPositions.Add(newRoom.transform);

            currentRoomIndex = activeRoomPositions.Count - 1; //so next room uses correct y position

            if(currentRoomIndex >= 5)
            {
                Destroy(activeRooms[0]);
                activeRooms.Remove(activeRooms[0]);
                activeRoomPositions.Remove(activeRoomPositions[0]);

                currentRoomIndex = activeRoomPositions.Count - 1; 
            }
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
