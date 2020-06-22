using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SadariManager : MonoBehaviour
{
    private DatabaseManager theDB;
    public PlayerStatManager theStat;
    public StockManager theStock;
    public MiniGameOnOff theMini;

    //사다리와 교차로
    [Header("LineCanvas")]
    public GameObject[] Line;
    public GameObject[] Verse;

    //상단 부분 인원수 부분
    [Header("UpPanel")]
    public TextMeshProUGUI peopleNumberText;
    public Slider slider;

    //플레이어와 도착결과, 플레이어 정렬 그리드 틀, 플레이어 스크립트
    [Header("Player&Finish")]
    public GameObject[] Player;
    public GameObject[] Finish;
    public GridLayoutGroup PlayersGl;
    public Player[] PlayerCs;

    //결과 내용, 유저가 고른 플레이어
    [Header("Result")]
    private List<string> result;
    public GameObject[] playerCheck;
    public TextMeshProUGUI[] inGameResultText;

    //startgame 패널, 사다리 가림막, 결과창, 결과내용, 게임중 터치 방지 패널
    [Header("OtherPanel")]
    public GameObject GameStartPanel;
    public GameObject FencePanel;
    public GameObject ResultPanel;
    public TextMeshProUGUI[] ResultText;
    public GameObject BlindPanel;

    [Header("Exit")]
    public GameObject ExitPanel;
    public TextMeshProUGUI ExitPanelText;
    public GameObject SadariGameObject;

    /*변수*/
    int peopleNumber;
    int playerNum;
    bool playerEnd;
    public bool player;

    /*기타*/
    private NoticeManager theNotice;
    private MiniGameOnOff theMiniGame;
    private new SpriteRenderer renderer;
    public StockReportManager theReport;

    public void init()
    {
        theNotice = FindObjectOfType<NoticeManager>();
        theMiniGame = FindObjectOfType<MiniGameOnOff>();
        LadderSetting(false);
        GameStartPanel.SetActive(true);
        ExitPanel.SetActive(false);

        //랜덤으로 나올 상품들
        result = new List<string>();
        string[] randomString = new string[12] { "꽝", "꽝", "꽝", "꽝", "꽝", "본전", "하트", "하트", "하트", "잭팟X6", "주식하나", " 잭팟X3" };

        for (int i = 0; i < 6; i++)
        {
            int rand = Random.Range(0, 12);
            result.Add(randomString[rand]);
            inGameResultText[i].text = result[i];
            inGameResultText[i].gameObject.SetActive(false);
        }
    }

    void Start()
    {
    }

    void Update()
    {
        //false 일 경우 진행
        // 유저가 내려가면 더이상 실행 x, 유저가 아직 안내려가면 계속 체크
        if (playerEnd)
            return;

        if (!PlayerCs[playerNum].isEnd)
            //isEnd 아니면 playerEnd 아님
            playerEnd = false;
        else
            //isEnd 이면 playerEnd 임
            playerEnd = true;
        
        //유저가 종료했으면 실행
        if (playerEnd)
        {
            ResultPanel.SetActive(true);
            for (int i = 0; i < 6; i++)
            {
                playerCheck[i].SetActive(false);
            }
            playerCheck[playerNum].SetActive(true);

            for (int i = 0; i < peopleNumber; i++)
            {
                if(i != playerNum)
                    ResultText[i].text = result[i];
                else
                {
                    ResultText[playerNum].text = result[PlayerCs[playerNum].finishNum - 1];

                    if (ResultText[playerNum].text == "하트")
                    {
                        if(theStat.numberOfHarts < 5)
                        {
                            theNotice.NotificationAppear("하트당첨");
                            renderer = theStat.harts[theStat.numberOfHarts].GetComponent<SpriteRenderer>();
                            renderer.color = new Color(1f, 1f, 1f);
                            theStat.numberOfHarts += 1;
                        }
                       else
                            theNotice.NotificationAppear("하트당첨");
                    }
                    else if(ResultText[playerNum].text == "본전")
                    {
                        theStat.myAllMoney += theMini.inputMoney;
                        theNotice.NotificationAppear(theMini.inputMoney.ToString() + "$ 본전당첨");
                        /*레포트 미니게임 번 돈*/
                        theReport.nMonthEarnEstateMoney += theMini.inputMoney;
                    }
                    else if(ResultText[playerNum].text == "잭팟X6")
                    {
                        theStat.myAllMoney += theMini.inputMoney*6;
                        theNotice.NotificationAppear((theMini.inputMoney * 6).ToString() + "$ 당첨");
                        /*레포트 미니게임 번 돈*/
                        theReport.nMonthEarnEstateMoney += theMini.inputMoney * 6;
                    }
                    else if (ResultText[playerNum].text == "잭팟X3")
                    {
                        theStat.myAllMoney += theMini.inputMoney*3;
                        theNotice.NotificationAppear((theMini.inputMoney*3).ToString() + "$ 당첨");
                        /*레포트 미니게임 번 돈*/
                        theReport.nMonthEarnEstateMoney += theMini.inputMoney * 3;
                    }
                    else if(ResultText[playerNum].text == "주식하나")
                    {
                        int stockN = 0;

                        for (int n = 0; n < theStat.myStockCountArray.Length; n++)
                            if (theStat.myStockCountArray[n] > 0)
                                stockN = n;

                            if (theStat.myStockCountArray[stockN] >= 1)
                            {
                                theStat.myStockCountArray[stockN] += 1;
                                theNotice.NotificationAppear(theStock.stockSc[stockN].stockNamePrice[stockN, 0] + " 주식 당첨");
                                /*레포트 미니게임 번 돈*/
                                theReport.nMonthEarnMiniMoney += theStock.stockSc[stockN].thisStockPrice;
                            }
                            else
                            {
                                theNotice.NotificationAppear("주식 미보유로인한 수령불가");
                            }
                    }

                    else
                        theNotice.NotificationAppear(ResultText[playerNum].text);
                }
            }

            //게임 종료
            BlindPanel.SetActive(false);
            StartCoroutine(ExitCoroutine());
            theMiniGame.ProfileSetting();
        }

    }

    IEnumerator ExitCoroutine()
    {
        theDB = FindObjectOfType<DatabaseManager>();
        yield return new WaitForSeconds(3f);

        ExitPanel.SetActive(true);
        //DB에 상품 저장
        theDB.GetItemResult(ResultText[playerNum].text);
        string ExitButton = "";
        ButtonClick(ExitButton);
    }

    public void LadderSetting(bool start)
    {
        ResultPanel.SetActive(false);

        //인원수 넣기
        peopleNumber = (int)slider.value;
        peopleNumberText.text = peopleNumber + "명";

        //끝난것들 초기화
        for (int i = 0; i < 6; i++)
        {
            PlayerCs[i].Clear();
            playerEnd = false;
        }

        //플레이어 정렬하기
        PlayersGl.enabled = true;

        //라인, 끝번호, 플레이어, 결과 텍스트 부모셀을 인원수 만큼 활성화
        for (int i = 0; i < 6; i++)
        {
            if (i < peopleNumber)
            {
                Line[i].SetActive(true);
                Finish[i].SetActive(true);
                Player[i].SetActive(true);
                ResultText[i].transform.parent.gameObject.SetActive(true);
            }
            else
            {
                Line[i].SetActive(false);
                Finish[i].SetActive(false);
                Player[i].SetActive(false);
                ResultText[i].transform.parent.gameObject.SetActive(false);
            }
        }

        //모든교차로 box collider 비활성화
        for (int i = 0; i < Verse.Length; i++)
        {
            Verse[i].SetActive(false);
        }

        //활성화 된 라인들의 7행 중 2~4행을 랜덤하게 활성화 *** 중요 ***
        for (int i = 0; i < peopleNumber - 1; i++)
        {
            List<int> list = new List<int>() { 7 * i, 7 * i + 1, 7 * i + 2, 7 * i + 3, 7 * i + 4, 7 * i + 5, 7 * i + 6 };
            for (int j = 0; j < Random.Range(2, 5); j++)
            {
                int rand = Random.Range(0, list.Count);
                Verse[list[rand]].SetActive(true);
                list.Remove(rand);
            }
        }
    }

    //모든 버튼 입력
    public void ButtonClick(string whatbutton)
    {
        switch (whatbutton)
        {
            //StartPanel - 게임시작 버튼
            case "StartGame":
                GameStartPanel.SetActive(false);
                FencePanel.SetActive(true);
                LadderSetting(true);
                for (int i = 0; i < peopleNumber; i++)
                    inGameResultText[i].gameObject.SetActive(true);
                break;

            //ExitPanel - 종료 버튼
            case "Exit":
                theNotice.NotificationAppear("게임을 종료합니다.");
                SadariGameObject.SetActive(false);
                break;

            default:
                for (int i = 0; i < 6; i++)
                {
                    if (whatbutton == "Player" + (i + 1).ToString())
                    {
                        FencePanel.SetActive(false);
                        player = true;
                        PlayerCs[i].StartCoroutine("Move");
                        playerNum = i;
                        player = false;
                    }
                }
                break;
        }
    }
}
