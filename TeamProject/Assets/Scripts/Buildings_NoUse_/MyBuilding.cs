using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* 내 빌딩 클래스, Building을 상속한다 */
public class MyBuilding : Building
{
    public float buildingCondition = 1.0f; //처음에는 1.0 이었다가(완벽한 컨디션) 최소 0s

    public TextMeshProUGUI buildingDateText;
    public TextMeshProUGUI buildingRentText;
    public TextMeshProUGUI buildingConditionText;

    public float updateTime = 100.0f;
    public float currentTime = 100.0f;
    public int flag = -1;

    void Start()
    {
        currentTime = updateTime;
    }

    /* 되팔기 가격 결정 */
    public double ResellValue()
    {
        return buildingPrice * 0.7f * buildingCondition;
    }

    public double MonthlyRentValue()
    {
        if(buildingCondition > 0.1f)
        {
            return monthlyRent;
        }
        else
        {
            return monthlyRent * buildingCondition * 8;
        }
    }
}
