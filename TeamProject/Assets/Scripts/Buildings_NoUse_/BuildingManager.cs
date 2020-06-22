using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* 모든 빌딩과 내 빌딩에 대한 정보를 가지고 있음, 구매/팔기/보수 등을 여기서 할 것 */
public class BuildingManagerNoUse : MonoBehaviour
{
    public GameObject prefab;

    public Building[] allBuildings;
    public MyBuilding[] myBuildings;
    public GameObject buyPanel;
    public GameObject managePanel;
    public GameObject resellPanel;
    public TextMeshProUGUI resellValueText;
    public GameObject maintenancePanel;
    public TextMeshProUGUI maintenanceValueText;
    /*레포트용 건물 월 거래비용 계산*/
    public StockReportManager theReport;

    [Header("상세 패널")]
    public GameObject detailPanel;
    public TextMeshProUGUI buildingNameText;
    public TextMeshProUGUI buildingDetailText;
    public TextMeshProUGUI buildingPriceText;
    public TextMeshProUGUI buildingRentText;

    private DateManager theDate;
    private NoticeManager theNotice;
    private PlayerStatManager theStat;
    private NewsManager theNews;
    private int temp;
    private int flag = 0;

    private int selectedPanel = -1;

    // Start is called before the first frame update
    void Start()
    {
        theDate = FindObjectOfType<DateManager>();
        theNotice = FindObjectOfType<NoticeManager>();
        theStat = FindObjectOfType<PlayerStatManager>();
        theNews = FindObjectOfType<NewsManager>();

        /*******************************
         * 테스트 하려고 넣어놈 */

        for (int i = 0; i < 5; i++)
        {
            allBuildings[0].conditionComplete[i] = true ;
            allBuildings[4].conditionComplete[i] = true;
            allBuildings[7].conditionComplete[i] = true;
        }

        allBuildings[9].conditionComplete[0] = true;
        allBuildings[1].conditionComplete[2] = true;
        /**********여기까지**************/
    }

    /* 테스트하기 위한 함수, 나중에 지울 예정 */
    public void StartEstate()
    {
        UpdateAllPanels();
    }

    /* 테스트하기 위한 함수, 나중에 지울 예정 */
    public void UpdateAllPanels()
    {
        for(int i=0; i<allBuildings.Length; i++)
        {
            if (allBuildings[i].buildingSold)
            {
                allBuildings[i].soldPanel.SetActive(true);
            }
            else
            {
                allBuildings[i].soldPanel.SetActive(false);
            }
            for(int j = 0; j < 5; j++)
            {
                if (allBuildings[i].conditionComplete[j])
                {
                    allBuildings[i].completePanel[j].SetActive(true);
                }
                else
                {
                    allBuildings[i].completePanel[j].SetActive(false);
                }
            }
        }
    }

    /* index번째 건물을 선택한다. 함수 이름이 쪼금 잘못됐지만 바꾸면 너무 바꿀게 많아서 일단 keep */
    public void BuyBuilding(int index)
    {
        selectedPanel = index;
        detailPanel.SetActive(true);
        buildingNameText.text = allBuildings[selectedPanel].buildingName;
        buildingDetailText.text = allBuildings[selectedPanel].buildingDetail;
        buildingPriceText.text = "가격 : $" + string.Format("{0}", allBuildings[selectedPanel].buildingPrice.ToString("n0"));
        buildingRentText.text = "월세 : $" + string.Format("{0}", allBuildings[selectedPanel].monthlyRent.ToString("n0"));
    }

