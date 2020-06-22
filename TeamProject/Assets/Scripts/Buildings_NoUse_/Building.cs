using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public GameObject prefab;
    public bool buildingSold = false;
    public bool buildingGetIn = false;

    [Header("기본 빌딩 속성")]
    public int buildingNum;
    public string buildingName;
    [Multiline(3)]
    public string buildingDetail;
    public double buildingPrice;

    [Header("최소 조건")]
    public int minLevel;
    public int minCash;
    public int minTrade;
    public int minPlay;
    public int indexOfMinStock;
    public int minStock;

    [Header("건물로 돈을 벌 수 있죠")]
    public int monthlyRent;

    [Header("빌딩 설명 Text")]
    public TextMeshProUGUI buildingNameText;
    public TextMeshProUGUI buildingDetailText;
    public TextMeshProUGUI buildingPriceText;

    [Header("조건 Text")]
    public TextMeshProUGUI[] conditionText;

    [Header("Complete Panel")]
    public GameObject[] completePanel;
    public GameObject soldPanel;

    public bool[] conditionComplete = { false, false, false, false, false};

    public PlayerStatManager theStat;
    public MiniGameOnOff theMiniGame;

    void Start()
    {
        monthlyRent = (int)(buildingPrice * 0.05);

        /* 기본 설정 텍스트 */
        buildingNameText.text = buildingName;
        buildingPriceText.text = "가격 : $" + string.Format("{0}", buildingPrice.ToString("n0"));

        /* 조건 설정, 조건이 없으면 안보이게 설정 */
        if(minLevel == 0 || minLevel == 9)
            conditionText[0].gameObject.SetActive(false);
        else
            conditionText[0].text = "신용등급" + minLevel + " 이상";

        if (minCash == 0)
            conditionText[1].gameObject.SetActive(false);
        else
            conditionText[1].text = "주식 보유액" + minCash + "이상";

        if (minTrade == 0)
            conditionText[2].gameObject.SetActive(false);
        else
            conditionText[2].text = "주식 거래 " + minTrade + "이상";

        if (minPlay == 0)
            conditionText[3].gameObject.SetActive(false);
        else
            conditionText[3].text = "미니 게임 " + minPlay + "회 이상 플레이";

        if (minStock == 0)
            conditionText[4].gameObject.SetActive(false);
        else
            conditionText[4].text = indexOfMinStock + "번 주식 " + minStock + "주 이상 보유";
    }

    /* 킬때마다 */
    void OnEnable()
    {
        UpdatePanels();
    }

    /* 조건불만족 -> 만족 또는 조건만족 -> 불만족으로 업데이트 시킨다 */
    public void UpdateCondition(int conditionIndex)
    {
        if(conditionIndex == 0) //레벨을 확인합니다.
        {
            if(minLevel >= theStat.playerCredit)
            {
                conditionComplete[0] = true;
            }
            else
            {
                conditionComplete[0] = false;
            }
        }
        else if(conditionIndex == 1) //주식 보유액
        {
            if(minCash <= theStat.myAllStockMoney)
            {
                conditionComplete[1] = true;
            }
            else
            {
                conditionComplete[1] = false;
            }
        }
        else if(conditionIndex == 2) //수정예정
        {
            if(minTrade <= theStat.stockTradeCountNum)
            {
                conditionComplete[2] = true;
            }
            else
            {
                conditionComplete[2] = false;
            }
        }
        else if(conditionIndex == 3)
        {
            if(minPlay <= theMiniGame.playCount)
            {
                conditionComplete[3] = true;
            }
            else
            {
                conditionComplete[3] = false;
            }
        }

        UpdatePanels();
    }

    public void UpdatePanels()
    {
        for(int i = 0; i < completePanel.Length; i++)
        {
            if(conditionComplete[i] == true)
            {
                completePanel[i].SetActive(true);
            }
            else
            {
                completePanel[i].SetActive(false);
            }
        }
    }

    /* 팔렸을 경우 구매 완료로 표시 */
    public void Sold()
    {
        buildingSold = true;
        soldPanel.SetActive(true);
        this.gameObject.GetComponent<Button>().interactable = false; /* 더 이상 클릭할 수 없게 설정*/
        var clone = Instantiate(prefab, this.gameObject.transform);
        clone.GetComponent<FloatingText>().SetText("구매 완료");
        clone.GetComponent<FloatingText>().text.color = Color.red;

    }

    /* 모든 조건을 만족했는지 확인하는 함수, return 값으로 true 또는 false가 return 된다. */
    public bool CheckAllConditionsComplete()
    {
        for(int i=0;i<conditionComplete.Length; i++)
        {
            if(conditionComplete[i] == false)
            {
                return false;
            }
        }
        return true;
    }
}
