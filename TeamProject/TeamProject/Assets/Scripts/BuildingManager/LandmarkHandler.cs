using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LandmarkHandler : BuildingStruct
{
    void Start()
    {
        Color color = image.GetComponent<Image>().color;
        color.a = 0.1f;
        image.GetComponent<Image>().color = color;
        type = "l";
    }
}
