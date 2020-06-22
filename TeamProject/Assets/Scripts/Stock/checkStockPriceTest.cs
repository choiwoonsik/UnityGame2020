using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkStockPriceTest : MonoBehaviour
{

    private DateManager theDate;
    private StockManager theStock;
    public Stack<int> stockStack = new Stack<int>();
    private int m = 1;
    private int d = 1;
    private bool onlyone = false;

    // Start is called before the first frame update
    void Start()
    {
        theDate = FindObjectOfType<DateManager>();
        theStock = FindObjectOfType<StockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theDate.month == m && theDate.day == d)
            inStack();

        if (theDate.month == m + 1)
            outStack();
    }

    public void inStack()
    {
        if (!onlyone)
        {
            for (int i = 0; i < theStock.stockSc.Length; i++)
            {
                stockStack.Push((int)theStock.stockSc[i].thisStockPrice);
            }
            onlyone = true;
        }
    }


    public void outStack()
    {
        Debug.Log("=================" + m + "월 =================");
        Debug.Log(stockStack);
        while (stockStack.Count > 0)
            Debug.Log("check : "+stockStack.Pop());
        m++;
        onlyone = false;
    }
}
