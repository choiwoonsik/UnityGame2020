using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CitizenChat : MonoBehaviour
{
    public GameObject chat;
    public TextMeshProUGUI chatT;
    private float currentTime;
    private string[] chatInText = { "벼리><", "벼리벼리", "벼리벼리벼리!!", "StarThis", "별이별이*_*",
                                    "*별별*", "@별2별2@", "이별 저별 내별", "냠!", "뇽뇽", "엣헴!", "ㅎㅅㅎ"};

    // Start is called before the first frame update
    void Start()
    {
        chatT.text = chatInText[Random.Range(0, chatInText.Length)];
        
        currentTime = Random.Range(10, 15);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            chat.SetActive(true);
            currentTime -= Time.deltaTime;
        }
        else
        {
            chat.SetActive(false);
        }
    }

    public void setTime()
    {
        currentTime = Random.Range(10, 15);
    }

    public void setChatInText(string _text)
    {
        this.chatT.text = _text;
    }
}