    /* 구매 확인 버튼을 클릭할 때 실행되는 함수 */
    public void BuyYesButtonOnClick()
    {
        if (allBuildings[selectedPanel].buildingPrice <= theStat.myAllMoney) /* 충분한 돈이 있을 경우 */
        {
            if (allBuildings[selectedPanel].CheckAllConditionsComplete()) /* 모든 조건을 만족한 경우 */
            {
                allBuildings[selectedPanel].Sold();
                theStat.myAllMoney -= allBuildings[selectedPanel].buildingPrice;
                /*레포트용 건물 월 거래비용 계산*/
                theReport.nMonthUseEstateMoney += allBuildings[selectedPanel].buildingPrice;
                theNotice.NotificationAppear("건물 구매 완료! 건물 관리 탭에서 내 건물을 확인할 수 있어요.");

                /* 내 건물 탭에 들어갈 내용들을 초기화 시킨다 */
                myBuildings[selectedPanel].gameObject.SetActive(true);

                myBuildings[selectedPanel].buildingSold = true;
                myBuildings[selectedPanel].buildingNum = allBuildings[selectedPanel].buildingNum;
                myBuildings[selectedPanel].buildingName = allBuildings[selectedPanel].buildingName;
                myBuildings[selectedPanel].buildingDetail = allBuildings[selectedPanel].buildingDetail;
                myBuildings[selectedPanel].buildingPrice = allBuildings[selectedPanel].buildingPrice;
                myBuildings[selectedPanel].monthlyRent = allBuildings[selectedPanel].monthlyRent;

                myBuildings[selectedPanel].buildingNameText.text = allBuildings[selectedPanel].buildingNameText.text;
                myBuildings[selectedPanel].buildingDetailText.text = allBuildings[selectedPanel].buildingDetail;
                myBuildings[selectedPanel].buildingRentText.text = "월세 : $" + string.Format("{0}", allBuildings[selectedPanel].monthlyRent.ToString("n0"));
                myBuildings[selectedPanel].buildingDateText.text = "구매 날짜 : " + theDate.year + "년 " + theDate.month + "월 " + theDate.day + "일";
                myBuildings[selectedPanel].buildingCondition = 1.0f;
                myBuildings[selectedPanel].buildingConditionText.text = "건물 상태 : " + (int)(myBuildings[selectedPanel].buildingCondition * 100) + "/100";
                /* 여기까지 초기화 할 내용이 들어간다 */
            }
            else /* 조건을 다 만족시키지 못하는 경우 */
            {
                theNotice.NotificationAppear("건물을 사기 위한 조건을 만족시키세요.");
                buyPanel.SetActive(false);
            }
        }
        else /* 충분한 돈이 없는 경우 */
        {
            theNotice.NotificationAppear("건물을 사기 위한 돈이 부족합니다.");
            buyPanel.SetActive(false);
        }
    }

    /* 내 건물관리 탭에서 건물을 선택한 경우, 어떤 건물을 선택했는 지 인덱스를 저장해둔다 */
    public void MyBuildingsOnClick(int index)
    {
        managePanel.SetActive(true);
        selectedPanel = index;
    }

    /* 되팔기 버튼을 눌렀을 경우 되팔기를 확인하는 부분이 나온다 */
    public void ResellButtonOnClick()
    {
        managePanel.SetActive(false);
        resellPanel.SetActive(true);

        temp = (int)myBuildings[selectedPanel].ResellValue();

        resellValueText.text = "$" + string.Format("{0}", temp.ToString("n0"));
    }

    /* 되팔기 확인을 눌렀을 경우, 되팔기를 시작한다 */
    public void ResellOkButtonOnClick()
    {
        temp = (int)myBuildings[selectedPanel].ResellValue();
        theStat.myAllMoney += temp;
        /*레포트용 건물 월 거래비용 계산*/
        theReport.nMonthEarnEstateMoney += temp;
        theNotice.NotificationAppear("건물을 되팔았습니다. 부동산에서 다시 구매할 수 있습니다.");
        resellPanel.SetActive(false);

        /* 다시 부동산에서 살 수 있게 한다 */
        myBuildings[selectedPanel].gameObject.SetActive(false);
        myBuildings[selectedPanel].buildingSold = false;
        allBuildings[selectedPanel].buildingSold = false;
        allBuildings[selectedPanel].soldPanel.SetActive(false);
        allBuildings[selectedPanel].GetComponent<Button>().interactable = true;
    }

    //유지보수하기를 선택했습니다.
    public void MaintenanceButtonOnClick()
    {
        managePanel.SetActive(false);
        maintenancePanel.SetActive(true);
        //유지 보수 비용은 건물의 상태와 상관없이 가격의 0.1 입니다.
        temp = (int)(myBuildings[selectedPanel].buildingPrice * 0.1);
        maintenanceValueText.text = "$" + string.Format("{0}", temp.ToString("n0"));
    }

