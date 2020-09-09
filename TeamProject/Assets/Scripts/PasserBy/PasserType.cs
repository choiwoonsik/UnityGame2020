using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasserType : MonoBehaviour
{
    //0~8
    public int nomalPerson;
    //6
    public int foodPerson;
    //9~13
    public int oldPerson;
    //14~15
    public int businessPerson;
    //16
    public int enterPerson;
    //17
    public int richWomanPerson;

    public int[,] personType = new int[5,6];

    public TextMeshProUGUI showCitizenTypeT;
    int all = 0;
    public int checkAll = 0;

    private ChangeCountryName theCountry;
    private PasserByManager thePasser;


    void Start()
    {
        thePasser = FindObjectOfType<PasserByManager>();
        theCountry = FindObjectOfType<ChangeCountryName>();

        for(int country = 0; country < 5; country++)
        {
            for (int i = 0; i < 6; i++)
            {
                personType[country, i] = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        showPersonTypeCount((int)theCountry.countrySlider.value-1);

        thePasser.CitizenCountT.text = "시민 수 >> " + thePasser.citizenCount * 8 + "명";
    }

    public void plusPersonTypeCount(int country, int _type)
    {
        int type;
        switch (_type){
            case 0:case 1: case 2: case 4: case 5: case 7: case 8: case 15: case 16:
                type = 5;
                personType[country, type]++;
                break;

            //영화, 드라마
            case 3:
                type = 4;
                personType[country, type]++;
                break;

            //요식업 
            case 6:
                type = 3;
                personType[country, type]++;
                break;

            //제약
            case 9: case 10: case 11: case 12: case 13:
                type = 1;
                personType[country, type]++;
                break;

            //직장인
            case 14:
                type = 0;
                personType[country, type]++;
                break;

            //패션화장품
            case 17:
                type = 2;
                personType[country, type]++;
                break;
        }
    }

    public void minusPersonTypeCount(int country, int _type)
    {
        int type = 0;
        switch (_type)
        {
            case 0:
            case 1:
            case 2:
            case 4:
            case 5:
            case 7:
            case 8:
            case 15:
            case 16:
                type = 5;
                personType[country, type]--;
                break;

            //영화, 드라마
            case 3:
                type = 4;
                personType[country, type]--;
                break;

            //요식업 
            case 6:
                type = 3;
                personType[country, type]--;
                break;

            //제약
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
                type = 1;
                personType[country, type]--;
                break;

            //직장인
            case 14:
                type = 0;
                personType[country, type]--;
                break;

            //패션화장품
            case 17:
                type = 2;
                personType[country, type]--;
                break;
        }
    }

    public void showPersonTypeCount(int country)
    {
        showCitizenTypeT.text = "IT > " + personType[country, 0]*8 + "/" + all.ToString() + "명\n" +
                                "제약 > " + personType[country, 1]*8 + "/" + all.ToString() + "명\n" +
                                "패션&화장품 > " + personType[country, 2]*8 + "/" + all.ToString() + "명\n" +
                                "요식업 > " + personType[country, 3]*8 + "/" + all.ToString() + "명\n" +
                                "엔터 > " + personType[country, 4]*8 + "/" + all.ToString() + "명\n" +
                                "일반인 > " + personType[country, 5]*8 + "/" + all.ToString() + "명";
    }
}
