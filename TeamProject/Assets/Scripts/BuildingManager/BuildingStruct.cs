using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingStruct : MonoBehaviour
{
    public bool has = false;
    public bool stockIn = false;
    public bool hasSynergy = false;
    public string country;
    public string type;
    public string buildingName;
    public string buildingDetail;
    public string buildingLocation;
    public int buildingPrice;
    public int monthlyRevenue;
    public GameObject image;
    public float buildingCondition = 1.00f;
    //매력지수
    public int attractivePoint = 0;
    public int stockTerm = 0;
}
