using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LandmarksHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public BuildingStruct[] Buildings;

    public int[] countryAttractivePoint;
    public int allCountryAttractivePoint = 0;

    private int selectedPanel;
    private int selectedType; //0은 랜드마크 1은 빌딩

    private PlayerStatManager theStat;
    private NoticeManager theNotice;
    private GetCompanyManager theGet;
    private DateManager theDate;

    public GameObject detailPanel;
    public TextMeshProUGUI buildingNameText;
    public TextMeshProUGUI buildingDetailText;
    public TextMeshProUGUI buildingLocationText;
    public TextMeshProUGUI buildingPriceText;
    public TextMeshProUGUI buildingRevenueText;
    public TextMeshProUGUI buildingConditionText;

    public GameObject managePanel;
    public GameObject buyPanel;
    public GameObject chooseBuildingPanel;
    public GameObject maintenancePanel;
    public TextMeshProUGUI maintenanceText;
    public GameObject ResellPanel;
    public TextMeshProUGUI resellText;
    public GameObject changeDesignPanel;
    public TextMeshProUGUI changeDesignText;
    public GameObject chooseOutteriorPanel;
    public Button chooseOutteriorDefaultButton;
    public GameObject changeNamePanel;
    public TextMeshProUGUI placeHolder;
    public TextMeshProUGUI inputNameText;

    public Button chooseBuildingDefaultButton;

    public GameObject cleaningEffect;
    public GameObject congratEffect;
    public GameObject floatingText;

    private float tmpPrice;
    private int checkBuildPrice;
    private int SynergyPoint = 0;

    void Start()
    {
        theStat = FindObjectOfType<PlayerStatManager>();
        theNotice = FindObjectOfType<NoticeManager>();
        theGet = FindObjectOfType<GetCompanyManager>();
        theDate = FindObjectOfType<DateManager>();

        countryAttractivePoint = new int[5];
        for (int i = 0; i < 5; i++)
            countryAttractivePoint[i] = 0;

        allCountryAttractivePoint = 0;
        CalcAllBuildingsAttractivePoint();
        CalcCountryAttractivePoint();

        for (int i=0; i<Buildings.Length; i++)
        {
            if (i >= 0 && i <= 3){
                Buildings[i].country = "fra";
            }
            else if (i >= 4 && i <= 7){
                Buildings[i].country = "ger";
            }
            else if (i >= 8 && i <= 11){
                Buildings[i].country = "aus";
            }
            else if (i >= 12 && i <= 15){
                Buildings[i].country = "kor";
            }
            else if (i >= 16 && i <= 19){
                Buildings[i].country = "usa";
            }
        }
    }

    void Update()
    {
        allCountryAttractivePoint = 1;
        for (int i = 0; i < 5; i++)
            countryAttractivePoint[i] = 0;
        CalcAllBuildingsAttractivePoint();
        CalcCountryAttractivePoint();
    }
    
    public void CalcAllBuildingsAttractivePoint()
    {
        for (int i = 0; i < Buildings.Length; i++)
        {
            if (Buildings[i].has)
            {
                // 1만달러 건물이면 2점
                // 10만달러 건물이 최고점 20점
                if ((Buildings[i].buildingPrice * 2) / 10000 >= 20)
                {
                    checkBuildPrice = 20;
                }
                else
                    checkBuildPrice = Buildings[i].buildingPrice * 2 / 10000 + 1;

                if (Buildings[i].hasSynergy)
                    SynergyPoint = 30;
                else
                    SynergyPoint = 0;

                if (Buildings[i].buildingCondition <= 0.1f)
                    Buildings[i].attractivePoint = 0;
                else
                {
                    Buildings[i].attractivePoint = (int)(50 * Buildings[i].buildingCondition) /* 50% */+
                                                   checkBuildPrice /* 20% */ +
                                                   SynergyPoint /* 30% */;   
                }
            }
            else
                Buildings[i].attractivePoint = 0;
        }
    }

    public void CalcCountryAttractivePoint()
    {
        for (int i = 0; i < Buildings.Length; i++)
        {
            if (i >= 1 && i <= 3)
            {
                if(Buildings[i].has)
                    countryAttractivePoint[0] += Buildings[i].attractivePoint;
            }
            else if (i >= 4 && i <= 7)
            {
                if (Buildings[i].has)
                    countryAttractivePoint[1] += Buildings[i].attractivePoint;
            }
            else if (i >= 8 && i <= 11)
            {
                if (Buildings[i].has)
                    countryAttractivePoint[2] += Buildings[i].attractivePoint;
            }
            else if (i >= 12 && i <= 15)
            {
                if (Buildings[i].has)
                    countryAttractivePoint[3] += Buildings[i].attractivePoint;
            }
            else if (i >= 16 && i <= 19)
            {
                if (Buildings[i].has)
                    countryAttractivePoint[4] += Buildings[i].attractivePoint;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            allCountryAttractivePoint += countryAttractivePoint[i];
        }
    }

    public void ShowBuyPanel() {
        buyPanel.SetActive(true);
    }

    public void ShowBuyPanelOK() {

        buyPanel.SetActive(false);
        detailPanel.SetActive(true);
        chooseBuildingPanel.SetActive(true);
        if (!Buildings[selectedPanel].has)
        {
            Color color = Buildings[selectedPanel].image.GetComponent<Image>().color;
            color.a = 0.1f;
            Buildings[selectedPanel].image.GetComponent<Image>().color = color;
        }
        BuyChooseBuilding(0);
        chooseBuildingDefaultButton.Select();
    }

    public void BuyChooseBuilding(int index)
    {
        string path = "Building/building" + index.ToString();
        Sprite newSprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        Buildings[selectedPanel].image.GetComponent<Image>().sprite = newSprite;
        Color color = Buildings[selectedPanel].image.GetComponent<Image>().color;
        color.a = 1.0f;
        Buildings[selectedPanel].image.GetComponent<Image>().color = color;
    }

    public void BuyChooseBuildingOK()
    {
        if(theStat.myAllMoney >= Buildings[selectedPanel].buildingPrice)
        {
            theStat.myAllMoney -= Buildings[selectedPanel].buildingPrice;
            Buildings[selectedPanel].has = true;
            chooseBuildingPanel.SetActive(false);
            detailPanel.SetActive(false);
            theNotice.NotificationAppear("건물 구매 완료!");
            var clone = Instantiate(congratEffect, Buildings[selectedPanel].transform);
        }
        else
        {
            theNotice.NotificationAppear("건물을 사기위한 돈이 부족합니다.");
        }
    }

    public void ShowDetail(int index) {
        ClosePanel();
        selectedPanel = index;
        detailPanel.SetActive(true);

        try
        {
            Vector3 position = Buildings[index].image.transform.position;
            detailPanel.transform.position = new Vector3(position.x + 1500, position.y + 600, position.z);

            buildingNameText.text = Buildings[index].buildingName;
            buildingDetailText.text = Buildings[index].buildingDetail;
            buildingLocationText.text = "위치 : " + Buildings[index].buildingLocation;

            if (Buildings[index].type == "b")
            {
                buildingPriceText.text = "가격 : " + "$ " + string.Format("{0}", Buildings[index].buildingPrice.ToString("n0"));
                buildingRevenueText.text = "매달 예상 월세 : $ " + string.Format("{0}", Buildings[index].monthlyRevenue.ToString("n0"));
            }
            else
            {
                buildingPriceText.text = "비매품입니다.";
                buildingRevenueText.text = "매달 예상 수익 : $ " + string.Format("{0}", Buildings[index].monthlyRevenue.ToString("n0"));
            }

            if (!Buildings[index].has)
            {
                buildingConditionText.text = "";
                buildingDetailText.text = "";
            }
            else if (Buildings[index].type == "b")//내가 가지고 있는 건물
            {
                buildingConditionText.text = "건물 상태 : " + (int)(Buildings[index].buildingCondition * 100) + "/100";
                if (Buildings[index].stockIn)
                {
                    buildingDetailText.text = "기업 주식 정보 제공 미완성";
                    //주식 정보에 대해서 알려주기
                }
                else
                {
                    buildingDetailText.text = "기업이 입주되지 않았습니다. 매월 10-17일 기업입주가 가능합니다.";
                }
            }

            Color color;

            for (int i = 0; i < Buildings.Length; i++)
            {
                if (!Buildings[i].has)
                {
                    color = Buildings[i].image.GetComponent<Image>().color;
                    color.a = 0.1f;
                    Buildings[i].image.GetComponent<Image>().color = color;
                }
            }

            if (!Buildings[selectedPanel].has && Buildings[selectedPanel].type == "b")
            {
                //안산거면 구매
                ShowBuyPanel();
            }
            else if (Buildings[selectedPanel].has && Buildings[selectedPanel].type == "b")
            {
                //산거면 관리
                managePanel.SetActive(true);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void ClosePanel() {

        detailPanel.SetActive(false);
        buyPanel.SetActive(false);
        chooseBuildingPanel.SetActive(false);
        changeDesignPanel.SetActive(false);
        ResellPanel.SetActive(false);
        maintenancePanel.SetActive(false);
        chooseOutteriorPanel.SetActive(false);
        managePanel.SetActive(false);
        changeNamePanel.SetActive(false);
        theGet.goStop = false;

        if (!Buildings[selectedPanel].has)
        {
            Color color = Buildings[selectedPanel].image.GetComponent<Image>().color;
            color.a = 0.1f;
            Buildings[selectedPanel].image.GetComponent<Image>().color = color;
        }
    }

    public void ChangeDesignOnClick()
    {
        detailPanel.SetActive(false);
        changeDesignPanel.SetActive(true);
        tmpPrice = Buildings[selectedPanel].buildingPrice * 0.05f;
        changeDesignText.text = "$ " + string.Format("{0}", tmpPrice.ToString("n0"));
    }

    public void ChangeDesignOK()
    {
        if (theStat.myAllMoney >= tmpPrice)//돈이 충분하다
        {
            theStat.myAllMoney -= tmpPrice;
            managePanel.SetActive(false);
            changeDesignPanel.SetActive(false);
            chooseOutteriorPanel.SetActive(true);
            BuyChooseBuilding(0);
            chooseOutteriorDefaultButton.Select();
        }
        else
        {
            theNotice.NotificationAppear("디자인 변경을 위한 돈이 부족합니다");
            changeDesignPanel.SetActive(false);
            detailPanel.SetActive(true);
        }
    }

    /* 건물 되팔기 */
    public void ResellOnClick()
    {
        detailPanel.SetActive(false);
        ResellPanel.SetActive(true);
        tmpPrice = Buildings[selectedPanel].buildingPrice * 0.7f * Buildings[selectedPanel].buildingCondition;
        if (tmpPrice < 0) tmpPrice = 0.0f;
        resellText.text = "$ " + string.Format("{0}", tmpPrice.ToString("n0"));
    }

    //주식이 들어있는 경우 건물을 되팔면 어떻게 처리할 것인가?
    public void ResellOK()
    {
        tmpPrice = Buildings[selectedPanel].buildingPrice * 0.7f * Buildings[selectedPanel].buildingCondition;

        if (Buildings[selectedPanel].stockIn)
        {
            theNotice.NotificationAppear("건물에 주식이 입주되어있지 않는 경우만 되팔 수 있습니다");
            ResellPanel.SetActive(false);
        }
        else
        {
            Buildings[selectedPanel].has = false;
            Buildings[selectedPanel].buildingCondition = 1.0f;
            theStat.myAllMoney += tmpPrice;
            ClosePanel();
            theNotice.NotificationAppear("건물을 되팔았습니다");
        }
    }

    /*건물 유지 보수*/
    public void MaintenanceOnClick()
    {
        detailPanel.SetActive(false);
        maintenancePanel.SetActive(true);
        tmpPrice = Buildings[selectedPanel].buildingPrice * 0.1f;
        maintenanceText.text = "$ " + string.Format("{0}", tmpPrice.ToString("n0"));
    }

    public void MaintenanceOK()
    {
        tmpPrice = Buildings[selectedPanel].buildingPrice * 0.1f;

        if (theStat.myAllMoney >= tmpPrice)//돈이 충분하다
        {
            theStat.myAllMoney -= tmpPrice;
            managePanel.SetActive(false);
            maintenancePanel.SetActive(false);
            theNotice.NotificationAppear("건물이 새것처럼 변했습니다!");
            Buildings[selectedPanel].buildingCondition = 1.0f;

            var clone = Instantiate(cleaningEffect, Buildings[selectedPanel].transform);
        }
        else
        {
            theNotice.NotificationAppear("유지보수를 위한 돈이 부족합니다");
            maintenancePanel.SetActive(false);
            detailPanel.SetActive(true);
        }
    }

    /*건물 이름 변경*/
    public void ShowChangeNamePaenl()
    {
        detailPanel.SetActive(false);
        changeNamePanel.SetActive(true);
        changeNamePanel.SetActive(false);
        changeNamePanel.SetActive(true);
        placeHolder.text = Buildings[selectedPanel].buildingName;
        inputNameText.text = null;
    }

    public void ChangeNameOK()
    {
        if(inputNameText.text.Length <= 0 || inputNameText.text.Length > 10)
        {
            theNotice.NotificationAppear("건물 이름은 1자 이상, 10자 이내로 작성해주세요");
        }
        else
        {
            Buildings[selectedPanel].buildingName = inputNameText.text;
            changeNamePanel.SetActive(false);
            theNotice.NotificationAppear("건물 이름이 변경되었습니다");
        }
    }

    //기업 입주 후 시너지 체크
    public void SynergyCheck(int buildN, int stockN)
    {
        try
        {
            bool flag = false;
            //프랑스 빌딩, 코스메틱 주식
            if (buildN >= 1 && buildN <= 3 && stockN >= 3 && stockN <= 5)
            {
                flag = true;
            }
            //독일 빌딩, 제약 주식
            else if (buildN >= 5 && buildN <= 7 && stockN >= 12 && stockN <= 14)
            {
                flag = true;
            }
            //호주 빌딩, 푸드 주식
            else if (buildN >= 9 && buildN <= 11 && stockN >= 9 && stockN <= 11)
            {
                flag = true;
            }
            //한국 빌딩, 아이티 주식
            else if (buildN >= 13 && buildN <= 15 && stockN >= 0 && stockN <= 2)
            {
                flag = true;
            }
            //미국 빌딩, 엔터 주식
            else if (buildN >= 17 && buildN <= 19 && stockN >= 6 && stockN <= 8)
            {
                flag = true;
            }
            if (flag)
            {
                var clone = Instantiate(floatingText, Buildings[buildN].transform);
                clone.GetComponent<FloatingText>().text.color = new Color(219 / 255.0f, 99 / 255.0f, 0.0f);
                clone.GetComponent<FloatingText>().text.fontSizeMax = 100;
                clone.GetComponent<FloatingText>().SetText("기업과 지역 이미지 일치!");
                StartCoroutine(DelayCoroutine(buildN, Buildings[buildN].country));
                Buildings[buildN].hasSynergy = true;
            }
            //시너지 효과 제거하기
            else if (Buildings[buildN].hasSynergy)
            {
                Buildings[buildN].hasSynergy = false;
                //매력 지수 반감하기
            }
            ClosePanel();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    IEnumerator DelayCoroutine(int buildN, String country)
    {
        yield return new WaitForSeconds(1.5f);
        var clone2 = Instantiate(floatingText, Buildings[buildN].transform);
        clone2.GetComponent<FloatingText>().text.color = new Color(219 / 255.0f, 99 / 255.0f, 0.0f);
        clone2.GetComponent<FloatingText>().text.fontSizeMax = 100;
        clone2.GetComponent<FloatingText>().SetText("시너지 효과 발생!");

        //매력지수 증가

        if(buildN > 0 && Buildings[buildN-1].type == "b" && Buildings[buildN - 1].hasSynergy)
        {
            var clone3 = Instantiate(floatingText, Buildings[buildN-1].transform);
            clone3.GetComponent<FloatingText>().text.color = new Color(219 / 255.0f, 99 / 255.0f, 0.0f);
            clone3.GetComponent<FloatingText>().text.fontSizeMax = 100;
            clone3.GetComponent<FloatingText>().SetText("시너지 효과 발생!");

            //매력 지수 증가
        }
        if (buildN > 1 && Buildings[buildN - 2].type == "b" && Buildings[buildN - 2].hasSynergy)
        {
            var clone4 = Instantiate(floatingText, Buildings[buildN - 2].transform);
            clone4.GetComponent<FloatingText>().text.color = new Color(219 / 255.0f, 99 / 255.0f, 0.0f);
            clone4.GetComponent<FloatingText>().text.fontSizeMax = 100;
            clone4.GetComponent<FloatingText>().SetText("시너지 효과 발생!");

            //매력 지수 증가
        }

        if (buildN < 19 && Buildings[buildN + 1].type == "b" && Buildings[buildN + 1].hasSynergy)
        {
            var clone5 = Instantiate(floatingText, Buildings[buildN+1].transform);
            clone5.GetComponent<FloatingText>().text.color = new Color(219 / 255.0f, 99 / 255.0f, 0.0f);
            clone5.GetComponent<FloatingText>().text.fontSizeMax = 100;
            clone5.GetComponent<FloatingText>().SetText("시너지 효과 발생!");

            //매력 지수 증가
        }

        if (buildN < 18 && Buildings[buildN + 2].type == "b" && Buildings[buildN + 2].hasSynergy)
        {
            var clone6 = Instantiate(floatingText, Buildings[buildN + 2].transform);
            clone6.GetComponent<FloatingText>().text.color = new Color(219 / 255.0f, 99 / 255.0f, 0.0f);
            clone6.GetComponent<FloatingText>().text.fontSizeMax = 100;
            clone6.GetComponent<FloatingText>().SetText("시너지 효과 발생!");

            //매력 지수 증가
        }

        int synergy_count = 0;

        //랜드마크 건설 체크
        for(int i=0; i<Buildings.Length; i++)
        {
            if(Buildings[i].type == "b" && Buildings[i].country == country && Buildings[i].has && Buildings[i].stockIn && Buildings[i].hasSynergy)
            {
                synergy_count++;
            }
        }

        if(synergy_count == 3)
        {
            int landmark_index;
            for(landmark_index = 0; landmark_index < Buildings.Length; landmark_index++)
            {
                if(Buildings[landmark_index].type == "l" && Buildings[landmark_index].country == country)
                {
                    break;
                }
            }

            //랜드마크 건설
            if (!Buildings[landmark_index].has)
            {
                Buildings[landmark_index].has = true;
                Color color = Buildings[landmark_index].image.GetComponent<Image>().color;
                color.a = 1.0f;
                Buildings[landmark_index].image.GetComponent<Image>().color = color;
                var clone7 = Instantiate(floatingText, Buildings[landmark_index].transform);

                clone7.GetComponent<FloatingText>().text.color = new Color(219 / 255.0f, 99 / 255.0f, 0.0f);
                clone7.GetComponent<FloatingText>().text.fontSizeMax = 100;
                clone7.GetComponent<FloatingText>().SetText("축! 랜드마크 건설");
            }
        }
    }
}
