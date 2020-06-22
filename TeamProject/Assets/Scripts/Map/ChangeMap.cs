using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeMap : MonoBehaviour
{
    public Slider ChangeSlider;
    public Scrollbar MapSlider;

    public void ValueChanged()
    {
        int n = (int)ChangeSlider.value;
        switch (n)
        {
            case 1: MapSlider.value = 0.108f; break; //프랑스
            case 2: MapSlider.value = 0.318f; break; //미국
            case 3: MapSlider.value = 0.524f; break; //호주
            case 4: MapSlider.value = 0.74f; break; //한국
            case 5: MapSlider.value = 0.949f; break; //독일
        }
    }
}
