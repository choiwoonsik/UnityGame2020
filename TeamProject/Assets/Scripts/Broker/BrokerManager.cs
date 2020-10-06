using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BrokerManager : MonoBehaviour
{
    [Header("Object")]
    /*브로커 등장*/
    public GameObject brokerShow;

    /*제안 및 제안 내용*/
    public GameObject offerPanel;
    public GameObject offerPanelOk;
    public GameObject YesNoChoice;

    /*제안 텍스트*/
    public TextMeshProUGUI offerPanelT;
    public TextMeshProUGUI offerPanelOkT;
    public TextMeshProUGUI offerAccuracyT;

    /*수락 버튼*/
    public TextMeshProUGUI offerOkT1;
    public TextMeshProUGUI offerOkT2;
    public TextMeshProUGUI offerOkT3;

    [Header("기타")]
    /*날짜, 주식*/
    public StockManager theStockManager;
    public DateManager theDate;
    public PlayerStatManager theStat;

    /*지역변수*/
    int M;
    int D;
    int N;
    int stockN;
    int accuracy;
    int accuracyMax = 50;
    bool On = false;
    bool rightEffect;
    bool opened = false;

    private readonly string[,] offerThema = new string[5, 2]
    {
        {"대량적자가 발생할거라는 소식이네", "신사업에 대 성공을 했다는군"},
        {"부정부패가 심하다는 얘기가 정부에 들어갔다는데", "청렴도 최우수 표창을 받는다는데"},
        {"개발포기를 조만간 선언한다는군", "비밀 프로젝트 개발성공했다는 이야기가 있어"},
        {"사장이 구속당할 거라는 소식이야" , "대통령 표창장을 조만간 받는다고 하네"},
        {"대량해고를 준비중이라는 얘기가 들려오고있어" , "실적 획기적 개선에 성공했다고 발표할것이네"}
    };

    /*브로커가 등장하는 월, 일*/
    public void BrokerShowFunc(int M)
    {
        if (theDate.month == M)
        {
            if(theDate.day == D)
            {
                brokerShow.SetActive(true);
            }
        }
    }

    /*거절 - 모든 창 닫음*/
    public void DeclineOffer()
    {
        offerPanel.SetActive(false);
        offerPanelOk.SetActive(false);
        brokerShow.SetActive(false);
        opened = true;
        On = false;
    }

    /*브로커 클릭 - 제안 확인*/
    public void ClickBroker()
    {
        opened = true;

        if (!On)
        {
            offerPanel.SetActive(true);
            BrokerOfferFunc();
            On = true;
        }
        else if (On)
        {
            brokerShow.SetActive(false);
            offerPanel.SetActive(false);
            offerPanelOk.SetActive(false);
        }
    }

    /*브로커 제안 수락 - 제안 내용 확인*/
    public void ClickOfferOk1()
    {
        /*100 ~ 85*/
        accuracy = Random.Range(-50, 50);
        if (-35 < accuracy && accuracy < 35)
            accuracy = 35;
        N = 9;
        YesNoChoice.SetActive(true);
    }
    /*브로커 제안 수락 - 제안 내용 확인*/
    public void ClickOfferOk2()
    {
        /*85 ~ 55*/
        accuracy = Random.Range(-35, 35);
        if (-5 < accuracy && accuracy < 5)
            accuracy = 0;
        N = 5;
        YesNoChoice.SetActive(true);
    }
    /*브로커 제안 수락 - 제안 내용 확인*/
    public void ClickOfferOk3()
    {
        /*45 ~ 30*/
        accuracy = Random.Range(-15, 0);
        accuracyMax = 30;

        N = 1;
        YesNoChoice.SetActive(true);
    }

    public void ClickYes()
    {
        offerPanelOk.SetActive(true);
        BrokerOfferOkFunc(accuracy, N);
    }
    public void ClickNo()
    {
        YesNoChoice.SetActive(false);
    }

    public void BrokerOfferFunc()
    {
        stockN = Random.Range(0, 10);
        offerPanelT.text = theStockManager.stockSc[stockN].stockNamePrice[stockN, 0].ToString() + "회사에 관한 일급보안 내용이 있는데 관심이 있나?";

        offerOkT1.text = "$" + string.Format("{0}", (30000 * theStat.myAllMoney.ToString().Length * 9).ToString("n0"));
        offerOkT2.text = "$" + string.Format("{0}", (30000 * theStat.myAllMoney.ToString().Length * 5).ToString("n0"));
        offerOkT3.text = "$" + string.Format("{0}", (30000 * theStat.myAllMoney.ToString().Length).ToString("n0"));
    }

    public void BrokerOfferOkFunc(int accuracy, int N)
    {
        int defaultMoney = 30000 * theStat.myAllMoney.ToString().Length;

        if (theStat.myAllMoney >= defaultMoney * N)
        {
            theStat.myAllMoney -= defaultMoney * N;
            int badGood = Random.Range(0, 2);
            if (badGood == 0)
            {
                if (accuracy > 0)
                    accuracy *= -1;
                /*안좋은 소식*/
                offerPanelOkT.text = theStockManager.stockSc[stockN].stockNamePrice[stockN, 0].ToString() + "회사에서 " + offerThema[Random.Range(0, 5), badGood]
                                        + "\n(다음달 1일 적용 예정)";
                offerAccuracyT.text = "\n정확도 :" + ((accuracy - accuracyMax) * -1).ToString() + "%";
                if (Random.Range(0, 100) < ((accuracy - accuracyMax) * -1))
                    rightEffect = true;
                else
                    rightEffect = false;
                StartCoroutine(BrokerMinOfferEffect());
            }
            else
            {
                if (accuracy < 0)
                    accuracy *= -1;
                /*좋은 소식*/
                offerPanelOkT.text = theStockManager.stockSc[stockN].stockNamePrice[stockN, 0].ToString() + "회사에서 " + offerThema[Random.Range(0, 5), badGood]
                                        + "\n(다음달 1일 적용 예정)";
                offerAccuracyT.text = "\n정확도 :" + (accuracy + accuracyMax).ToString() + "%";
                if (Random.Range(0, 100) < accuracy + accuracyMax)
                    rightEffect = true;
                else
                    rightEffect = false;
                StartCoroutine(BrokerPlsOfferEffect());
            }
        }
        else
        {
            offerPanelOkT.text = "돈이 부족하구만,,, 그럼 20000~";
        }
    }

    void Start()
    {
        theDate = FindObjectOfType<DateManager>();
        theStat = FindObjectOfType<PlayerStatManager>();
        theStockManager = FindObjectOfType<StockManager>();
        M = Random.Range(1, 2);
        D = Random.Range(1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (M == theDate.month && !opened)
            StartCoroutine(BrokerCoroutine());

        else if (opened)
            brokerShow.SetActive(false);
    }

    IEnumerator BrokerCoroutine()
    {
        BrokerShowFunc(M);
        if (D + 5 <= theDate.day)
            opened = true;
        /*브로커가 나타나고 1~2개월 후에 다시 새로운 월을 계산*/
        yield return new WaitUntil(()=> theDate.month == M+Random.Range(1,3));

        /* 1~3개월 내에 랜덤으로 배정*/
        if (theDate.month+1 % 13 == 0)
            M = Random.Range(1, 4);
        else
            M = Random.Range(theDate.month+1, theDate.month+4);
        D = Random.Range(1, 20);
        opened = false;
    }
    IEnumerator BrokerMinOfferEffect()
    {
        yield return new WaitUntil(() => theDate.day == 1);
        //Debug.Log("브로커 정보 영향 시작~");ㅋ
        theStockManager.stockSc[stockN].NewsEffectActivate(-0.35f, accuracy, rightEffect);
        
        yield return new WaitUntil(() => theDate.hour == 4);
        theStockManager.stockSc[stockN].NewsEffectNotActivate();
        //Debug.Log("브로커 정보 영향 끝");
    }

    IEnumerator BrokerPlsOfferEffect()
    {
        yield return new WaitUntil(() => theDate.day == 1);
        //Debug.Log("브로커 정보 영향 시작~");
        theStockManager.stockSc[stockN].NewsEffectActivate(0.35f, accuracy, rightEffect);
        
        yield return new WaitUntil(() => theDate.hour == 4);
        theStockManager.stockSc[stockN].NewsEffectNotActivate();
        //Debug.Log("브로커 정보 영향 끝!");
    }
}
