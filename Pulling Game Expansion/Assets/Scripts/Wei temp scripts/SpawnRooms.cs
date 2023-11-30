using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    //see if a room is nearby, if not, spawn one in to fill the space

    public LayerMask whatIsRoom;
    public LevelGeneration levelGen; 

    private void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
       
        if(roomDetection == null && levelGen.doneGenerating == true) //spawn random room 
        {
            int rand = Random.Range(0, levelGen.roomTypes.Length);
            Instantiate(levelGen.roomTypes[rand], transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        else if (levelGen.doneGenerating == true)
        {
            gameObject.SetActive(false); 
        }
    }
}
