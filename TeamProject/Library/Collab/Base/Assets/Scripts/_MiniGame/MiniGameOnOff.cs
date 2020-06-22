using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniGameOnOff : MonoBehaviour
{
    private PlayerStatManager theStat; /* Stat 정보 가져오기 */
    private NoticeManager theNotice; /* 알림창 사용할 수 있게 하기 */
    private BuildingManager theBuilding;
    public StockReportManager theReport; /*미니게임한거 레포트에 반영*/

    /* 각 게임 정보 가져오기 */
    private OddEvenGame theOddEven;
    private SadariManager theSadari;
    private roulette theRoulette;

    /* 내 정보 간략하게 알려주기 */
    public TextMeshProUGUI myAllMoney;
    public TextMeshProUGUI playCountText;

    /* 버튼 클릭시 게임 패널 제어 */
    public GameObject OEGamePanel;
    public GameObject SadariPanel;
    public GameObject RoulettePanel;
    public GameObject SlotPanel;
    public GameObject AskPanel;

    public int playCount = 0;

    /*입장료*/
    public int inputMoney;
    public TextMeshProUGUI inputMoneyT1;
    public TextMeshProUGUI inputMoneyT2;
    public TextMeshProUGUI inputMoneyT3;
    public TextMeshProUGUI inputMoneyT4;

    private int selectedButton;

    public static bool activated = false;

    void Start()
    {
        theNotice = FindObjectOfType<NoticeManager>();
        theOddEven = FindObjectOfType<OddEvenGame>();
        theStat = FindObjectOfType<PlayerStatManager>();
        theBuilding = FindObjectOfType<BuildingManager>();
        InitMoneyChange();
    }

    /* 홀짝 게임 버튼을 누르면 게임 시작 */
    public void OddEvenStart()
    {
        theOddEven.inputMoney = inputMoney;
        if(theStat.myAllMoney >= inputMoney) /* 500 이상 있을 경우 */
        {
            if (theStat.numberOfHarts > 0) /* 하트가 있을 경우 게임 시작 */
            {
                theStat.HartOff();
                theStat.myAllMoney -= inputMoney;
                theNotice.NotificationAppear(inputMoney.ToString() + "$ 입장료");
                OEGamePanel.SetActive(true);
                theOddEven.init();
                playCount++; //게임 실행 횟수 증가
                ProfileSetting();
                theBuilding.UpdateAllConditions(3); //건물 조건 만족 확인하기
                theReport.nMonthTradeMiniCount += 1; /*미니게임 플레이횟수 증가*/
                theReport.nMonthUseMiniMoney += inputMoney; /*미니게임 게임비 쓴 돈*/
            }
            else //없을 경우, 시작 못함
            {
                theNotice.NotificationAppear("하트가 부족합니다.");
            }
        }
        else //돈이 부족할 경우, 시작 못함
        {
            theNotice.NotificationAppear("돈이 부족합니다.");
        }
    }

    /* 사다리 게임 버튼을 누르면 게임 시작 */
    public void SadariStart()
    {
        if(theStat.myAllMoney >= inputMoney)//돈이 있을 경우
        {
            if (theStat.numberOfHarts > 0)//하트가 있을 경우
            {
                theStat.HartOff();
                theStat.myAllMoney -= inputMoney;
                theNotice.NotificationAppear(inputMoney.ToString() + "$ 입장료");
                SadariPanel.SetActive(true);
                theSadari = FindObjectOfType<SadariManager>();
                theSadari.init();
                playCount++; //게임 실행 횟수 증가
                ProfileSetting();
                theBuilding.UpdateAllConditions(3); //건물 조건 만족 확인하기
                theReport.nMonthTradeMiniCount += 1; /*미니게임 플레이횟수 증가*/
                theReport.nMonthUseMiniMoney += inputMoney; /*미니게임 게임비 쓴 돈*/
            }
            else//하트가 없을 경우
            {
                theNotice.NotificationAppear("하트가 부족합니다.");
            }
        }
        else//돈이 없을 경우
        {
            theNotice.NotificationAppear("돈이 부족합니다.");
        }
    }

    /* 룰렛 게임 버튼을 누르면 게임 시작 */
    public void RouletteStart()
    {
        if(theStat.myAllMoney >= inputMoney)//돈이 있을 경우
        {
            if (theStat.numberOfHarts > 0)//하트가 있을 경우
            {
                theStat.HartOff();
                theStat.myAllMoney -= inputMoney;
                RoulettePanel.SetActive(true);
                theNotice.NotificationAppear(inputMoney.ToString() + "$ 입장료");
                theRoulette = FindObjectOfType<roulette>();
                theRoulette.init();
                ProfileSetting();
                playCount++; //게임 실행횟수 증가
                theBuilding.UpdateAllConditions(3); //건물 조건 만족 확인하기
                theReport.nMonthTradeMiniCount += 1; /*미니게임 플레이횟수 증가*/
                theReport.nMonthUseMiniMoney += inputMoney; /*미니게임 게임비 쓴 돈*/
            }
            else//하트가 없을 경우
            {
                theNotice.NotificationAppear("하트가 부족합니다.");
            }
        }
        else//돈이 없을 경우
        {
            theNotice.NotificationAppear("돈이 부족합니다.");
        }
    }

    public void SlotMachineStart()
    {
        if (theStat.myAllMoney >= inputMoney)//돈이 있을 경우
        {
            if (theStat.numberOfHarts > 0)//하트가 있을 경우
            {
                theStat.HartOff();
                theStat.myAllMoney -= inputMoney;
                SlotPanel.SetActive(true);
                theNotice.NotificationAppear(inputMoney.ToString() + "$ 입장료");
                playCount++; //게임 실행횟수 증가
                ProfileSetting();
                theBuilding.UpdateAllConditions(3); //건물 조건 만족 확인하기
                theReport.nMonthTradeMiniCount += 1; /*미니게임 플레이횟수 증가*/
                theReport.nMonthUseMiniMoney += inputMoney; /*미니게임 게임비 쓴 돈*/
            }
            else//하트가 없을 경우
            {
                theNotice.NotificationAppear("하트가 부족합니다.");
            }
        }
        else//돈이 없을 경우
        {
            theNotice.NotificationAppear("돈이 부족합니다.");
        }
    }

    /* 슬롯 머신 실행 끝 
     * 결과에 따라 돈을 얻는다.
     */
    public void AfterSlotMachine(int x, int y, int z)
    {
        if(x==y && y == z)
        {
            switch (x)
            {
                //watermelon
                case 0:
                    theStat.myAllMoney += inputMoney * 20;
                    theReport.nMonthEarnMiniMoney += inputMoney * 20;
                    theNotice.NotificationAppear("대박! 건 돈의 20 배를 가져간다!");
                    break;
                //heart
                case 1:
                    theStat.myAllMoney += inputMoney * 20;
                    theReport.nMonthEarnMiniMoney += inputMoney * 20;
                    theNotice.NotificationAppear("대박! 건 돈의 20 배를 가져간다!"); 
                    break;
                //seven
                case 2:
                    theStat.myAllMoney += inputMoney * 777;
                    theReport.nMonthEarnMiniMoney += inputMoney * 777;
                    theNotice.NotificationAppear("잭팟! 건 돈의 777 배를 가져간다!"); 
                    break;
                //win
                case 3:
                    theStat.myAllMoney += inputMoney * 100;
                    theReport.nMonthEarnMiniMoney += inputMoney * 100;
                    theNotice.NotificationAppear("대박! 건 돈의 100 배를 가져간다!"); 
                    break;
                //cherry
                case 4:
                    theStat.myAllMoney += inputMoney * 20;
                    theReport.nMonthEarnMiniMoney += inputMoney * 20;
                    theNotice.NotificationAppear("대박! 건 돈의 20 배를 가져간다!"); 
                    break;
            }
        }
        else if (x == y)
        {
            switch (x)
            {
                //watermelon
                case 0:
                    theStat.myAllMoney += inputMoney;
                    theReport.nMonthEarnMiniMoney += inputMoney * 1;
                    theNotice.NotificationAppear("그래도 본전은 찾았다!");
                    break;
                //heart
                case 1:
                    theStat.myAllMoney += inputMoney * 1;
                    theReport.nMonthEarnMiniMoney += inputMoney * 1;
                    theNotice.NotificationAppear("그래도 본전은 찾았다!");
                    break;
                //seven
                case 2:
                    theStat.myAllMoney += inputMoney * 5;
                    theReport.nMonthEarnMiniMoney += inputMoney * 5;
                    theNotice.NotificationAppear("대박! 건 돈의 5배를 가져간다!");
                    break;
                //win
                case 3:
                    theStat.myAllMoney += inputMoney * 5;
                    theReport.nMonthEarnMiniMoney += inputMoney * 5;
                    theNotice.NotificationAppear("대박! 건 돈의 5배를 가져간다!");
                    break;
                //cherry
                case 4:
                    theStat.myAllMoney += inputMoney * 1;
                    theReport.nMonthEarnMiniMoney += inputMoney * 1;
                    theNotice.NotificationAppear("그래도 본전은 찾았다!");
                    break;
            }
        }
        else if (y == z)
        {
            switch (y)
            {
                //watermelon
                case 0:
                    theStat.myAllMoney += inputMoney;
                    theReport.nMonthEarnMiniMoney += inputMoney * 1;
                    theNotice.NotificationAppear("그래도 본전은 찾았다!");
                    break;
                //heart
                case 1:
                    theStat.myAllMoney += inputMoney * 1;
                    theReport.nMonthEarnMiniMoney += inputMoney * 1;
                    theNotice.NotificationAppear("그래도 본전은 찾았다!");
                    break;
                //seven
                case 2:
                    theStat.myAllMoney += inputMoney * 5;
                    theReport.nMonthEarnMiniMoney += inputMoney * 5;
                    theNotice.NotificationAppear("대박! 건 돈의 5배를 가져간다!");
                    break;
                //win
                case 3:
                    theStat.myAllMoney += inputMoney * 5;
                    theReport.nMonthEarnMiniMoney += inputMoney * 5;
                    theNotice.NotificationAppear("대박! 건 돈의 5배를 가져간다!");
                    break;
                //cherry
                case 4:
                    theStat.myAllMoney += inputMoney * 1;
                    theReport.nMonthEarnMiniMoney += inputMoney * 1;
                    theNotice.NotificationAppear("그래도 본전은 찾았다!");
                    break;
            }
        }
        else if (x == z)
        {
            switch (x)
            {
                //watermelon
                case 0:
                    theStat.myAllMoney += inputMoney;
                    theNotice.NotificationAppear("그래도 본전은 찾았다!");
                    break;
                //heart
                case 1:
                    theStat.myAllMoney += inputMoney * 1;
                    theNotice.NotificationAppear("그래도 본전은 찾았다!");
                    break;
                //seven
                case 2:
                    theStat.myAllMoney += inputMoney * 5;
                    theNotice.NotificationAppear("대박! 건 돈의 5배를 가져간다!");
                    break;
                //win
                case 3:
                    theStat.myAllMoney += inputMoney * 5;
                    theNotice.NotificationAppear("대박! 건 돈의 5배를 가져간다!");
                    break;
                //cherry
                case 4:
                    theStat.myAllMoney += inputMoney * 1;
                    theNotice.NotificationAppear("그래도 본전은 찾았다!");
                    break;
            }
        }
    }

    /* profile 정보 세팅 */
    public void ProfileSetting()
    {
        theStat = FindObjectOfType<PlayerStatManager>();
        myAllMoney.text = "내가 가진 돈 : $" + string.Format("{0}", theStat.myAllMoney.ToString("n0"));
        playCountText.text = "누적 플레이 횟수 : " + playCount;
    }

    public void InitMoneyChange()
    {
        /*신용등급에 따른 입장료의 변화*/
        /*함수 호출시 선언 필요*/
        theStat = FindObjectOfType<PlayerStatManager>();

        if (theStat.playerCredit > 7)
        {
            inputMoney = 1000;
            inputMoneyT1.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT2.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT3.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT4.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
        }
        else if (theStat.playerCredit > 5)
        {
            inputMoney = 10000;
            inputMoneyT1.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT2.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT3.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT4.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
        }
        else if (theStat.playerCredit > 3)
        {
            inputMoney = 50000;
            inputMoneyT1.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT2.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT3.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT4.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
        }
        else if (theStat.playerCredit > 1)
        {
            inputMoney = 100000;
            inputMoneyT1.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT2.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT3.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT4.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
        }
        else if (theStat.playerCredit == 1)
        {
            inputMoney = 1000000;
            inputMoneyT1.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT2.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT3.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
            inputMoneyT4.text = "비용 : $" + string.Format("{0}", inputMoney.ToString("n0"));
        }
    }

    /* 버튼 클릭 */
    public void SelectButton(int i)
    {
        selectedButton = i;
        AskPanel.SetActive(true); //게임을 시작하시겠습니까?
    }

    /* 게임을 시작하시겠습니까? 네 버튼 클릭 */
    public void StartGame()
    {
        AskPanel.SetActive(false);
        if(selectedButton == 0)
        {
            OddEvenStart();
        }
        else if(selectedButton == 1)
        {
            SadariStart();
        }
        else if(selectedButton == 2)
        {
            RouletteStart();
        }
        else if(selectedButton == 3)
        {
            SlotMachineStart();
        }
    }

    private void OnEnable()
    {
        activated = true;
    }

    private void OnDisable()
    {
        activated = false;
    }
}
