using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Vector3 respawnPoint;
    private bool showDeathText = false;
    private string causeOfDeathMessage = "Nothing";
    private float deathMsgTimer = 5.0f;

    public void playerRespawn(string causeOfDeath){
        //currently resets player to one standard spawn point
        Transform playerTransform = GetComponentInParent<Transform>();
        playerTransform.position = respawnPoint;
        causeOfDeathMessage = "Player died from " + causeOfDeath;
        showDeathText = true;
    }
    void OnGUI()
     {
         //displays death message for 5 seconds in upper left of screen
         if(showDeathText){
            GUI.color = Color.red;
            GUI.Label(new Rect(150, 50, 200, 100), causeOfDeathMessage);
            deathMsgTimer -= Time.deltaTime;
            if (deathMsgTimer < 0){
                deathMsgTimer = 5.0f;
                showDeathText = false;
            }
         }
            
     }
}
