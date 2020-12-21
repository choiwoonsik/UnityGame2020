using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameObject panels; //패널 전체

    public GameObject[] panel;

    public Toggle firstToggle;

    private bool activated = false;
    private int selectedIndex = 0;

    public void SettingOnClick()
    {
        if (!activated)
        {
            activated = true;
            panels.SetActive(true);
            for(int i = 0; i < panel.Length; i++)
            {
                panel[i].SetActive(false);
            }

            /* 예전 결과와 상관없이 항상 첫번째 탭이 먼저 보이도록 한다.*/
            panel[0].SetActive(true);
            selectedIndex = 0;
            firstToggle.isOn = true;
        }
        else
        {
            activated = false;
            panels.SetActive(false);
        }
    }

    /* 탭을 선택함에 따라 다른 패널이 보이도록 한다. */
    public void TabOnClick(int index)
    {
        if (index == 0)
        {
            panel[0].SetActive(true);
            panel[1].SetActive(false);
            panel[2].SetActive(false);
        }
        else if(index == 1)
        {
            panel[0].SetActive(false);
            panel[1].SetActive(true);
            panel[2].SetActive(false);
        }
        else
        {
            panel[0].SetActive(false);
            panel[1].SetActive(false);
            panel[2].SetActive(true);
        }

        selectedIndex = index;
    }
}