    //유지보수하기 확인버튼을 클릭했습니다.
    public void MaintenanceOkButtonOnClick()
    {

        if (theStat.myAllMoney >= temp) //돈이 있는 경우
        {
            //건물의 상태를 100으로 만듭니다.
            theStat.myAllMoney -= temp;
            /*레포트용 건물 월 거래비용 계산*/
            theReport.nMonthUseEstateMoney += temp;
            myBuildings[selectedPanel].buildingCondition = 1.0f;
            myBuildings[selectedPanel].buildingConditionText.text = "건물 상태 : 100/100";
            GameObject clone = Instantiate(prefab, myBuildings[selectedPanel].gameObject.transform);
            clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y - 600, clone.transform.position.z);
            clone.GetComponent<FloatingText>().text.color = Color.blue;
            clone.GetComponent<FloatingText>().text.fontSizeMax = 50;
            clone.GetComponent<FloatingText>().SetText("유지 보수 완료!");
            //theNotice.NotificationAppear("건물이 새것처럼 변했습니다! 주민들이 좋아합니다.");
            maintenancePanel.SetActive(false);
        }
        else
        {
            theNotice.NotificationAppear("유지 보수를 위한 돈이 부족합니다.");
            maintenancePanel.SetActive(false);
        }
    }

    //모든 빌딩에 대해서 조건을 만족시키는 지 확인합니다.
    public void UpdateAllConditions(int index)
    {
        for(int i=0; i < allBuildings.Length; i++)
        {
            if(!allBuildings[i].buildingSold) {
                allBuildings[i].UpdateCondition(index);
            }
        }
    }

    void Update()
    {
        //매월 10일 0시에 월세를 걷습니다.
        if(theDate.day == 10 && theDate.hour == 0 && flag == 0)
        {
            flag = 1;
            double totalRent = 0; //전체 월세 계산
            for(int i=0; i<myBuildings.Length; i++)
            {
                if (myBuildings[i].buildingSold)
                {
                    totalRent += myBuildings[i].MonthlyRentValue();
                }
            }

            theNotice.NotificationAppear("매월 10일 월세를 걷습니다." + totalRent);
            /*레포트용 건물 월 거래비용 계산*/
            theReport.nMonthEarnEstateMoney += totalRent;
            theStat.myAllMoney += totalRent;
        }
        if(theDate.day == 10 && theDate.hour == 1)
        {
            flag = 0;
        }

        /* 시간이 지남에 따라 건물이 점점 낡습니다.. */
        if (DateManager.activated)
        {
            for(int i = 0; i < myBuildings.Length; i++)
            {
                if (myBuildings[i].buildingSold)
                {
                    myBuildings[i].currentTime -= Time.deltaTime;

                    if(myBuildings[i].currentTime < 0.0f)
                    {
                        myBuildings[i].currentTime = myBuildings[i].updateTime; 
                        if (myBuildings[i].buildingCondition > 0.0f)
                        {  //각 빌딩의 updateTime이 지날때마다 1/100씩 낡아집니다.
                            myBuildings[i].buildingCondition -= 0.01f;
                            myBuildings[i].buildingConditionText.text = "건물 상태 : " + (int)(myBuildings[i].buildingCondition * 100) + "/100";
                        }
                        if (myBuildings[i].buildingCondition < 0.1f)
                        {
                            myBuildings[i].buildingRentText.text = "월세 : $" + string.Format("{0}", myBuildings[i].MonthlyRentValue().ToString("n0"));
                            //10/100이하일 경우 지속적으로 주민으로부터 쪽지를 받습니다.
                            int n = DatabaseManager.buildingConditionNoteList.Count;
                            SimpleNote newNote = DatabaseManager.buildingConditionNoteList[Random.Range(0, n)];
                            newNote.SetWhom(myBuildings[i].buildingNameText.text + " 주민");
                            theNews.SetNewNote(newNote);

                            SimpleNote newNote2 = new SimpleNote(myBuildings[i].buildingNameText.text + "의 열악한 건물 상태에 방을 빼겠다는 주민들이 늘어나고 있습니다. 관계자의 말에 따르면 늘어나는 주민 불만에 월세 수입이 줄어들었다고 합니다.");
                            newNote2.SetWhom("동네 신문");
                            theNews.SetNewNote(newNote2);
                        }
                    }
                }
            }
        }
    }
}
