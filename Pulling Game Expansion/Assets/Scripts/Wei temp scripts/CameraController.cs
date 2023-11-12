using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player; 

    private float lookDownTimer = .5f;
    public float lookDownRange;

    public Transform target; //positional data of whatever you want camera to focus on 
    public float smoothSpeed = 0.125f; //this camera has a bit of lag as it follows player, gives impression of mvmt
    public Vector3 offset; //cam uses z-axis to zoom

    private Vector3 _velocity = Vector3.zero; //set to 0,0,0
    private PlayerController _player; //gets the player controller script we made 

    public float limitLeft, limitRight, limitBottom, limitTop;

    // Update is called once per frame
    void Update()
    {
       if (target == null || transform.gameObject.tag != "Player")
            {
                player = GameObject.FindGameObjectWithTag("Player"); 
                target = player.transform; //have to tag player obj with 'player' tag for this to work 

        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            lookDownTimer = .5f;
            offset = new Vector3(0, 0, -100);  
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (lookDownTimer <= 0)
            {
                offset = new Vector3(0, -lookDownRange, -100); 
            }
            else
            {
                lookDownTimer -= Time.deltaTime; 
            }
        }
    }

    //lateupdate runs after update - good for, ex: things you don't want affected by physics
    private void LateUpdate()
    {
        Transform currentTarget = target;
        Vector3 desiredPosition = currentTarget.position + offset;

        //^ lerp (sd alternative) doesn't like dealing w moving objects, it jitters if cam follows smth moving fast
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, smoothSpeed); //ref = "use velocity"  

        //boundary check - clamp moves any exceeding values back to the set limits
        transform.position = new Vector3(Mathf.Clamp(smoothedPosition.x, limitLeft, limitRight), Mathf.Clamp(smoothedPosition.y, limitBottom, limitTop), smoothedPosition.z); 

    }

    public void ChangeTarget(GameObject newTarget)
    {
        target = newTarget.transform; 
    }

}
