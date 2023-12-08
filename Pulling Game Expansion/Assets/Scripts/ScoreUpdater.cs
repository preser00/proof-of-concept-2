using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ScoreUpdater : MonoBehaviour
{
    private TextMeshProUGUI textmesh;

    private void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>(); 

    }

    private void Update()
    {
        textmesh.text = GameManager.distanceClimbed.ToString() + " ft closer to VENGEANCE \n$" + GameManager.costsIncurred + " in damages wreaked to ELDrCO\n" + GameManager.superiorsDefeated + " superiors dispatched"; 
    }
}
