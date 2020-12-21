using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasserType : MonoBehaviour
{
    public int[] personType = new int[5];

    public TextMeshProUGUI showCitizenTypeT;
    private ChangeCountryName theCountry;
    private PasserByManager thePasser;

    void Start()
    {
        thePasser = FindObjectOfType<PasserByManager>();
        theCountry = FindObjectOfType<ChangeCountryName>();

        for(int country = 0; country < 5; country++)
        {
            personType[country] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int all = 0;
        for (int i = 0; i < personType.Length; i++)
            all += personType[i];
        StartCoroutine(ShowCoroutine((int)theCountry.countrySlider.value - 1, all));
    }

    public void plusPersonTypeCount(int country)
    {
        personType[country]++;
    }

    public void minusPersonTypeCount(int country)
    {
        if (personType[country] > 0)
            personType[country]--;
    }

    public void showPersonTypeCount(int all)
    {
        thePasser.CitizenCountT.text = "world 인구수 " + (all * 7) + "명";
    }

    IEnumerator ShowCoroutine(int country, int all)
    {
        yield return new WaitForSeconds(2f);
        showCitizenTypeT.text = "인구 " + (personType[country] * 7) + "명\n";
        showPersonTypeCount(all);
    }
}
