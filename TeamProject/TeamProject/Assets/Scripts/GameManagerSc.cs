using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerSc : MonoBehaviour
{
    public GameObject panel;
    public GameObject YesB;
    public GameObject NoB;
    public TextMeshProUGUI gameOverT;
    private DateManager theDate;
    //public static string webplayerQuitURL = "http://google.com";

    void Start()
    {
        theDate = FindObjectOfType<DateManager>();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            theDate.NotActivate();
            panel.SetActive(true);
            YesB.SetActive(true);
            NoB.SetActive(true);
            gameOverT.text = "게임을 종료합니다.";
        }
    }

    public void ButtonClikcYes()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
        //Application.OpenURL(webplayerQuitURL);
    }

    public void ButtonClickNo()
    {
        YesB.SetActive(false);
        NoB.SetActive(false);
        panel.SetActive(false);
        theDate.Activate();
    }
    public void ButtonClickExit()
    {
        YesB.SetActive(true);
        NoB.SetActive(true);
        panel.SetActive(true);
        theDate.NotActivate();
        gameOverT.text = "게임을 종료합니다.";
    }
}
