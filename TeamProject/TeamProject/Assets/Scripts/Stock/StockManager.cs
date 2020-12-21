using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StockManager : MonoBehaviour
{
    //나의 본 주식 계좌인 MyStocks 클래스를 불러온다
    public MyStocks myStocks;
    //플레이어 스탯매니저
    public PlayerStatManager playerSM;
    //각각의 주식에 들어가는 스크립트배열
    public Stock[] stockSc;
    //내가 소유중인 각각의 주식에 들어가는 스크립트 배열
    public MyStock[] ownStockSc;
    public int[] stockType = new int[15] { 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4 };

    //MyStocks클래스에서 보유 주식 수량을 배열에 받아와서 사용한다
    void Start()
    {
        //본계좌의 주식소유량을 받아서 사용하는 배열
        playerSM.myStockCountArray = myStocks.GsMyAllStocks;
    }
}
