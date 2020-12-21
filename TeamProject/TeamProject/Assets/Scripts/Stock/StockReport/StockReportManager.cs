using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StockReportManager : MonoBehaviour
{
    public GameObject reportCanvas;
    public GameObject reportButton;

    public DateManager theDate;
    public PlayerStatManager theStat;
    public MiniGameOnOff theMini;
    public MyStocks theStocks;
    public NoticeManager theNotice;
    private LandmarksHandler theBuilding;

    [Header("Var")]
    private int checkMonth;
    public TextMeshProUGUI reprotTitleT;

    [Header("StockText")]
    public TextMeshProUGUI nMonthOwnStockT;
    public TextMeshProUGUI nMonthUseStockMoneyT;
    public TextMeshProUGUI nMonthEarnStockMoneyT;
    public TextMeshProUGUI nMonthDiffStockT;
    public TextMeshProUGUI nMonthDiffStockPercentT;
    public TextMeshProUGUI nMonthTradeStockCountT;

    [Header("StockVar")]
    private int nMonthOwnstcokCount;
    private double nMonthDiffStockPercent;
    public double nMonthUseStockMoney;
    public double nMonthEarnStockMoney;
    public int nMonthTradeStockCount;

    [Header("EstateText")]
    public TextMeshProUGUI nMonthOwnBuildT;
    public TextMeshProUGUI nMonthUseEstateMoneyT;
    public TextMeshProUGUI nMonthEarnEstateMoneyT;
    public TextMeshProUGUI nMonthDiffEstateT;
    public TextMeshProUGUI nMonthDiffEstatePercentT;

    [Header("EstateVar")]
    public int nMonthOwnBuild;
    public double nMonthUseEstateMoney;
    public double nMonthEarnEstateMoney;
    public double nMonthDiffEstate;
    public double nMonthDiffEstatePercent;

    [Header("MiniText")]
    public TextMeshProUGUI nMonthTradeMiniCountT;
    public TextMeshProUGUI nMonthUseMiniMoneyT;
    public TextMeshProUGUI nMonthEarnMintMoneyT;
    public TextMeshProUGUI nMonthDiffMiniT;
    public TextMeshProUGUI nMonthDiffMiniPercentT;

    [Header("MiniVar")]
    public int nMonthTradeMiniCount;
    public double nMonthUseMiniMoney;
    public double nMonthEarnMiniMoney;
    public double nMonthDiffMini;
    public double nMonthDiffMiniPercent;

    [Header("AllEarnUse")]
    public TextMeshProUGUI nMonthUseT;
    public TextMeshProUGUI nMonthEarnT;
    public TextMeshProUGUI nMonthDiffPercentT;

    public double nMonthUse;
    public double nMonthEarn;
    public double nMonthDiffPercent;

    [Header("BeforMonth")]
    public TextMeshProUGUI BeforMonthDiffT;
    public TextMeshProUGUI BeforMonthDiffPecentT;

    private double beforMonthDiff;
    private double N;


    public void ReportShow()
    {
        reportCanvas.SetActive(true);
    }
    public void ReportOff()
    {
        reportCanvas.SetActive(false);
    }

    public void CheckStock()
    {
        /*주식 갯수 파악*/
        for (int i = 0; i < theStat.myStockCountArray.Length; i++)
            if (theStocks.GsMyAllStocks[i] > 0)
                nMonthOwnstcokCount = theStat.myStockCountArray[i];

        /*주식 출력*/
        nMonthOwnStockT.text = nMonthOwnstcokCount.ToString() + "개";
        nMonthUseStockMoneyT.text = "$" + string.Format("{0}", nMonthUseStockMoney.ToString("n1"));
        nMonthEarnStockMoneyT.text = "$" + string.Format("{0}", nMonthEarnStockMoney.ToString("n1"));
        nMonthDiffStockT.text = "$"+ string.Format("{0}", (nMonthEarnStockMoney - nMonthUseStockMoney).ToString("n1"));

        if (nMonthUseStockMoney == 0)
        {
            if (nMonthEarnStockMoney > 0)
                nMonthDiffStockPercent = 100;
            else
                nMonthDiffStockPercent = 0;
        }
        else
            nMonthDiffStockPercent = (float)((nMonthEarnStockMoney - nMonthUseStockMoney) / nMonthUseStockMoney * 100);

        nMonthDiffStockPercentT.text = string.Format(nMonthDiffStockPercent.ToString("n1")) + "%";
        nMonthTradeStockCountT.text = nMonthTradeStockCount.ToString() + "회";
    }



    public void CheckEstate()
    {
        /*건물 소유 개수*/
        for(int i=0; i < theBuilding.Buildings.Length; i++)
        {
            if (theBuilding.Buildings[i].has)
            {
                nMonthOwnBuild += 1;
            }
        }
        nMonthDiffEstate = nMonthEarnEstateMoney - nMonthUseEstateMoney;
        if (nMonthUseEstateMoney != 0)
            nMonthDiffEstatePercent = nMonthDiffEstate / nMonthUseEstateMoney * 100;
        else
        {
            if(nMonthDiffEstate > 0)
                nMonthDiffEstatePercent = 100;
            else if(nMonthDiffEstate == 0)
                nMonthDiffEstatePercent = 0;
        }

        nMonthOwnBuildT.text = nMonthOwnBuild + "개";
        nMonthEarnEstateMoneyT.text = "$" + string.Format("{0}", nMonthEarnEstateMoney.ToString("n1"));
        nMonthUseEstateMoneyT.text = "$" + string.Format("{0}", nMonthUseEstateMoney.ToString("n1"));
        nMonthDiffEstateT.text = "$" + string.Format("{0}", nMonthDiffEstate.ToString("n1"));
        nMonthDiffEstatePercentT.text = string.Format("{0}", nMonthDiffEstatePercent.ToString("n0")) + "%";
    }


    public void CheckMini()
    {
        nMonthTradeMiniCountT.text = nMonthTradeMiniCount + "회";
        nMonthDiffMini = nMonthEarnMiniMoney - nMonthUseMiniMoney;
        if (nMonthDiffMini != 0)
            nMonthDiffMiniPercent = nMonthDiffMini / nMonthUseMiniMoney * 100;
        else
            nMonthDiffMiniPercent = 0;

        nMonthEarnMintMoneyT.text = "$" + string.Format("{0}", nMonthEarnMiniMoney.ToString("n1"));
        nMonthUseMiniMoneyT.text = "$" + string.Format("{0}", nMonthUseMiniMoney.ToString("n1"));
        nMonthDiffMiniT.text = "$" + string.Format("{0}", nMonthDiffMini.ToString("n1"));
        nMonthDiffMiniPercentT.text = string.Format("{0}", nMonthDiffMiniPercent.ToString("n0")) + "%";
    }

    public void CheckAll()
    {
        nMonthEarn = nMonthEarnEstateMoney + nMonthEarnStockMoney + nMonthEarnMiniMoney;
        nMonthUse = nMonthUseEstateMoney + nMonthUseStockMoney + nMonthUseMiniMoney;

        /* 초기화전 저장 - 직전달 순수익, 지금월 1일에 얻은 값*/
        beforMonthDiff = nMonthEarn - nMonthUse;


        if (nMonthUse != 0)
            nMonthDiffPercent = 100 - (nMonthEarn / nMonthUse * 100);
        else
            nMonthDiffPercent = 100;

        nMonthEarnT.text = "$" + string.Format("{0}", nMonthEarn.ToString("n1"));
        nMonthUseT.text = "$" + string.Format("{0}", nMonthUse.ToString("n1"));
        if(nMonthEarn > nMonthUse)
        {
            if (nMonthDiffPercent < 0)
                nMonthDiffPercent *= -1;
            nMonthDiffPercentT.text = "총 수익률: \n" + "<color=#FF0000> +" + string.Format("{0}", nMonthDiffPercent.ToString("n0")) + "</color>" + "%";
        }
        else if(nMonthEarn < nMonthUse)
            nMonthDiffPercentT.text = "총 수익률: \n" + "<color=#0000FF> -" + string.Format("{0}", nMonthDiffPercent.ToString("n0")) + "</color>" + "%";

        /* 주식*/
        nMonthOwnstcokCount = 0;
        nMonthUseStockMoney = 0;
        nMonthEarnStockMoney = 0;
        nMonthTradeStockCount = 0;

        /* 건물*/
        nMonthOwnBuild = 0;
        nMonthEarnEstateMoney = 0;
        nMonthUseEstateMoney = 0;
        nMonthDiffEstate = 0;
        nMonthDiffEstatePercent = 0;

        /* 미니*/
        nMonthTradeMiniCount = 0;
        nMonthEarnMiniMoney = 0;
        nMonthUseMiniMoney = 0;
        nMonthDiffMini = 0;
        nMonthDiffMiniPercent = 0;
    }

    public void BeforMonthCheck()
    {
        /*지금월 마지막일 마지막 시간에 1일에 얻은 값과 체크*/
        if (beforMonthDiff > 0)
            BeforMonthDiffT.text = "$" + "<color=#FF0000> +" + string.Format("{0}", beforMonthDiff.ToString("n0")) + "</color>";
        else
            BeforMonthDiffT.text = "$" + "<color=#0000FF> " + string.Format("{0}", beforMonthDiff.ToString("n0")) + "</color>";

        /*이번달 순수익(이제올 1일) - 저번달 순수익(이번 월초 1일)*/
        nMonthEarn = nMonthEarnEstateMoney + nMonthEarnStockMoney + nMonthEarnMiniMoney;
        nMonthUse = nMonthUseEstateMoney + nMonthUseStockMoney + nMonthUseMiniMoney;
        if (beforMonthDiff <= 0)
            N = (nMonthEarn - nMonthUse) + beforMonthDiff;
        else
            N = (nMonthEarn - nMonthUse) - beforMonthDiff;

        if (N > 0)
            BeforMonthDiffPecentT.text = "전월대비 수익액" + "<color=#FF0000> $" + string.Format("{0}", N.ToString("n0")) + "</color>";
        else if (N <= 0)
            BeforMonthDiffPecentT.text = "전월대비 수익액" + "<color=#0000FF> $" + string.Format("{0}", N.ToString("n0")) + "</color>";
    }

    void Start()
    {
        /* 주식*/
        checkMonth = 1;
        nMonthOwnstcokCount = 0;
        nMonthUseStockMoney = 0;
        nMonthEarnStockMoney = 0;
        nMonthTradeStockCount = 0;

        /* 건물*/
        nMonthOwnBuild = 0;
        nMonthEarnEstateMoney = 0;
        nMonthUseEstateMoney = 0;
        nMonthDiffEstate = 0;
        nMonthDiffEstatePercent = 0;

        /* 미니*/
        nMonthTradeMiniCount = 0;
        nMonthEarnMiniMoney = 0;
        nMonthUseMiniMoney = 0;
        nMonthDiffMini = 0;
        nMonthDiffMiniPercent = 0;

        /* 전달*/
        beforMonthDiff = -1;

        theBuilding = FindObjectOfType<LandmarksHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theDate.day == 4)
            reportButton.SetActive(false);

        if (theDate.month == checkMonth%12+1 && theDate.day == 1 && theDate.hour == 1)
        {
            reportButton.SetActive(true);

            theNotice.NotificationAppear("월간 수익률 보고서가 발행되었습니다");
            checkMonth += 1;

            if(theDate.month != 1)
                reprotTitleT.text = "<" + theStat.playerNameText.text + ">의 " + (theDate.month-1).ToString() + "월 자산 수익동향 레포트";
            else
                reprotTitleT.text = "<" + theStat.playerNameText.text + ">의 " + "12월 자산 수익동향 레포트";

            /*주식*/
            CheckStock();

            /*빌딩*/
            CheckEstate();

            /*미니*/
            CheckMini();

            /*전부*/
            CheckAll();

            /* 전달*/
            StartCoroutine(BeforMonthCoroutine());
        }
    }

    IEnumerator BeforMonthCoroutine()
    {
        if(theDate.month < 8)
        {
            if(theDate.month == 2)
                yield return new WaitUntil(() => theDate.day == 28);
            else if (theDate.month%2 == 1)
                yield return new WaitUntil(() => theDate.day == 31);
            else if(theDate.month%2 == 0)
                yield return new WaitUntil(() => theDate.day == 30);
        }
        else
        {
            if(theDate.month %2 == 0)
                yield return new WaitUntil(() => theDate.day == 30);
            else
                yield return new WaitUntil(() => theDate.day == 31);
        }

        yield return new WaitUntil(() => theDate.hour == 23);
        BeforMonthCheck();
    }
}
