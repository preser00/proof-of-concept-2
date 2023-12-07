using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public int minCost;
    public int maxCost; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Respawn")
        {

            int cost = Random.Range(minCost, maxCost);
            GameManager.costsIncurred += cost;

            Destroy(this.gameObject); 

        }
    }
}
