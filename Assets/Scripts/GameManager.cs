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

    public AudioSource m_Music;

    private GameObject pauseMenu;

    private GameObject gameCompletion;
    private TextMeshProUGUI timeCompleted;
    private TextMeshProUGUI totalDeaths;

    public TextMeshProUGUI bestTime;

    //Sensitivity Variables;
    [Header("Sensitivity Variables")]
    public Slider sensitivity_slider;
    private Button apply_btn;
    private Button exit_btn;
    private Button quit_btn;
    internal float sensitivity;
    private TextMeshProUGUI senstivity_output;
    
    [Header("Tutorial level")]
    public GameObject[] tutorial_coins;

    [Header("Connector 1")]
    public Vector3 respawnConnector1;

    [Header("Level 1")]
    public GameObject[] level1_coins;
    public Vector3 respawnLevel1;

    [Header("Level 2")]
    public GameObject[] level2_coins;
    public Vector3 respawnLevel2;
    
    
    private float tutorialTimer = 0;
    //private float level1Timer = 0;
    //private float level2Timer = 0;

    private Button reload_btn;

    public Image m_ReticleImage;

    public static GameManager instance = null;


    internal bool pauseMenuActive = false;
    internal bool reloadLevel = false;
    internal Image grapple_availableImage;



    //Level bools
    public enum CurrentLevel { 
        tutorialLevel,
        connector1,
        level1,
        connector2,
        level2,
    }

    //Controlling actions for tutorial.
    internal int deathCounter = 0;
    internal bool canLookAround = true;
    internal bool canMove = false;
    internal bool canJump = false;
    internal bool canRun = false;
    internal bool canGrapple = false;
    internal bool canShoot = false;
    internal bool countTimer = true;
    internal bool isRespawning = false;
    private AudioSource gameAudioSource;

    public CurrentLevel currentLevel;

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
        exit_btn = GameObject.Find("Exit").GetComponent<Button>();
        quit_btn = GameObject.Find("Quit").GetComponent<Button>();
        pauseMenu = GameObject.Find("PauseMenu");
        gameCompletion = GameObject.Find("CompletionScreen");
        timeCompleted = GameObject.Find("CompletionTime").GetComponent<TextMeshProUGUI>();
        totalDeaths = GameObject.Find("TotalDeaths").GetComponent<TextMeshProUGUI>();
        grapple_availableImage = GameObject.Find("GrappleAvailable").GetComponent<Image>();
        apply_btn.onClick.AddListener(() => ApplySetting());
        reload_btn.onClick.AddListener(() => ReloadLevel());
        exit_btn.onClick.AddListener(() => ExitToMenu());
        quit_btn.onClick.AddListener(() => Quit());
        bestTime.text = "Time to beat : 600.00";
        gameAudioSource = AudioManager.instance.gameObject.GetComponent<AudioSource>();

        currentLevel = CurrentLevel.tutorialLevel;

        if (isTutorial)
            gameAudioSource.PlayOneShot(AudioManager.instance.introClip);
        else {
            canMove = true;
            canShoot = true;
            canRun = true;
            canGrapple = true;
            canJump = true;
        }
    }

    private void Update()
    {
        if (gameAudioSource.isPlaying) {
            canMove = false;
            countTimer = false;
            m_Music.Pause();
        }
        else{
            canMove = true;
            countTimer = true;
            m_Music.Play();
        }

        CheckGameOver();

        if (isRespawning) Time.timeScale = 0.0f;
        else Time.timeScale = 1.0f;

        PauseMenuHandler();

        TimerHandler();

    }

    void CheckGameOver() {
        if (HUDscoreTracker.instance.score >= (level1_coins.Length + level2_coins.Length + tutorial_coins.Length)) {
            Time.timeScale = 0.0f;
            gameCompletion.SetActive(true);
            timeCompleted.text = tutorialTimer.ToString("000.00");
            totalDeaths.text = deathCounter.ToString();
        }
        else {
            gameCompletion.SetActive(false);
        }
    }

    void AudioHandler() {
        switch (currentLevel) {
            case CurrentLevel.connector1:
                gameAudioSource.PlayOneShot(AudioManager.instance.connector1);
                break;
            case CurrentLevel.level1:
                gameAudioSource.PlayOneShot(AudioManager.instance.level1Clip);
                break; 
            case CurrentLevel.level2:
                gameAudioSource.PlayOneShot(AudioManager.instance.connector2); 
                break;
            case CurrentLevel.connector2:
                gameAudioSource.PlayOneShot(AudioManager.instance.level2Clip);
                break;
        }
    
    }

    void PauseMenuHandler() {
        if (pauseMenuActive)
        {
            Debug.Log("Pause True");

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            countTimer = false;
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
        else
        {
            Debug.Log("Pause False");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if(!gameAudioSource.isPlaying) countTimer = true;
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
        }
    }

    //void FlowSwitcher() {
    //    if (HUDscoreTracker.instance.score == tutorial_coins.Length)
    //    {
    //        currentLevel = CurrentLevel.connector1;
    //    }
    //    else if (HUDscoreTracker.instance.score == tutorial_coins.Length + level1_coins.Length) {
    //        currentLevel = CurrentLevel.connector2;
    //    }
    
    //}

    void TimerHandler() {

        switch (currentLevel) {

            case CurrentLevel.tutorialLevel:
                if (HUDscoreTracker.instance.score < tutorial_coins.Length && countTimer)
                {
                    Debug.Log("Tutorial Timer");
                    tutorialTimer += Time.unscaledDeltaTime;
                    timeText.text = tutorialTimer.ToString("000.00");
                }
                break;
            case CurrentLevel.level1:
                if (HUDscoreTracker.instance.score < (level1_coins.Length + tutorial_coins.Length) && countTimer) {
                    tutorialTimer += Time.unscaledDeltaTime;
                    timeText.text = tutorialTimer.ToString("000.00");
                }
                break;
            case CurrentLevel.level2:
                if (HUDscoreTracker.instance.score < (level2_coins.Length + level1_coins.Length + tutorial_coins.Length) && countTimer)
                {
                    tutorialTimer += Time.unscaledDeltaTime;
                    timeText.text = tutorialTimer.ToString("000.00");
                }
                break;
            default:
                Debug.Log("incorrect Level");
                break;
        
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

    void ExitToMenu() {
        SceneManager.LoadScene(0);
    }

    void Quit() {
        Application.Quit();
    }

}
