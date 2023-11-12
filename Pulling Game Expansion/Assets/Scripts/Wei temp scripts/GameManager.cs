using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CameraController _cam;
    public GameObject camDistraction;

    private AudioSource _audioSource;

    public GameObject[] roomPositions;

    //score display takes place in ui obj
    public static int collected = 0;  
    public static int timePassed = 0; 

    private void Start()
    {
        DontDestroyOnLoad(gameObject); 

        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        _audioSource = gameObject.GetComponent<AudioSource>();

        _audioSource.loop = true; 
        _audioSource.Play(); 
    }

    private void Update()
    {

        if(collected < 0)
        {
            collected = 0; 
        }
    }

    private void LateUpdate()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "MenuEnd")
        {
            Destroy(gameObject); 
        }
    }

    public void CreateNewLevel()
    {
        Debug.Log("create new level");
        /*_cam.transform.position = blackScreen.transform.position;
        _cam.ChangeTarget(blackScreen);*/

        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Spawner"); 

        for(var i=0; i < rooms.Length; i++)
        {Destroy(rooms[i]); }

        GameObject exit = GameObject.FindGameObjectWithTag("Exit");
        Destroy(exit);

        //put positions back
        for (var i = 0; i < roomPositions.Length; i++)
        {
            roomPositions[i].SetActive(true);
        }

        //REGENERATE ROOMS
        GameObject currentLevelGen = GameObject.FindGameObjectWithTag("LevelGenerator");
        currentLevelGen.SetActive(false);

        //camera transition from destroyed player to newly gen'd one
        _cam.ChangeTarget(camDistraction);
        GameObject[] currentPlayer = GameObject.FindGameObjectsWithTag("Player"); 
        for(var i=0; i<currentPlayer.Length; i++) { Destroy(currentPlayer[i]);  } 

        currentLevelGen.SetActive(true);

        timePassed++; 
    }
}
