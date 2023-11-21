using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoundary : MonoBehaviour
{
    public LevelGeneration levelGen;

    private GameObject _player;
    public bool _roomSpawned;

    private void Start()
    {
        _roomSpawned = false; 
        _player = GameObject.FindGameObjectWithTag("Player");

        levelGen = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGeneration>(); 
    }

    private void Update()
    {
        if (_roomSpawned == false){

            if(_player.transform.position.y > gameObject.transform.position.y)
            {
                levelGen.CreateNewRoom("above");

                _roomSpawned = true;
            }

        }
    }
}
