using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpeaker : MonoBehaviour
{
    public AudioSource audioSource;
    public bool over = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            over = true;
        }
    }
}
