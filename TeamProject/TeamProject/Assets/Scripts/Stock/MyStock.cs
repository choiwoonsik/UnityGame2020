using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//내 소유 주식 관리 함수
public class MyStock : MonoBehaviour
{
    public StockManager sM;
    public TextMeshProUGUI myStockNameT;
    public TextMeshProUGUI myStockPriceT;
    public TextMeshProUGUI myStockCount;
    public TextMeshProUGUI revenuePriceText;
    public TextMeshProUGUI ratingPriceText;
    public GameObject[] ownStock;
    public double revenuePrice;
}    
