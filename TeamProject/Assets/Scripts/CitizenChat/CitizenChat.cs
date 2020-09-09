using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CitizenChat : MonoBehaviour
{
    public GameObject chat;
    public TextMeshProUGUI chatT;
    private float currentTime;
    private string[] chatInText = { "날씨가 너무 좋은데?", "오늘 주식이 좀 올랐을까?", "아 과제하기 싫다", "시험기간은 너무 힘들어",
"월급일은 언제오는거야?", "빨리 주말이 왔으면!", "더워더워 너무더워", "오늘 가족들이랑 뭘 먹지?", "아 오늘은 상사가 기분이 좋았으면~",
"와 저 자전거 예쁘다", "엄마한테 전화해볼까?", "오늘따라 기분이 별로네?", "오늘따라 기분이 너무 좋은걸?",
"아이고 삭신이야", "헤헤 우리집 강아지 너무 귀여워", "껄껄껄", "운동하니 기분이 좋은걸?",
"하루가 정말 길다", "앗 마스크 안썼다!", "밖에 공기가 너무 좋은걸?", "행복해~~~",
"친구들한테 주말에 만나자고 할까?"
};

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
