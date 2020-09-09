using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasserType : MonoBehaviour
{
    public int[] countryPerson = new int[5];
    public TextMeshProUGUI showCitizenTypeT;
    private ChangeCountryName theCountry;
    private PasserByManager thePasser;

    void Start()
    {
        thePasser = FindObjectOfType<PasserByManager>();
        theCountry = FindObjectOfType<ChangeCountryName>();

        for(int country = 0; country < 5; country++)
        {
            countryPerson[country] = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        showPersonCount((int)theCountry.countrySlider.value-1);

        thePasser.CitizenCountT.text = "시민 수 >> " + thePasser.citizenCount * 8 + "명";
    }

    public void plusPersonCount(int country)
    {
        countryPerson[country]++;
    }

    public void minusPersonCount(int country)
    {
        countryPerson[country]--;
    }

    public void showPersonCount(int country)
    {
        showCitizenTypeT.text = "국가 인구수 > " + countryPerson[country]*8 + "명\n";
    }
}
