using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//나의 본 주식 계좌 역활
public class MyStocks : MonoBehaviour
{
    //주식
    public int[] myAllStocks = new int[15] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public int[] GsMyAllStocks
    {
        get
        {//주거니
            return myAllStocks;
        }
        set
        {//받거니
            myAllStocks = value;
        }
    }

    /* myAllMoney 부분 필요 없다고 생각하여 삭제 */

    //받고
    public void GetStock(int index, int n)
    {
        myAllStocks[index] += n;
    }
    //더블로가
    public void GiveStock(int index, int n)
    {
        myAllStocks[index] -= n;
    }
}
