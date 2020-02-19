using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    public GameObject[] coins;

    private float timer = 0;

    private void Update()
    {
        if (HUDscoreTracker.instance.score < coins.Length)
        {
            timer += Time.deltaTime;
            timeText.text = timer.ToString();
        }
    }


}
