using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private GameObject pauseMenu;

    //Sensitivity Variables;
    [Header("Sensitivity Variables")]
    public Slider sensitivity_slider;
    private Button apply_btn;
    internal float sensitivity;
    private TextMeshProUGUI senstivity_output;
    public GameObject[] coins;

    private float timer = 0;

    public Image m_ReticleImage;

    public static GameManager instance = null;

    internal bool pauseMenuActive = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        //senstivity_output = GameObject.Find("Current Sensitivity").GetComponent<TextMeshProUGUI>();
        pauseMenu = GameObject.Find("PauseMenu");
        apply_btn = GameObject.Find("Apply").GetComponent<Button>();
        apply_btn.onClick.AddListener(() => ApplySetting());
    }

    private void Update()
    {
        if (HUDscoreTracker.instance.score < coins.Length)
        {
            timer += Time.unscaledDeltaTime;
            timeText.text = timer.ToString();
        }

        if (pauseMenuActive)
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            sensitivity = sensitivity_slider.value;
            if (sensitivity > 1 && sensitivity <= 4)
            {
                senstivity_output.text = "LOW";
            }
            else if (sensitivity > 4 && sensitivity <= 6)
                senstivity_output.text = "MEDIUM";
            else if (sensitivity > 6 && sensitivity <= 8)
                senstivity_output.text = "HIGH";
            else if (sensitivity > 8 && sensitivity <= 10)
                senstivity_output.text = "ULTRA HIGH";
        }
        else {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
        }
    }


    void ApplySetting() {
        pauseMenuActive = false;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

}
