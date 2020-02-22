using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    public GameObject[] coins;

    private float timer = 0;

    public Image m_ReticleImage;

    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }

    private void Update()
    {
        if (HUDscoreTracker.instance.score < coins.Length)
        {
            timer += Time.deltaTime;
            timeText.text = timer.ToString();
        }
    }


}
