using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    Button startBtn;
    Button quitBtn;

    // Start is called before the first frame update
    void Start()
    {
        startBtn = GameObject.Find("Start").GetComponent<Button>();
        startBtn.onClick.AddListener(OpenMenu);

        quitBtn = GameObject.Find("Quit").GetComponent<Button>();
        quitBtn.onClick.AddListener(QuitButton);
    }

    void OpenMenu() {
        SceneManager.LoadScene(1);
    }

    void QuitButton() {
//#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
//#elif
        //Application.Quit();
//#endif
    }
}
