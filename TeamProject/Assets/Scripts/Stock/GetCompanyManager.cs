using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetCompanyManager : MonoBehaviour
{
    private PlayerStatManager theStat;
    private StockManager theSellBuy;
    private NoticeManager theNotice;
    private LandmarksHandler theHandler;

    public GameObject putStockButton;
    public GameObject outStockButton;

    public GameObject GetCompanyChoiceP;
    public TextMeshProUGUI GetCompanyChoiceT;

    public GameObject OutCompanyChoiceP;
    public TextMeshProUGUI OutCompanyChoiceT;

    public GameObject ConfirmOkP;

    public GameObject[] BackGroundBuildingName;
    public TextMeshProUGUI[] stockName;
    public TextMeshProUGUI[] stockPrice;

    public TextMeshProUGUI stockInOutT;
    public bool StockInOut;

    public Hashtable stockTable = new Hashtable();
    public Hashtable priceTable = new Hashtable();

    private int buildN;
    private int stockN;
    private int stockN1;
    private int stockN2;
    private int stockN3;
    private double GetInPrice;
    public bool goStop;

    public int populationMax = 0;

    //기업 내보내기
    public void ClickOutStock()
    {
        if (!StockInOut)
        {
            OutCompanyChoiceP.SetActive(true);

            if (stockTable.ContainsKey(buildN))
            {
                OutCompanyChoiceT.text = "<건물 입주기업 관리>\n\n" + theSellBuy.stockSc[(int)stockTable[buildN]].stockNameT.text + "회사 를 건물에서 퇴출 시키겠습니까?"
                                            + "\n\n 퇴출시 보증금 반납 금액 > $" + priceTable[buildN].ToString() + "입니다.";
            }
        }
    }

    //내보내기 진행
    public void OutYes()
    {
        int outPay = int.Parse(priceTable[buildN].ToString());

        if(theStat.myAllMoney >= outPay)
        {
            theNotice.NotificationAppear(theSellBuy.stockSc[(int)stockTable[buildN]].stockNameT.text + "을 건물에서 퇴출시켰습니다.");
            theStat.myAllMoney -= outPay;

            theSellBuy.stockSc[(int)stockTable[buildN]].stockGetin = false;
            theHandler.Buildings[buildN].stockIn = false;
            BackGroundBuildingName[buildN].SetActive(false);

            if (stockTable.ContainsKey(buildN))
                stockTable.Remove(buildN);
            if (priceTable.ContainsKey(buildN))
                priceTable.Remove(buildN);

            populationMax -= 1;
            OutCompanyChoiceP.SetActive(false);
            checkInOut();
        }
        else
        {
            theNotice.NotificationAppear("퇴출을 위한 보증금 반납 비용이 부족합니다.");
            OutCompanyChoiceP.SetActive(false);
        }
    }

    //내보내기 취소
    public void OutNo()
    {
        theNotice.NotificationAppear("취소하였습니다.");
        OutCompanyChoiceP.SetActive(false);
    }

    //기업 고르기
    public void GetCompanyChoice(int _buildN)
    {
        buildN = _buildN;

        checkInOut();

        //주식이 없는경우
        if (theHandler.Buildings[buildN].has && !theHandler.Buildings[buildN].stockIn)
        {
            //주식넣을수있는 버튼 활성화
            Color color = putStockButton.GetComponent<Image>().color;
            color.a = 1f;
            putStockButton.GetComponent<Image>().color = color;

            Button button = putStockButton.GetComponent<Button>();
            button.interactable = true;
        }

        //주식이 있는경우
        else if (theHandler.Buildings[buildN].has && theHandler.Buildings[buildN].stockIn)
        {
            //주식뺄수있는 버튼 활성화
            Color color = putStockButton.GetComponent<Image>().color;
            color.a = 1f;
            putStockButton.GetComponent<Image>().color = color;

            Button button = putStockButton.GetComponent<Button>();
            button.interactable = true;
        }

        else
        {
            Color color = putStockButton.GetComponent<Image>().color;
            color.a = 0.1f;
            putStockButton.GetComponent<Image>().color = color;
            Button button = putStockButton.GetComponent<Button>();
            button.interactable = false;
        }
    }

    //기업 유치 창 열기
    public void ClickPutStock()
    {
        if (StockInOut)
        {
            checkEachBuilding();
            GetCompanyChoiceP.SetActive(true);
            GetCompanyChoiceT.text = "기업 유치를 위해 친히 와주셔서 감사합니다!!\n\n아래 3개의 기업 후보들 중에서 마음에 드시는 기업을 선택하여\n건물에 기업을 입주시킬 수 있습니다."
                                        + "\n\n< Tip. 기업으로부터 회사를 입주시키기위한 보증금을 일정금액 받으며, 기업을 바꾸거나, 뺄 경우 보증금을 전액반환하여야 합니다. >";
            int count = 0;
            while(count < 100)
            {
                int i = Random.Range(0, 5);
                count++;
                if (!theSellBuy.stockSc[i].stockGetin)
                {
                    stockN1 = i;
                    break;
                }
            }

            count = 0;
            while (count < 100)
            {
                int i = Random.Range(5, 10);
                count++;
                if (!theSellBuy.stockSc[i].stockGetin)
                {
                    stockN2 = i;
                    break;
                }
            }

            count = 0;
            while (count < 100)
            {
                int i = Random.Range(10, 15);
                count++;
                if (!theSellBuy.stockSc[i].stockGetin)
                {
                    stockN3 = i;
                    break;
                }
            }
        }
    }

    //입주기업 고르기
    public void GetCompanyClick1()
    {
        if (theSellBuy.stockSc[stockN1].stockGetin)
        {
            stockName[0].text = "입주 기업 없음";
            stockPrice[0].text = " - ";
        }
        ConfirmOkP.SetActive(true);
        stockName[0].text = theSellBuy.stockSc[stockN1].stockNameT.text;
        stockPrice[0].text = "기업 입주 보증금 $"+string.Format("{0}", (theSellBuy.stockSc[stockN1].thisStockPrice.ToString().Length * 5000).ToString("n0"));
        GetInPrice = theSellBuy.stockSc[stockN1].thisStockPrice.ToString().Length * 5000;
        stockN = stockN1;
    }
    public void GetCompanyClick2()
    {
        if (theSellBuy.stockSc[stockN2].stockGetin)
        {
            stockName[0].text = "입주 기업 없음";
            stockPrice[0].text = " - ";
        }
        ConfirmOkP.SetActive(true);
        stockName[1].text = theSellBuy.stockSc[stockN2].stockNameT.text;
        stockPrice[1].text = "기업 입주 보증금 $" + string.Format("{0}", (theSellBuy.stockSc[stockN2].thisStockPrice.ToString().Length * 8000).ToString("n0"));
        GetInPrice = theSellBuy.stockSc[stockN2].thisStockPrice.ToString().Length * 8000;
        stockN = stockN2;
    }
    public void GetCompanyClick3()
    {
        if (theSellBuy.stockSc[stockN3].stockGetin)
        {
            stockName[0].text = "입주 기업 없음";
            stockPrice[0].text = " - ";
        }
        ConfirmOkP.SetActive(true);
        stockName[2].text = theSellBuy.stockSc[stockN3].stockNameT.text;
        stockPrice[2].text = "기업 입주 보증금 $" + string.Format("{0}", (theSellBuy.stockSc[stockN3].thisStockPrice.ToString().Length * 11000).ToString("n0"));
        GetInPrice = theSellBuy.stockSc[stockN3].thisStockPrice.ToString().Length * 11000;
        stockN = stockN3;
    }

    public void ConfirmOk()
    {
        theStat.myAllMoney += GetInPrice;

        stockTable.Add(buildN, stockN);
        priceTable.Add(buildN, GetInPrice);

        theSellBuy.stockSc[stockN].stockGetin = true;
        theHandler.Buildings[buildN].stockIn = true;

        ConfirmOkP.SetActive(false);
        GetCompanyChoiceP.SetActive(false);

        BackGroundBuildingName[buildN].SetActive(true);
        BackGroundBuildingName[buildN].GetComponentInChildren<TextMeshProUGUI>().text = theSellBuy.stockSc[stockN].stockNameT.text.ToString();

        theNotice.NotificationAppear("\t\t\t\t(축)! 기업을 건물에 입주시키셨습니다 !(축)");
        //인구 제어값 증가, 분야는 아직 미설정
        populationMax += 1;
        checkInOut();

        //체크해서 시너지효과 발생
        theHandler.SynergyCheck(buildN, stockN);
    }
    public void ConfirmNot()
    {
        ConfirmOkP.SetActive(false);
    }

    public void ExitChice()
    {
        ConfirmOkP.SetActive(false);
        GetCompanyChoiceP.SetActive(false);

        theNotice.NotificationAppear("기업 입주 취소");
    }

    public void checkEachBuilding()
    {
        stockN = Random.Range(0, 5);

        for (int i = 0; i < 3; i++)
        {
            stockName[i].text = "Click!";
            stockPrice[i].text = "가격 -";
        }
    }

    public void checkInOut()
    {
        if (theHandler.Buildings[buildN].stockIn)
        {
            StockInOut = false;
            stockInOutT.text = "기업퇴출";
        }
        else if (!theHandler.Buildings[buildN].stockIn)
        {
            StockInOut = true;
            stockInOutT.text = "기업입주";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        theStat = FindObjectOfType<PlayerStatManager>();
        theSellBuy = FindObjectOfType<StockManager>();
        theNotice = FindObjectOfType<NoticeManager>();
        theHandler = FindObjectOfType<LandmarksHandler>();
        StockInOut = true;
        stockInOutT.text = "기업입주";
    }
}
