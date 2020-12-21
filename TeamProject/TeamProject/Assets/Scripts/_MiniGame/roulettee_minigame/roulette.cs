using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class roulette : MonoBehaviour
{
    /*게임 오프젝트*/
    public GameObject Roulette;
    public GameObject RoulettePanel;
    public GameObject RoulettePlate;
    public GameObject Exit;
    public GameObject StartGameB;
    public GameObject activeFalse;
    public GameObject exitFalse;
    public GameObject goFalse;
    public Image resultImage;
    public TextMeshProUGUI resultItemT;

    /*가르키는 니들, 아이템이미지, 보여주는 슬롯*/
    public Transform needle;
    public Sprite[] itemImage;
    public Image[] displayItemSolt;

    List<int> StartList = new List<int>();  //랜덤 뽑기를 위한 리스트
    List<int> ResultIndexList = new List<int>();  //당첨된 스킬을 저장할 리스트
    int ItemCnt = 8;
    int closetIndex;

    private MiniGameOnOff theMiniGame;
    private PlayerStatManager theStat;
    private NoticeManager theNotice;
    private MiniGameOnOff theMini;
    private StockReportManager theReport;
    private List<string> result;
    private new SpriteRenderer renderer;

    public void init()
    {
        goFalse.SetActive(false);
        activeFalse.SetActive(false);
        theMiniGame = FindObjectOfType<MiniGameOnOff>();
        theStat = FindObjectOfType<PlayerStatManager>();
        theNotice = FindObjectOfType<NoticeManager>();
        theMini = FindObjectOfType<MiniGameOnOff>();
        theReport = FindObjectOfType<StockReportManager>();

        /*그림 및 글 초기화*/
        resultItemT.text = "두구두구";
        Sprite newSprite = Resources.Load<Sprite>("mystery") as Sprite;
        resultImage.GetComponent<Image>().sprite = newSprite;

        //랜덤으로 나올 상품들
        result = new List<string>();
        string[] randomString = new string[8] {"하트", "꽝", "본전X5", "꽝", "본전X2", "하트", "꽝", "다시하기"};

        for (int i = 0; i < ItemCnt; i++)
        {
            /*리스트에 0~7의 숫자를 넣는다*/
            StartList.Add(i);
        }

        for (int i = 0; i < ItemCnt; i++)
        {
            /*랜덤 인덱스에 0~7의 숫자를 배정*/
            int randomIndex = Random.Range(0, StartList.Count);
            /*결과리스트에 스타트 리스트에 있는 숫자중 랜덤 인덱스의 위치에 있는 값을 넣는다*/
            ResultIndexList.Add(StartList[randomIndex]);
            /*보여줄 아이템슬롯 0~7번에 랜점으로 배정된 이미지를 넣는다*/
            displayItemSolt[i].sprite = itemImage[StartList[randomIndex]];
            result.Add(randomString[StartList[randomIndex]]);
            /*이미 넣은 인덱스는 제거한다, count => 8, 7, 6, ...*/
            StartList.RemoveAt(randomIndex);
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartRoulette());
        closetIndex = -1;
        exitFalse.SetActive(true);
        goFalse.SetActive(true);
    }

    IEnumerator StartRoulette()
    {
        float randomSpd = Random.Range(1.5f, 2.5f);
        float rotateSpeed = 30f * randomSpd;

        while (true)
        {
            yield return null;
            if (rotateSpeed <= 0.08f)  //속도가 0.03 이하가 되면 멈춤
                break;

            rotateSpeed = Mathf.Lerp(rotateSpeed, 0, Time.deltaTime * 1.1f);
            yield return new WaitForSeconds(0.01f);
            RoulettePlate.transform.Rotate(0, 0, rotateSpeed); //z축 회전
        }
        yield return new WaitForSeconds(0.5f);
        Result();
    }

    void Result()
    {
        if (closetIndex != -1)
            ExitGame();

        float closetDis = 50000f;
        float allItemDis = 0f;

        for (int i = 0; i < ItemCnt; i++)  //바늘과 당첨된 스킨..?의 거리를 계산
        {
            /*바늘과 모든 아이템들과의 거리 계산*/
            allItemDis = Vector2.Distance(displayItemSolt[i].transform.position, needle.position);

            if (allItemDis < closetDis)
            {
                closetDis = allItemDis;
                closetIndex = i;
            }
        }

        resultImage.sprite = displayItemSolt[closetIndex].sprite;  //당첨된 스킨...?을 밑에 표시
        resultItemT.text = result[closetIndex] + " 당첨";
        theMiniGame.ProfileSetting();
        exitFalse.SetActive(false);
        activeFalse.SetActive(true);
        ShowResultItem();
    }

    public void ShowResultItem()
    {
        if (result[closetIndex] == "하트")
        {
            if (theStat.numberOfHarts < 5)
            {
                theNotice.NotificationAppear("하트당첨");
                renderer = theStat.harts[theStat.numberOfHarts].GetComponent<SpriteRenderer>();
                renderer.color = new Color(1f, 1f, 1f);
                theStat.numberOfHarts += 1;
            }
            else
            {
                theNotice.NotificationAppear("하트당첨");
            }
        }
        else if (result[closetIndex] == "본전X2")
        {
            theStat.myAllMoney += theMini.inputMoney * 2;
            theNotice.NotificationAppear((theMini.inputMoney * 2).ToString() + "$ 본전 두배당첨");
            /*레포트 미니게임 번 돈*/
            theReport.nMonthEarnEstateMoney += theMini.inputMoney * 2;
        }
        else if (result[closetIndex] == "본전X5")
        {
            theStat.myAllMoney += theMini.inputMoney * 5;
            theNotice.NotificationAppear((theMini.inputMoney * 5).ToString() + "$ 본전 다섯배당첨");
            /*레포트 미니게임 번 돈*/
            theReport.nMonthEarnEstateMoney += theMini.inputMoney * 5;
        }
        else if (result[closetIndex] == "꽝")
        {
            theNotice.NotificationAppear("꽝");
        }
        else if (result[closetIndex] == "다시하기")
        {
            theNotice.NotificationAppear("한번 더~!");
            activeFalse.SetActive(false);
            goFalse.SetActive(false);
            init();
        }
    }


    public void ExitGame()
    {
        Roulette.SetActive(false);
        result.Clear();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
