using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private CameraController _cam;
    public GameObject camDistraction;

    private AudioSource _audioSource;

    public AudioSource deathSource;
    public GameObject deathSpeaker;

    private GameObject _player;
    

    //score display takes place in ui obj
    public static int distanceClimbed = 0;
    public static int highestDistanceClimbed = 0;
    public static int costsIncurred = 0;
    public static int superiorsDefeated = 0;

    public static int highScoreDistance = 0;
    public static int highScoreCosts = 0;
    public static int highScoreSuperiors = 0; 

    public GameObject[] roomPositions;

    public static bool gameOver = false;
    public static int thrashPoint = 0;
 

    private void Start()
    {
        DontDestroyOnLoad(gameObject); 

        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        _player = GameObject.FindGameObjectWithTag("Player");

        deathSpeaker = GameObject.Find("DeathSpeaker");
        deathSource = deathSpeaker.GetComponentInChildren<AudioSource>();       
        _audioSource.loop = true; 
        _audioSource.Play();
    }

    private void Update()
    {
        if(_player.transform.position.y > highestDistanceClimbed)
        {
            distanceClimbed = highestDistanceClimbed + (int)(_player.transform.position.y - highestDistanceClimbed);
            highestDistanceClimbed = (int)_player.transform.position.y;
        }
             

        if (gameOver)
        {
            if(highestDistanceClimbed > highScoreDistance) { highScoreDistance = highestDistanceClimbed; }
            if(costsIncurred > highScoreCosts) { highScoreCosts = costsIncurred; }
            if(superiorsDefeated > highScoreSuperiors) { highScoreSuperiors = superiorsDefeated; }

            SceneManager.LoadScene("GameOver");
            deathSource.Play();
            _audioSource.Stop();

            //if (Input.GetKey(KeyCode.R))
            //{
            //    Debug.Log("R pressed on gameover screen"); 
            //    SceneManager.LoadScene("JoinTest");
            //}
        }
    }

    public void CreateNewLevel()
    {
        Debug.Log("create new level");
        /*_cam.transform.position = blackScreen.transform.position;
        _cam.ChangeTarget(blackScreen);*/

        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Spawner");

        for (var i = 0; i < rooms.Length; i++)
        { Destroy(rooms[i]); }

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
        for (var i = 0; i < currentPlayer.Length; i++) { Destroy(currentPlayer[i]); }

        currentLevelGen.SetActive(true);
    }
}
