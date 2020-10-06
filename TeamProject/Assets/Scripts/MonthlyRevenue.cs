using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonthlyRevenue : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI date;
    public TextMeshProUGUI revenueText;
    public TextMeshProUGUI peopleSay;

    private PlayerStatManager theStat;
    private LandmarksHandler theBuildings;
    private DateManager theDate;
    private bool flag = false;
    private double revenue;
    private int oldBuildingIndex;
    private int smellyBuildingIndex;

    // Start is called before the first frame update
    void Start()
    {
        theDate = FindObjectOfType<DateManager>();
        theBuildings = FindObjectOfType<LandmarksHandler>();
        theStat = FindObjectOfType<PlayerStatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DateManager.activated && !flag && theDate.day == 5)
        {
            flag = true;
            revenue = 0;
            oldBuildingIndex = -1;
            smellyBuildingIndex = -1;
            for(int i=0; i<theBuildings.Buildings.Length; i++)
            {
                if(theBuildings.Buildings[i].type == "b" && theBuildings.Buildings[i].has 
                    && theBuildings.Buildings[i].stockIn)
                {
                    if(theBuildings.Buildings[i].buildingCondition >= 0.5)
                    {
                        revenue += theBuildings.Buildings[i].monthlyRevenue;
                    }
                    else
                    {
                        if (theBuildings.Buildings[i].monthlyRevenue * theBuildings.Buildings[i].buildingCondition > 0)
                            revenue += theBuildings.Buildings[i].monthlyRevenue * theBuildings.Buildings[i].buildingCondition;
                        oldBuildingIndex = i;
                        if(theBuildings.Buildings[i].buildingCondition < 0.1f)
                        {
                            smellyBuildingIndex = i;
                        }
                    }
                }
            }

            //건물 수익이 납니다.
            if(revenue > 0)
            {
                panel.SetActive(true);
                date.text = "<color=#154121>" + theDate.year + "</color>년 <color=#154121>" + theDate.month + "</color>월 명세서";
                revenueText.text = "$" + string.Format("{0}", revenue.ToString("n0"));
                if(oldBuildingIndex == -1 && smellyBuildingIndex == -1) //더러운 빌딩이 하나도 없을 경우 (완전한 월세를 받음)
                {
                    peopleSay.text = "건물 관리를 완벽하게 해주셔서 감사해요!";
                }
                else if(smellyBuildingIndex != -1) //똥파리 날리는 빌딩이 있을 경우
                {
                    peopleSay.text = "<color=#154121>" + theBuildings.Buildings[smellyBuildingIndex].buildingName + "</color>에 너무 더러워서 못 살겠어";
                }
                else
                {
                    peopleSay.text = "관리를 안 해주셔서 <color=#154121>" + theBuildings.Buildings[oldBuildingIndex].buildingName + "</color> 월세 좀 덜냈어요";
                }

                theStat.myAllMoney += revenue;
            }
        }
        else if(theDate.day == 6 && flag)
        {
            flag = false;
        }
    }
}
