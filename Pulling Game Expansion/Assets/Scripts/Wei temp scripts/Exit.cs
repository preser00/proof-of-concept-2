using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Exit : MonoBehaviour
{
    public GameObject gmObject;
    private GameManager _gm; 
    public int leaveOrContinue; //leave = 0; continue = 1

    private bool touchingPlayer;

    private void Start()
    {
        gmObject = GameObject.FindGameObjectWithTag("GameManager"); 
        _gm = gmObject.GetComponent<GameManager>(); 
    }
    private void Update()
    {
        if (touchingPlayer)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                if (leaveOrContinue == 0) //leaves & ends game
                {
                    Debug.Log("leave");
                    SceneManager.LoadScene("MenuEnd");
                }
                else if (leaveOrContinue == 1)
                {
                    if(GameManager.timePassed >= 4) //automatically end game on fifth time attempting to go on
                    {
                        SceneManager.LoadScene("MenuEnd");
                    }
                    else
                    {
                        Debug.Log("continue");
                        _gm.CreateNewLevel();
                    }
                    
                }
                else
                {
                    Debug.Log("leaveOrContinue unassigned");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touchingPlayer = true;
        }

        /*move if spawned over tile - currently not working
       if (collision.gameObject.tag == "Tile")
        {
            Debug.Log("touching tile"); 
            Vector3 moveUp = Vector3.up * 1.1f;
            gameObject.transform.position += moveUp; 
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            touchingPlayer = false; 
        }
    }
}
