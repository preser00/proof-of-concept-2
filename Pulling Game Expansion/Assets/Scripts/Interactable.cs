using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Interactable : MonoBehaviour
{
    public int minCost;
    public int maxCost;

    float locationTrackingTimer;
    float locationTrackingSeconds;
    //AudioSource audioSource;
    public AudioClip clip;

    [SerializeField]
    float trackingCooldown = 1.5f; 

    Vector3 lastRecordedPosition; 
    bool isUntouched = false;

    private void Start()
    {
        lastRecordedPosition = transform.position;
        //audioSource = gameObject.GetComponent<AudioSource>();
        //clip = gameObject.GetComponent<AudioClip>();
    }

    private void Update()
    {
        locationTrackingTimer += Time.deltaTime;

        locationTrackingSeconds = locationTrackingTimer % 60; 

        if(locationTrackingSeconds >= trackingCooldown)
        {
            
            if(transform.position == lastRecordedPosition)
            {
                isUntouched = true; 
            }
            else if(transform.position != lastRecordedPosition)
            {
                isUntouched = false; 
            }

            lastRecordedPosition = transform.position;
            locationTrackingTimer = 0;
            locationTrackingSeconds = 0;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Respawn")
        {

            if(isUntouched == false) //give points if the obj has been moving, which should only happen if player touched it 
            {
                int cost = Random.Range(minCost, maxCost);
                GameManager.costsIncurred += cost;
                AudioSource.PlayClipAtPoint(clip, gameObject.transform.position);
               
            }

            Destroy(this.gameObject); 

        }
    }
}
