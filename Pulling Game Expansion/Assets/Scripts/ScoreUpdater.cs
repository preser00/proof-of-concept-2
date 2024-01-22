using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ScoreUpdater : MonoBehaviour
{
    private TextMeshProUGUI textmesh;

    public string whichUpdater; //general, your, or high 

    private void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>(); 

    }

    private void Update()
    {
        if(whichUpdater == "general")
        {
            textmesh.text = GameManager.distanceClimbed.ToString() + " ft closer to VENGEANCE \n$" + GameManager.costsIncurred + " in damages wreaked to ELDrCO\n" + GameManager.superiorsDefeated + " superiors dispatched";
        }
        else if(whichUpdater == "your")
        {
            textmesh.text = "Your Scores: \nDistance Climbed: " + GameManager.highestDistanceClimbed.ToString() + "\nDamages wreaked: $" + GameManager.costsIncurred.ToString() + "\nSuperiors defeated: " + GameManager.superiorsDefeated.ToString(); 
        }
        else if(whichUpdater == "high")
        {
            textmesh.text = "Highest Scores: \nDistance Climbed: " + GameManager.highScoreDistance.ToString() + "\nDamages wreaked: $" + GameManager.highScoreCosts.ToString() + "\nSuperiors defeated: " + GameManager.highScoreSuperiors.ToString();
        }
        else
        {
            print("no updater found"); 
        }
       
    }
}
