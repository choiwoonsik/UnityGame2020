using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public SadariManager SM;
    public int finishNum;
    public bool isEnd; //종료 확인

    bool isHorizontal;//왼쪽 or 오른쪽으로 이동 체크
    Vector2 dir;//방향

    public SadariManager thePlayer;

    // playerCs를 초기화한다
    public void Clear()
    {
        GetComponent<Button>().interactable = true;
        isEnd = false;
    }


    public IEnumerator Move()
    {
        //이미 내려갔는데 전체결과로 또 불러오면 안되기 때문에
        if (isEnd)
            yield break;

        finishNum = 0;
        isHorizontal = false;

        dir = Vector2.down;
        SM.PlayersGl.enabled = false;
        GetComponent<Button>().interactable = false;

        SM.BlindPanel.SetActive(true);

        RectTransform Rt = GetComponent<RectTransform>();
        thePlayer = FindObjectOfType<SadariManager>();
        int player;

        //플레이어가 고른 풍선은 2배로 내려가고 나머지는 정배속으로 내려감
        if (thePlayer.player)
            player = 2;
        else
            player = 1;


        while(Rt.anchoredPosition.y > -4000)
        {
            transform.Translate(dir * 450 * player *Time.deltaTime);

            yield return new WaitForSeconds(0.001f);
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        switch (col.name)
        {
            case "Left": case "Right":
                yield return new WaitForSeconds(0.001f);
                if (isHorizontal)
                {
                    isHorizontal = false;
                    dir = Vector2.down;
                }
                else
                {
                    isHorizontal = true;
                    dir = (col.name == "Left") ? Vector2.right : Vector2.left;
                }
                break;

            case "1": case "2": case "3": case "4": case "5": case "6":
                finishNum = int.Parse(col.name);
                isEnd = true;
                break;
        }
    }

}
