using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CitizenBuildingChat : MonoBehaviour
{
    public GameObject chat;
    public TextMeshProUGUI chatBuildingT;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = Random.Range(4, 6);
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
        currentTime = Random.Range(4, 6);
    }

    public void setChatBuildingInText(string _text)
    {
        this.chatBuildingT.text = _text;
    }
}
