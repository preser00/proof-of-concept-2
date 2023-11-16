using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class DeathUpdater : MonoBehaviour
{
    private TextMeshProUGUI textmesh;

    private void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>(); 

    }

    private void Update()
    {
        textmesh.text = "You fell..."; 
    }
}
