using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathZone : MonoBehaviour
{
    private GameObject _player;
    private float _distanceToPlayer; 

    public float crawlSpeed = .2f;
    public float maxDistance = 20f; 

    private Vector3 _velocity = Vector3.zero; 

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player"); 

    }

    private void Update()
    {
        if (_player != null)
        {
            _distanceToPlayer = _player.transform.position.y - gameObject.transform.position.y; 

            if(_distanceToPlayer >= maxDistance && _player.GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                Vector3 desiredPosition = new Vector3(transform.position.x, _player.transform.position.y - 5, transform.position.z); 
                gameObject.transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, .125f); 
            }
        }
        transform.Translate(crawlSpeed * Vector3.up * Time.deltaTime, Space.World);
    }
}
