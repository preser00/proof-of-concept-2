using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("R pressed on gameover screen");

            GameManager.gameOver = false;
            GameManager.distanceClimbed = 0; 
            GameManager.highestDistanceClimbed = 0;

            GameManager.costsIncurred = 0;
            GameManager.superiorsDefeated = 0; 

            SceneManager.LoadScene("Wei");
        }
    }
}
