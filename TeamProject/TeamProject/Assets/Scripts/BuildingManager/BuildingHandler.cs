using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingHandler : BuildingStruct
{
    public GameObject bug;
    public GameObject poop;

    //주식
    public bool putStock;
    private float updateTime = 1;
    public float currentTime;
    private bool dirtyFlag = false;
    private GameObject bugClone;
    private GameObject poopClone;

    void Start()
    {
        Color color = image.GetComponent<Image>().color;
        color.a = 0.1f;
        image.GetComponent<Image>().color = color;
        type = "b";
        currentTime = updateTime;
    }

    void BeDirty()
    {
        dirtyFlag = true;
        bugClone = Instantiate(bug, this.gameObject.transform);
        Color color = image.GetComponent<Image>().color;
        color.r = 100 / 255.0f;
        color.g = 100 / 255.0f;
        color.b = 100 / 255.0f;
        image.GetComponent<Image>().color = color;

        poopClone = Instantiate(poop, this.gameObject.transform);
    }

    void BeClean()
    {
        dirtyFlag = false;
        Destroy(bugClone);
        Color color = image.GetComponent<Image>().color;
        color.r = 255 / 255.0f;
        color.g = 255 / 255.0f;
        color.b = 255 / 255.0f;
        image.GetComponent<Image>().color = color;
        Destroy(poopClone);
    }

    void Update()
    {
        if (DateManager.activated && has)
        {
            currentTime -= Time.deltaTime;
            if(currentTime < 0)
            {
                currentTime = updateTime;
                if(buildingCondition > 0.0f)
                {
                    buildingCondition -= 0.0005f;
                }
                if(buildingCondition <= 0.1f && !dirtyFlag)
                {
                    BeDirty();
                    //주민들의 불만이 쏟아져나온다.
                    //말풍선 추가 (?)
                }
            }
        }
        if (dirtyFlag && buildingCondition == 1.0f) //유지보수를 했습니다. 또는 건물을 되팔았습니다.
        {
            BeClean();
        }
    }
}
