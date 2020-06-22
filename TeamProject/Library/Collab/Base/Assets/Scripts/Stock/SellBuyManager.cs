using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellBuyManager : StockManager
{
    [Header("Stocks")]
    //보유 주식 UI
    public MyStock mystockOnOff;
    public StockReportManager theReport;
    public static int stockLimitCheckNum = 0;

    [Header("StatOnOff")]
    public GameObject myStockPanel;
    public GameObject otherStockPanel;

    //팔때
    [Header("Sell")]
    public GameObject prefab;
    public GameObject sellPanel;
    public TextMeshProUGUI sellStockCountPanel;

    //살때
    [Header("Buy")]
    public GameObject buyPanel;
    public TextMeshProUGUI buyStockCountPanel;

    //취소
    [Header("Cancel")]
    public GameObject cancelPanel;
    public TextMeshProUGUI cancelTextPanel;

    //주식 증감
    [Header("PlusMinus")]
    public GameObject plus;
    public GameObject minus;

    //주식 확인
    [Header("StockVar")]
    public int stockNumN;       // 몇번째 주식
    private int stockCount;     //구매 주식 수량

    //유저 자산
    [Header("MyAssetVar")]
    private float revenuePercent;

    //거래 후 UI
    [Header("TradeResult")]
    private double tradePrcie;
    public GameObject tradePanel;
    public TextMeshProUGUI stockNameText;
    public TextMeshProUGUI stockCountText;
    public TextMeshProUGUI stockMoneyText;
    public TextMeshProUGUI stockResultText;

    [Header("레벨에 따른 개수제한")]
    public GameObject[] lockPanel;
    public GameObject[] lockButton;

    [Header("기타")]
    private PlayerStatManager theStat;
    private BuildingManager theBuilding;

    // 함수 //

    //주식 번호 확인
    public void OnClicked(Button button)
    {
        stockNumN = int.Parse(button.name);
    }

    //팔때
    public void Sell()
    {
        stockCount = 1;
        sellPanel.SetActive(true);
    }

    //살때
    public void Buy()
    {
        stockCount = 1;
        buyPanel.SetActive(true);
    }

    //주식 증감
    public void CountPlus()
    {
        stockCount += 1;
        sellStockCountPanel.text = stockCount.ToString();
        buyStockCountPanel.text = stockCount.ToString();
    }
    public void CountMinus()
    {
        stockCount -= 1;
        if (stockCount <= 1)
            stockCount = 1;
        sellStockCountPanel.text = stockCount.ToString();
        buyStockCountPanel.text = stockCount.ToString();
    }
    public void UpFive()
    {
        stockCount += 5;
        sellStockCountPanel.text = stockCount.ToString();
        buyStockCountPanel.text = stockCount.ToString();
    }
    public void DownFive()
    {
        if (stockCount > 5)
            stockCount -= 5;
        else
            stockCount = 1;
        sellStockCountPanel.text = stockCount.ToString();
        buyStockCountPanel.text = stockCount.ToString();
    }
    public void BuyAll()
    {
        stockCount = (int)(theStat.myAllMoney / stockSc[stockNumN].thisStockPrice);
        if (stockCount < 1)
            stockCount = 1;
        sellStockCountPanel.text = stockCount.ToString();
        buyStockCountPanel.text = stockCount.ToString();
    }
    public void SellAll()
    {
        stockCount = myStocks.GsMyAllStocks[stockNumN];
        sellStockCountPanel.text = stockCount.ToString();
        buyStockCountPanel.text = stockCount.ToString();
    }

    //보유 주식 수량과 매도 수량 확인 후 주식 매도 진행
    //조건이 맞으면 나의 현금자산에서 주식 가격을 더해주고, 주식 보유수량을 낮춘다
    public void SellOk()
    {
        playerSM.myStockCountArray = myStocks.GsMyAllStocks;

        if (playerSM.myStockCountArray[stockNumN] >= stockCount)
        {
            /*주식 floating text*/
            GameObject clone = Instantiate(prefab, stockSc[stockNumN].gameObject.transform);
            clone.transform.position = new Vector3(clone.transform.position.x + 250, clone.transform.position.y, clone.transform.position.z);
            clone.GetComponent<FloatingText>().text.color = Color.white;
            clone.GetComponent<FloatingText>().text.fontSizeMax = 30;
            clone.GetComponent<FloatingText>().SetText(stockSc[stockNumN].stockNameT.text.ToString() + "을/를" + stockCount + "개 매도");

            /*주식보유수량 감소, 보유 주식 가치액 감소*/
            myStocks.GiveStock(stockNumN, stockCount);
            theStat.myAllMoney += stockSc[stockNumN].thisStockPrice * stockCount;

            /*주식 거래 횟수 파악*/
            theStat.stockTradeCountNum += 1;
            theReport.nMonthTradeStockCount += 1;

            /*보유 주식이 없으면 보유주식리스트에서 제외*/
            if (playerSM.myStockCountArray[stockNumN] == 0)
            {
                mystockOnOff.ownStock[stockNumN].SetActive(false);
                stockLimitCheckNum--;
                if (stockLimitCheckNum < 0)
                    stockLimitCheckNum = 0;
            }

            ButtonClick("Exit");

            //체결 성공 알림창
            //tradePanel.SetActive(true);
            //Image img = GameObject.Find("TradePanel").GetComponent<Image>();
            //img.color = Color.black;
            //Color color = img.color;
            //color.a = 1f;
            //img.color = color;
            
            /*거래액 체크, 월간 수입액 증가*/
            tradePrcie = stockSc[stockNumN].thisStockPrice * stockCount;
            theReport.nMonthEarnStockMoney += tradePrcie;

            stockNameText.text = stockSc[stockNumN].stockNameT.text.ToString() + "을/를 ";
            stockCountText.text = stockCount.ToString() + "개";
            stockMoneyText.text = "$" + string.Format("{0}", tradePrcie.ToString("n1")) + "에 체결";
            stockResultText.text = "매도하였습니다";
            stockCount = 1;
            StartCoroutine(WaitCoroutine());
        }
        else if (playerSM.myStockCountArray[stockNumN] < stockCount)
        {
            cancelPanel.SetActive(true);
            sellPanel.SetActive(false);
            cancelTextPanel.text = "초과 수량 입력";
            return;
        }
        else
        {
            cancelPanel.SetActive(true);
            sellPanel.SetActive(false);
            cancelTextPanel.text = "보유 주식 없음";
            playerSM.myStockCountArray[stockNumN] = 0;
            return;
        }
        playerSM.myStockCountArray = myStocks.GsMyAllStocks;
    }

    //보유 자산 확인 후 주식 가격과 비교 후 주식 매매 진행
    //체결 성공시 현금 자산을 주식 매수 가격 * 수량 만큼 빼주고 나의 주식 보유량을 늘려준다
    public void BuyOk()
    {
        playerSM.myStockCountArray = myStocks.GsMyAllStocks;

        if (theStat.myAllMoney >= stockCount * stockSc[stockNumN].thisStockPrice && stockLimitCheckNum <= theStat.stockLimitCount)
        {
            if (playerSM.myStockCountArray[stockNumN] >= 1)
            {
                /*주식 floating text*/
                GameObject clone = Instantiate(prefab, mystockOnOff.ownStock[stockNumN].gameObject.transform);
                clone.transform.position = new Vector3(clone.transform.position.x - 250, clone.transform.position.y, clone.transform.position.z);
                clone.GetComponent<FloatingText>().text.color = Color.white;
                clone.GetComponent<FloatingText>().text.fontSizeMax = 30;
                clone.GetComponent<FloatingText>().SetText(stockSc[stockNumN].stockNameT.text.ToString() + "을/를" + stockCount + "개 매수");

                /*주식보유수량 증가, 보유 주식 가치액 증가*/
                myStocks.GetStock(stockNumN, stockCount);
                theStat.myAllMoney -= stockSc[stockNumN].thisStockPrice * stockCount;

                /*주식 거래 횟수 파악*/
                theStat.stockTradeCountNum += 1;
                theReport.nMonthTradeStockCount += 1;

                /*주식 거래 차액 계산을 위한 변수값 계산*/
                double alreadyBuyPrice = mystockOnOff.ownStock[stockNumN].GetComponent<MyStock>().revenuePrice * (playerSM.myStockCountArray[stockNumN] - stockCount);
                double nowBuyPrice = stockSc[stockNumN].thisStockPrice * stockCount;
                int myAllCount = playerSM.myStockCountArray[stockNumN];

                /*주식 구매 평균액*/
                stockSc[stockNumN].averagePrice = (alreadyBuyPrice + nowBuyPrice) / myAllCount;

                /*주식 값 변동으로 인한 구매액 평균과의 차액*/
                mystockOnOff.ownStock[stockNumN].GetComponent<MyStock>().revenuePrice = stockSc[stockNumN].averagePrice;

                /*월간 지출 금액 증가*/
                theReport.nMonthUseStockMoney += nowBuyPrice;
            }
            else if (playerSM.myStockCountArray[stockNumN] < 1)
            {
                ++stockLimitCheckNum;
                if (stockLimitCheckNum <= theStat.stockLimitCount)
                {
                    mystockOnOff.ownStock[stockNumN].SetActive(true);

                    /*주식 floating text*/
                    GameObject clone = Instantiate(prefab, mystockOnOff.ownStock[stockNumN].gameObject.transform);
                    clone.transform.position = new Vector3(clone.transform.position.x - 250, clone.transform.position.y, clone.transform.position.z);
                    clone.GetComponent<FloatingText>().text.color = Color.white;
                    clone.GetComponent<FloatingText>().text.fontSizeMax = 30;
                    clone.GetComponent<FloatingText>().SetText(stockSc[stockNumN].stockNameT.text.ToString() + "을/를" + stockCount + "개 매수");

                    /*주식보유수량 증가, 보유 주식 가치액 증가*/
                    myStocks.GetStock(stockNumN, stockCount); // 주식 개수 ++
                    theStat.myAllMoney -= stockSc[stockNumN].thisStockPrice * stockCount;

                    /*주식 거래 횟수 파악*/
                    theStat.stockTradeCountNum += 1;
                    theReport.nMonthTradeStockCount += 1;

                    /*주식 거래 차액 계산을 위한 변수값 계산*/
                    mystockOnOff.ownStock[stockNumN].GetComponent<MyStock>().revenuePrice = stockSc[stockNumN].thisStockPrice;

                    /*주식 구매 평균액*/
                    stockSc[stockNumN].averagePrice = stockSc[stockNumN].thisStockPrice;

                    /*월간 지출 금액 증가*/
                    theReport.nMonthUseStockMoney += stockSc[stockNumN].thisStockPrice * stockCount;
                }
                else
                {
                    stockLimitCheckNum--;
                    cancelTextPanel.text = "레벨당 보유 가능한\n주식 계좌 개수\n 제한도달";
                    cancelPanel.SetActive(true);
                    StartCoroutine(WaitCoroutine());
                    buyPanel.SetActive(false);
                    return;
                }
            }

            ButtonClick("Exit");

            //체결 성공 알림창
            //tradePanel.SetActive(true);
            //Image img = GameObject.Find("TradePanel").GetComponent<Image>();
            //img.color = Color.black;
            //Color color = img.color;
            //color.a = 1f;
            //img.color = color;

            tradePrcie = stockCount * stockSc[stockNumN].thisStockPrice;
            stockNameText.text = stockSc[stockNumN].stockNameT.text.ToString() + "을/를 ";
            stockCountText.text = stockCount.ToString() + "개";
            stockMoneyText.text = "$" + string.Format("{0}", tradePrcie.ToString("n0")) + "에 체결";
            stockResultText.text = "매수하였습니다";
            stockCount = 1;
            StartCoroutine(WaitCoroutine());
        }
        else
        {
            cancelPanel.SetActive(true);
            buyPanel.SetActive(false);
            cancelTextPanel.text = "보유 현금 부족";
            StartCoroutine(WaitCoroutine());
            return;
        }
        playerSM.myStockCountArray = myStocks.GsMyAllStocks;
    }
    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(1f);
        tradePanel.SetActive(false);
        cancelPanel.SetActive(false);
    }

    //주식 거래중 일어나는 버튼 클릭에 대한 함수
    public void ButtonClick(string whatbutton)
    {
        switch (whatbutton)
        {
            case "Sell":
                stockCount = 1;
                sellStockCountPanel.text = stockCount.ToString();
                buyStockCountPanel.text = stockCount.ToString();
                sellPanel.SetActive(true);
                break;

            case "Buy":
                stockCount = 1;
                sellStockCountPanel.text = stockCount.ToString();
                buyStockCountPanel.text = stockCount.ToString();

                buyPanel.SetActive(true);
                break;

            case "Exit":
                sellPanel.SetActive(false);
                buyPanel.SetActive(false);
                break;

            case "Cancel":
                stockCount = 1;
                cancelPanel.SetActive(false);
                break;

            default:
                cancelPanel.SetActive(false);
                break;
        }
    }

    //나의 주식함수를 불러서 내가 소유중인 주식 스크립트 배열을 사용한다
    //기본적인 초기화 실시
    void Start()
    {
        mystockOnOff = FindObjectOfType<MyStock>();
        sellStockCountPanel.text = "0";
        buyStockCountPanel.text = "0";
        theStat = FindObjectOfType<PlayerStatManager>();
        theBuilding = FindObjectOfType<BuildingManager>();
    }

    //꾸준히 보유 주식의 가치, 현금자산을 업데이트 해주고
    //보유 주식의 각각의 가격또한 시장 시세에 맞춰서 업데이트 해준다
    //보유 주식 수량을 MyStocks Class에서 가져와서 맞춰준다
    void Update()
    {
        for (int i = 0; i < myStocks.myAllStocks.Length; i++)
        {
            revenuePercent = 0;
            playerSM.myStockCountArray = myStocks.GsMyAllStocks;
            if (playerSM.myStockCountArray[i] > 0)
            {
                mystockOnOff.ownStock[i].GetComponent<MyStock>().myStockNameT.text = stockSc[i].stockNamePrice[i, 0];
                if (stockSc[i].thisStockPrice / 10000 >= 1)
                {
                    mystockOnOff.ownStock[i].GetComponent<MyStock>().myStockPriceT.text = "$" + string.Format("{0}", (stockSc[i].thisStockPrice / 10000).ToString("n1") + "만");
                }
                else
                {
                    mystockOnOff.ownStock[i].GetComponent<MyStock>().myStockPriceT.text = "$" + string.Format("{0}", stockSc[i].thisStockPrice.ToString("n1"));
                }
                mystockOnOff.ownStock[i].GetComponent<MyStock>().myStockCount.text = playerSM.myStockCountArray[i].ToString() + "개";

                if (stockSc[i].thisStockPrice * 100 / stockSc[i].averagePrice >= 100)
                    revenuePercent = (float)(stockSc[i].thisStockPrice * 100 / stockSc[i].averagePrice) - 100;
                else if (stockSc[i].thisStockPrice * 100 / stockSc[i].averagePrice < 100)
                    revenuePercent = (float)(-1 * (100 - (stockSc[i].thisStockPrice * 100 / stockSc[i].averagePrice)));

                stockSc[i].ratingPrice = stockSc[i].thisStockPrice * playerSM.myStockCountArray[i] - stockSc[i].averagePrice * playerSM.myStockCountArray[i];

                if (revenuePercent < 0)
                {
                    revenuePercent *= -1;
                    mystockOnOff.ownStock[i].GetComponent<MyStock>().revenuePriceText.text = "<color=#0000FF>" + "- " + string.Format("{0}", revenuePercent.ToString("n1")) + "%" + "</color>";
                    if (stockSc[i].ratingPrice / 10000 >= 1)
                    {

                        mystockOnOff.ownStock[i].GetComponent<MyStock>().ratingPriceText.text = "<color=#0000FF>"+ "$" + string.Format("{0}", (stockSc[i].ratingPrice / 10000).ToString("n1") + "만") + "</color>";
                    }
                    else
                    {

                        mystockOnOff.ownStock[i].GetComponent<MyStock>().ratingPriceText.text = "<color=#0000FF>" + "$" + string.Format("{0}", stockSc[i].ratingPrice.ToString("n1")) + "</color>";
                    }
                }
                else if (revenuePercent > 0)
                {
                    mystockOnOff.ownStock[i].GetComponent<MyStock>().revenuePriceText.text = "<color=#FF0000>" + "+ " + string.Format("{0}", revenuePercent.ToString("n1")) + "%" + "</color>";
                    if (stockSc[i].ratingPrice / 10000 >= 1)
                    {

                        mystockOnOff.ownStock[i].GetComponent<MyStock>().ratingPriceText.text = "<color=#FF0000>" + "$" + string.Format("{0}", (stockSc[i].ratingPrice / 10000).ToString("n1") + "만") + "</color>";
                    }
                    else
                    {

                        mystockOnOff.ownStock[i].GetComponent<MyStock>().ratingPriceText.text = "<color=#FF0000>" + "$" + string.Format("{0}", stockSc[i].ratingPrice.ToString("n1")) + "</color>";
                    }
                }
            }
            else if (myStocks.GsMyAllStocks[i] == 0)
            {
                mystockOnOff.ownStock[i].GetComponent<MyStock>().myStockCount.text = playerSM.myStockCountArray[i].ToString() + "개";
                mystockOnOff.ownStock[i].GetComponent<MyStock>().revenuePriceText.text = "0%";
                mystockOnOff.ownStock[i].GetComponent<MyStock>().ratingPriceText.text = "$0";
            }
        }

        playerSM.myAllStockMoney = 0;

        for (int i = 0; i < playerSM.myStockCountArray.Length; i++)
        {
            if (myStocks.GsMyAllStocks[i] > 0)
            {
                playerSM.myAllStockMoney += playerSM.myStockCountArray[i] * stockSc[i].thisStockPrice;
            }
        }

        //빌딩 조건 체크
        theBuilding.UpdateAllConditions(1);
    }
}
