using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerStatManager : MonoBehaviour
{
    /*오브젝트 참조*/
    public MyStocks myStocks;   /*보유주식 가격 표시*/
    public StockCheck stockCheck;   //stockCheck클래스에 접근하여 보유 주식 값 출력
    public SellBuyManager sellbuyManager;   //sellbuymanager를 통해 주식 이름, 번호, 보유 가치 및 stock의 오브젝트에 접근(on,off) 
    public PlayerStatOnOff statOnff;    //playerstat의 on,off를 위한 변수
    public NoticeManager theNotice; //신용등급 변동시 알림창 발동
    public DateManager theDate; //시간 및 월 참조
    public MiniGameOnOff theMiniGame; //미니게임 입장료 변경
    private LandmarksHandler theHander;

    //on,off체크 변수
    private bool OnOff = false;

    [Header("PlyaerHart")]
    //하트 표시
    public Hart[] harts;
    public int numberOfHarts;
    //하트 충전을 위한 시간체크 - 시간보다 정해진 초가 낫다고 생각....
    private new SpriteRenderer renderer;
    public float currentTime = 400.0f;
    public TextMeshProUGUI remainingTime; /*하트가 충전 될 남은 시간 선택적으로 표시하는 텍스트*/


    [Header("PlayerProFile")]
    private bool levelInfoOnOff = false;
    public GameObject profileImage;
    public GameObject playerLevelInfo;

    /*플레이어 현재 개인정보 텍스트*/
    public TextMeshProUGUI playerLevelText;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerSexText;
    public TextMeshProUGUI playerAgeText;
    public TextMeshProUGUI playerEtcText;

    [Header("PlayerAssetProFile")]
    /*플레이어 현재 자산정보 텍스트*/
    public GameObject playerAssetProfile;
    public TextMeshProUGUI playerCashText;
    public TextMeshProUGUI playerStockText;
    public TextMeshProUGUI playerEstateText;

    [Header("PlayerStockCheck")]
    /*플레이어 현재 자산정보 변수*/
    public GameObject playerHoldingStockPanel;
    public int[] myStockCountArray;
    public double myAllStockMoney;
    public double myAllMoney;
    public double myAllEstate;

    [Header("PlayerCollectionProFile")]
    public GameObject playerCollectionProfile;

    [Header("PlayerCreditProFile")]
    //플레이어 신용등급 관련
    /*플레이어 다음 신용등급 컷트라인*/
    public TextMeshProUGUI playerLevelUpIndexText;
    public TextMeshProUGUI playerLevelDownIndexText;
    public TextMeshProUGUI StockLimitCountText;


    [Header("others")]
    //플레이어스탯에서 관리하는 player의 소유 현금, 주식, 땅
    public double playerCash;
    public double playerStock;
    public double playerEstate;

    /*신용등급에 따른 주식개수 제한 값*/
    public int playerCredit;
    public float stockLimitCount;
    public int stockTradeCountNum;

    /*신용등급 컷트라인 기준*/
    private int playerLevelUpIndex;
    private int playerLevelDownIndex;

    public void playerStatOnOff()
    {
        if (!OnOff)
        {
            statOnff.paleyrStatOnOff.SetActive(true);
            OnOff = true;
        }
        else if (OnOff)
        {
            //프로필 버튼 클릭시 창 전환, 주식 시스템 다시 시작
            levelInfoOnOff = false;
            playerLevelInfo.SetActive(false);
            playerHoldingStockPanel.SetActive(false);
            playerCollectionProfile.SetActive(false);
            playerAssetProfile.SetActive(true);
            statOnff.paleyrStatOnOff.SetActive(false);
            OnOff = false;
        }
    }

    public void CheckHoldingStocksButton()
    {
        playerHoldingStockPanel.SetActive(true);
    }

    public void ExitButton()
    {
        playerHoldingStockPanel.SetActive(false);
    }

    public void PlayerCollectionProfileShow()
    {
        playerAssetProfile.SetActive(false);
        playerCollectionProfile.SetActive(true);
    }

    public void PlayerAssetProfileShow()
    {
        playerCollectionProfile.SetActive(false);
        playerAssetProfile.SetActive(true);
    }

    public void PlayerLevelImformationShow()
    {
        if (!levelInfoOnOff)
        {
            playerLevelInfo.SetActive(true);
            levelInfoOnOff = true;
        }
        else if (levelInfoOnOff)
        {
            playerLevelInfo.SetActive(false);
            levelInfoOnOff = false;
        }
    }

    void Start()
    {
        theHander = FindObjectOfType<LandmarksHandler>();

        //하트 충전을 위한 시간 받아오기
        myAllMoney = 500000;
        numberOfHarts = 5;
        playerCredit = 9;
        playerLevelUpIndex = 1000;
        playerLevelDownIndex = 200;
        stockLimitCount = 1;
        stockTradeCountNum = 1;

        StockLimitCountText.text = "보유 주식 가능 수: " + stockLimitCount.ToString();
        playerLevelText.text = "나의 신용등급: " + playerCredit + "등급";
        playerLevelUpIndexText.text = "신용등급 상승갱신 기준 :" +playerLevelUpIndex.ToString();
        playerLevelDownIndexText.text = "신용등급 하락갱신 기준 :" + playerLevelDownIndex.ToString();

        for (int i = 0; i < sellbuyManager.stockSc.Length; i++)
            sellbuyManager.lockButton[i].GetComponent<Button>().interactable = false;

    }
    void Update()
    {
        /*하트 업데이트 방식 이곳에 수정*/
        if (numberOfHarts < 5)/* 5개 미만일 경우 시간 카운팅 시작 */
        {
            currentTime -= Time.deltaTime;
            remainingTime.text = "남은 시간 : " + (int)(currentTime) + "초";
        }
        if(currentTime < 0)/* 시간이 되면 하나 업데이트를 시킨다 */
        {
            currentTime = 400.0f;
            renderer = harts[numberOfHarts].GetComponent<SpriteRenderer>();
            renderer.color = new Color(1f, 1f, 1f);
            numberOfHarts += 1;
            if(numberOfHarts == 5)
            {
                remainingTime.text = "";
            }
            else
            {
                remainingTime.text = "남은 시간 : " + (int)currentTime + "초";
            }
        }

        /*일정 시간마다 신용등급 체크*/
        if(theDate.hour == 12)
        {
            StartCoroutine(WaitCoroutine());
            /*보유 총 자산이 기준치 이상 && 주식 거래 횟수 일정 값 이상*/
            if (myAllMoney + myAllStockMoney > playerLevelUpIndex && stockTradeCountNum > 1)
                PlayerCreditUp();
            else if (myAllMoney + myAllStockMoney < playerLevelDownIndex && stockTradeCountNum < 10 && playerCredit < 9)
                PlayerCreditDown();

            StockLimitCountText.text = "보유 주식 가능 수: " + stockLimitCount.ToString();
        }

        /*주식 및 현금 정보 업데이트 */
        if (OnOff)
        {
            StartCoroutine(CheckStocksCoroutine());
            StartCoroutine(CheckEstateCoroutine());
        }

        /*9, 8*/
        if(playerCredit > 7)
        {
            for (int i = 0; i < 6; i++)
            {
                sellbuyManager.lockPanel[i].SetActive(false);
                sellbuyManager.lockButton[i].GetComponent<Button>().interactable = true;
            }
        }
        /*7, 6, 5, 4*/
        else if(playerCredit > 3)
        {
            for (int i = 0; i < 5; i++)
            {
                sellbuyManager.lockPanel[i].SetActive(false);
                sellbuyManager.lockButton[i].GetComponent<Button>().interactable = true;
            }
        }
        /*3, 2*/
        else if (playerCredit > 1)
        {
            for (int i = 0; i < 7; i++)
            {
                sellbuyManager.lockPanel[i].SetActive(false);
                sellbuyManager.lockButton[i].GetComponent<Button>().interactable = true;
            }
        }
        /*1*/
        else if (playerCredit == 1)
        {
            for(int i = 0; i < sellbuyManager.stockSc.Length; i++)
            {
                sellbuyManager.lockPanel[i].SetActive(false);
                sellbuyManager.lockButton[i].GetComponent<Button>().interactable = true;
            }
        }

    }
    
    IEnumerator CheckStocksCoroutine()
    {
        //유저 현금, 주식 보유량 최신화 & 지속적인 주식 보유량 최신화
        playerCash = myAllMoney;
        playerCashText.text = string.Format("{0}", playerCash.ToString("n0")) + "$";

        playerStock = myAllStockMoney;
        playerStockText.text = string.Format("{0}", playerStock.ToString("n0")) + "$";

        myStockCountArray = myStocks.GsMyAllStocks;

        for (int i = 0; i < myStockCountArray.Length; i++)
        {
            stockCheck.stockCheckNum[i].GetComponent<StockCheck>().stockName.text = sellbuyManager.stockSc[i].stockNameT.text.ToString();
            if (myStockCountArray[i] > 0)
            {
                stockCheck.stockCheckNum[i].GetComponent<StockCheck>().stockCount.text = myStockCountArray[i].ToString() + "개";
            }
            else
            {
                stockCheck.stockCheckNum[i].GetComponent<StockCheck>().stockCount.text = "보유 수량 X";
            }
        }
        yield return new WaitForSeconds(0.05f);
    }

    IEnumerator CheckEstateCoroutine()
    {
        int checkCount = 0;
        playerEstate = 0;
        for(int i = 0; i < theHander.Buildings.Length; i++)
        {
            if (theHander.Buildings[i].has)
            {
                playerEstate += theHander.Buildings[i].buildingPrice;
                checkCount++;
            }
        }
        playerEstateText.text = "보유 건물 수량: " + checkCount + "채, " + "평가 가치 :$" + playerEstate;
        yield return new WaitForSeconds(0.05f);

    }

    /* 하트 하나 사용하는 함수 */
    public void HartOff()
    {
        numberOfHarts -= 1; //개수를 줄이고
        renderer = harts[numberOfHarts].GetComponent<SpriteRenderer>();
        renderer.color = new Color(51 / 255f, 51 / 255f, 51 / 255f); //색깔 투명하게 설정
    }

    /*신용등급 조정위원회*/
    public void PlayerCreditUp()
    {
        /*신용등급 오르면 거래횟수 초기화*/
        stockTradeCountNum = 0;

        /*신용등급 올림*/
        if (playerCredit > 1)
            playerCredit--;
        else
            playerCredit = 1;

        /*신용등급에 따른 차등 주식보유 증가량*/
        //9~6
        if (playerCredit > 5)
            stockLimitCount += 0.5f;
        //5~3
        else if (playerCredit > 2)
            stockLimitCount += 1;
        //1~2
        else
            stockLimitCount = 8;

        playerLevelText.text = "나의 신용등급: " + playerCredit + "등급";

        /*플레이어레벨이 높아질수록 다음 기준선이 지수승으로 올라간다*/
        playerLevelUpIndex = playerLevelUpIndex * (int)Math.Pow(2, 10-playerCredit);
        playerLevelDownIndex = playerLevelDownIndex * (int)Math.Pow(2, 10-playerCredit);

        playerLevelUpIndexText.text = "신용등급 상승갱신 기준 :" + playerLevelUpIndex.ToString();
        playerLevelDownIndexText.text = "신용등급 하락갱신 기준 :" + playerLevelDownIndex.ToString();

        theNotice.NotificationAppear("신용등급 상승  " + (playerCredit + 1).ToString() + "->" + playerCredit.ToString() + "  확인 요망");
        theMiniGame.InitMoneyChange();
    }
    public void PlayerCreditDown()
    {
        /*신용등급 떨어지면 거래횟수 초기화*/
        stockTradeCountNum = 0;

        /*신용등급 내림*/
        if (playerCredit < 9)
            playerCredit++;
        else
            playerCredit = 9;

        /*신용등급에 따른 차등 주식보유 증가량*/
        //1~2
        if (playerCredit < 2)
            stockLimitCount -= 0;
        //3~5
        else if (playerCredit < 6)
            stockLimitCount -= 2;
        //6~9
        else
            stockLimitCount = 1;
        //0
        if(stockLimitCount < 1)
            stockLimitCount = 1;

        playerLevelText.text = "나의 신용등급: " + playerCredit + "등급";

        /*플레이어레벨이 높아질수록 다음 기준레벨이 지수승으로 올라간다*/
        playerLevelUpIndex = playerLevelUpIndex / (int)Math.Pow(2, 10 - playerCredit);
        playerLevelDownIndex = playerLevelDownIndex / (int)Math.Pow(2, 10 - playerCredit);

        playerLevelUpIndexText.text = "신용등급 상승갱신 기준 :" + playerLevelUpIndex.ToString();
        playerLevelDownIndexText.text = "신용등급 하락갱신 기준 :" + playerLevelDownIndex.ToString();

        theNotice.NotificationAppear("신용등급 하락  " + (playerCredit - 1).ToString() + "->" + playerCredit.ToString() +"  확인 요망");

        theMiniGame.InitMoneyChange();
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitUntil(() => theDate.hour != 13);
    }
}
