using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpperBarManager : MonoBehaviour
{
    [Header("Manager")]
    public PlayerStatManager theStat;
    public DateManager theDate;

    [Header("GameObject")]
    public GameObject profileButton;
    public GameObject mailButton;

    [Header("Text")]
    public TextMeshProUGUI upperCashT;
    public TextMeshProUGUI upperStockT;

    private float currentTime;
    private double startStat;
    private double startStock;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 5f;
        startStat = theStat.myAllMoney;
        startStock = theStat.myAllStockMoney;

        upperCashT.text = "총 현금 자산> $" + string.Format("{0}", theStat.myAllMoney.ToString("n1"));
        upperStockT.text = "총 주식 자산> $" + string.Format("{0}", theStat.myAllStockMoney.ToString("n1"));
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0f)
        {
            currentTime = 5f;
            /*현금*/
            double varStat = theStat.myAllMoney - startStat;
            if (varStat < 0)
            {
                if (varStat / 10000 >= 1)
                    upperCashT.text = "총 현금 자산> $" + string.Format("{0}", (theStat.myAllMoney / 10000).ToString("n0")) + "만" + "<color=#0000FF>\n"
                                        + "- $" + string.Format("{0}", (varStat / 10000).ToString("n0")) + "만" + "</color>";
                else
                    upperCashT.text = "총 현금 자산> $" + string.Format("{0}", theStat.myAllMoney.ToString("n1")) + "<color=#0000FF>\n"
                                        + "$" + string.Format("{0}", varStat.ToString("n1")) + "</color>";
            }

            else if (varStat > 0)
            {
                if (varStat / 10000 >= 1)
                    upperCashT.text = "총 현금 자산> $" + string.Format("{0}", (theStat.myAllMoney / 10000).ToString("n0")) + "만" + "<color=#FF0000>\n"
                                        + "+ $" + string.Format("{0}", (varStat / 10000).ToString("n0")) + "만" + "</color>";
                else
                    upperCashT.text = "총 현금 자산> $" + string.Format("{0}", theStat.myAllMoney.ToString("n1")) + "<color=#FF0000>\n"
                                        + string.Format("{0}", varStat.ToString("n1")) + "$" + "</color>";
            }
            else
                upperCashT.text = "총 현금 자산> $" + string.Format("{0}", theStat.myAllMoney.ToString("n1"));


            /*주식*/
            double varStock = theStat.myAllStockMoney - startStock;
            if (varStock < 0)
            {
                if (varStat / 10000 >= 1)
                    upperStockT.text = "총 주식 자산> $" + string.Format("{0}", (theStat.myAllStockMoney / 10000).ToString("n0")) + "만" + "<color=#0000FF>\n"
                                        + "- $" + string.Format("{0}", (varStat / 10000).ToString("n0")) + "만" + "</color>";
                else
                    upperStockT.text = "총 주식 자산> $" + string.Format("{0}", theStat.myAllStockMoney.ToString("n1")) + "<color=#0000FF>\n"
                                        + "$" + string.Format("{0}", varStock.ToString("n1")) + "</color>";
            }
            else if (varStock > 0)
            {
                if (varStat / 10000 >= 1)
                    upperStockT.text = "총 주식 자산> $" + string.Format("{0}", (theStat.myAllStockMoney / 10000).ToString("n0")) + "만" + "<color=#FF0000>\n"
                                        + "+ $" + string.Format("{0}", (varStat / 10000).ToString("n0")) + "만" + "</color>";
                else
                    upperStockT.text = "총 주식 자산> $" + string.Format("{0}", theStat.myAllStockMoney.ToString("n1")) + "<color=#FF0000>\n"
                                        + "$" + string.Format("{0}", varStock.ToString("n1")) + "</color>";
            }
            else
                upperStockT.text = "총 주식 자산> $" + string.Format("{0}", theStat.myAllStockMoney.ToString("n1"));

            /*5초 후 값과 비교*/
            startStat = theStat.myAllMoney;
            startStock = theStat.myAllStockMoney;
        }
    }
}
