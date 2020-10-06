/**
 * @brief 날짜 관리
 * @date 2020/03/05 마지막수정
 * @file DateManager.cs
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DateManager : MonoBehaviour
{

    public GameObject FloatingTextPrefab;

    public static bool activated = true;
    public TextMeshProUGUI dateText;
    public GameObject stopPanel;
    private LandmarksHandler theBuild;

    public int year = 0;
    public int month = 1;
    public int day = 1;
    public int hour = 1;

    /* 달마다 최대 일수를 미리 저장해놓음 ex) maxDays[0] 은 1월 최대 일수 */
    private int[] maxDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    public float updateTime; /* 업데이트 주기 */
    private float currentTime;

    void Start()
    {
        currentTime = updateTime;
    }

    void Update()
    {
        if (activated)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0) /*업데이트 할 때가 되면*/
            {
                currentTime = updateTime;
                hour++;

                if (hour >= 24)
                {
                    hour = 0;
                    day++; //날짜 증가
                    if (day > maxDays[month - 1]) //최대 일수가 넘어갈 경우 ex) 1월 32일
                    {
                        if (month < 12)
                        { //월을 넘긴다.
                            month++;
                            day = 1;
                        }
                        else
                        { /* 12월달이 넘어가면 년을 넘긴다. */
                            year++;
                            month = 1;
                            day = 1;
                        }
                    }
                    /* date Text를 다음과 같은 양식으로 설정한다. */
                }
                dateText.text = year + "년 " + month + "월 " + day + "일 " + hour + "시";
            }
        }
    }

    /* 시간이 흐르도록 설정한다. */
    public void Activate()
    {
        if (!MiniGameOnOff.activated)
        {
            activated = true;
            stopPanel.SetActive(false);
        }
    }

    /* 시간이 멈추도록 설정한다 */
    public void NotActivate()
    {
        activated = false;
        stopPanel.SetActive(true);
    }
}