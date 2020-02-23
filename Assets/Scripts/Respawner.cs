using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Respawner : MonoBehaviour
{
    public Vector3 respawnPoint;
    public  TextMeshProUGUI deathCounterText;
    public TextMeshProUGUI deathMessageText;
    private string causeOfDeathMessage = "Nothing";
    private float deathMsgTimer = 2.0f;

	private GUIStyle guiFontStyle = new GUIStyle();
	private int deathCount = 0;

	void Start()
	{
		guiFontStyle.fontSize = 32;
        guiFontStyle.normal.textColor = Color.red;
	}
    public void playerRespawn(string causeOfDeath){
        //currently resets player to one standard spawn point
        Transform playerTransform = GetComponentInParent<Transform>();
        playerTransform.position = respawnPoint;
        causeOfDeathMessage = "Player died from " + causeOfDeath;
        deathCount++;
        deathCounterText.text = deathCount.ToString();
        StartCoroutine("DeathPrompt");
    }
    void OnGUI()
     {
		 //GUI.Label(new Rect(150, 35, 200, 100), "Death Count: " + deathCount.ToString(), guiFontStyle);
   //      //displays death message for 5 seconds in upper left of screen
   //      if(showDeathText){
   //         GUI.Label(new Rect(150, 65, 200, 100), causeOfDeathMessage, guiFontStyle);
   //         deathMsgTimer -= Time.deltaTime;
   //         if (deathMsgTimer < 0){
   //             deathMsgTimer = 5.0f;
   //             showDeathText = false;
   //         }
   //      }
            
     }


    IEnumerator DeathPrompt() {
        GameObject deathScreen = GameObject.Find("DeathScreen");
        deathMessageText.text = causeOfDeathMessage;

        while (deathScreen.GetComponent<CanvasGroup>().alpha < 1) {
            Time.timeScale = 0.0f;
            deathScreen.GetComponent<CanvasGroup>().alpha += Time.unscaledDeltaTime * 0.01f;
        }
        yield return new WaitForSecondsRealtime(deathMsgTimer);
        deathScreen.GetComponent<CanvasGroup>().alpha = 0.0f;
        Time.timeScale = 1.0f;
    }
}
