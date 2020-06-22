using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Hart : MonoBehaviour
{
    //하트, 하트 색깔 조절위한 렌더러 참조
    public GameObject[] Harts;
    private new SpriteRenderer renderer;
    public bool On;

    //시작 시 하트개수 0으로 초기화
    void Start()
    {
        for(int i = 0; i < Harts.Length; i++)
        {
            renderer = Harts[i].GetComponent<SpriteRenderer>();
            renderer.color = new Color(51 / 255f, 51 / 255f, 51 / 255f);
            On = false;
        }
    }
}
