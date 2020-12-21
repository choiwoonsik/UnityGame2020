using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoBuilding : MonoBehaviour
{
    public GameObject flower;
    public GameObject bench;
    public GameObject streetLamp;

    public GameObject panel;

    private bool isFlower = false;
    private bool isBench = false;
    private bool isStreetLamp = false;

    private bool flag = false;

    public void BuildingOnClick() {
        flag = !flag;
        if (flag) {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }

    public void FlowerOnClick() {
        isFlower = !isFlower;
        if (isFlower)
        {
            flower.SetActive(true);
        }
        else {
            flower.SetActive(false);
        }
    }

    public void BenchOnClick() {
        isBench = !isBench;
        if (isBench)
        {
            bench.SetActive(true);
        }
        else {
            bench.SetActive(false);
        }
    }

    public void StreetLampOnClick()
    {
        isStreetLamp = !isStreetLamp;
        if (isStreetLamp)
        {
            streetLamp.SetActive(true);
        }
        else
        {
            streetLamp.SetActive(false);
        }
    }
}
