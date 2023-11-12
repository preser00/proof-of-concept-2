using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;

    private void Start()
    {
        int rand = Random.Range(0, objects.Length);

        //stores the go we instantiate into an instance var 
        GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
        
        //set instance to be a child of the room gameobject that spawned it
        instance.transform.parent = transform; 
    }
}
