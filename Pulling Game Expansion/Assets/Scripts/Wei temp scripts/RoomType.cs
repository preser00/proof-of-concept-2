using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;

    private bool colliderDisabled = false; 

    private void Update()
    {
        if(GameObject.Find("Player") != null && colliderDisabled == false)
        {
            gameObject.GetComponent<Collider2D>().enabled = false; 
            colliderDisabled = true; 
        }
    }

    public void RoomDestruction()
    {
        Destroy(gameObject); 
    }
}
