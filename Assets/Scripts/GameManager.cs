using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isTutorial = true;
    public TextMeshProUGUI timeText;

    private GameObject pauseMenu;

    public TextMeshProUGUI bestTime;

    //Sensitivity Variables;
    [Header("Sensitivity Variables")]
    public Slider sensitivity_slider;
    private Button apply_btn;
    internal float sensitivity;
    private TextMeshProUGUI senstivity_output;
    public GameObject[] coins;

    private float timer = 0;

    private Button reload_btn;

    public Image m_ReticleImage;

    public static GameManager instance = null;




    internal bool pauseMenuActive = false;
    internal bool reloadLevel = false;
    internal Image grapple_availableImage;


    //Controlling actions for tutorial.
    internal bool canLookAround = true;
    internal bool canMove = false;
    internal bool canJump = false;
    internal bool canRun = false;
    internal bool canGrapple = false;
    internal bool canShoot = false;
    internal bool countTimer = true;

    private AudioSource gameAudioSource;

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
        timeText.text = ("000.00");
        countTimer = false;
        senstivity_output = GameObject.Find("Current Sensitivity").GetComponent<TextMeshProUGUI>();
        reload_btn = GameObject.Find("ReloadLevel").GetComponent<Button>();
        apply_btn = GameObject.Find("Apply").GetComponent<Button>();
        pauseMenu = GameObject.Find("PauseMenu");
        grapple_availableImage = GameObject.Find("GrappleAvailable").GetComponent<Image>();
        apply_btn.onClick.AddListener(() => ApplySetting());
        reload_btn.onClick.AddListener(() => ReloadLevel());
        bestTime.text = "Time to beat : 30.00";
        gameAudioSource = AudioManager.instance.gameObject.GetComponent<AudioSource>();
        if(isTutorial)
            gameAudioSource.PlayOneShot(AudioManager.instance.introClip);
    }

    private void Update()
    {
        if (gameAudioSource.isPlaying) {
            canMove = false;
            countTimer = false;
        }
        else{
            canMove = true;
            countTimer = true;
        }
        if (HUDscoreTracker.instance.score < coins.Length && countTimer) 
        {
            timer += Time.unscaledDeltaTime;
            timeText.text = timer.ToString("000.00");
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

    void ReloadLevel() {
        reloadLevel = true;
        SceneManager.LoadScene(1);
    }

    void ApplySetting() {
        pauseMenuActive = false;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

}
