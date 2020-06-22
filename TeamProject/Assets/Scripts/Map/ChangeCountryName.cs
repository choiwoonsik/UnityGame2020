using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeCountryName : MonoBehaviour
{
    public TextMeshProUGUI countryNameText;
    public Scrollbar mapBar;
    public Image flagImage;
    public Slider countrySlider;
    private Sprite newSprite;
   
    public void ValueChanged()
    {
        if(mapBar.value < 0.234)
        {
            countrySlider.value = 1;
            countryNameText.text = "프랑스";
            newSprite = Resources.Load("NationalFlag/france", typeof(Sprite)) as Sprite;
            flagImage.sprite = newSprite;
        }
        else if(mapBar.value < 0.445)
        {
            countrySlider.value = 2;
            countryNameText.text = "미국";
            newSprite = Resources.Load("NationalFlag/usa", typeof(Sprite)) as Sprite;
            flagImage.sprite = newSprite;
        }
        else if(mapBar.value < 0.658)
        {
            countrySlider.value = 3;
            countryNameText.text = "호주";
            newSprite = Resources.Load("NationalFlag/australia", typeof(Sprite)) as Sprite;
            flagImage.sprite = newSprite;
        }
        else if(mapBar.value < 0.867)
        {
            countrySlider.value = 4;
            countryNameText.text = "대한한국";
            newSprite = Resources.Load("NationalFlag/korea", typeof(Sprite)) as Sprite;
            flagImage.sprite = newSprite;
        }
        else
        {
            countrySlider.value = 5;
            countryNameText.text = "독일";
            newSprite = Resources.Load("NationalFlag/germany", typeof(Sprite)) as Sprite;
            flagImage.sprite = newSprite;
        }
    }
}
