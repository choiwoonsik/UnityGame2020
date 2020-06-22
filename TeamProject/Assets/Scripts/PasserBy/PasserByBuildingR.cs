using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasserByBuildingR : MonoBehaviour
{
    public GameObject passer_by_Building;
    public GameObject Canvas;
    //건물 나오는 사람 따로 계산

    private float currentTime;
    private GetCompanyManager theGet;
    private PasserByManager thePasser;
    private LandmarksHandler theBuild;
    private CitizenChatManager theChat;
    private StockManager theStock;
    private PasserType theType;
    private ChangeCountryName theCountry;

    void Start()
    {
        currentTime = Random.Range(1, 2);
        theGet = FindObjectOfType<GetCompanyManager>();
        thePasser = FindObjectOfType<PasserByManager>();
        theBuild = FindObjectOfType <LandmarksHandler>();
        theChat = FindObjectOfType<CitizenChatManager>();
        theStock = FindObjectOfType<StockManager>();
        theType = FindObjectOfType<PasserType>();
        theCountry = FindObjectOfType<ChangeCountryName>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DateManager.activated)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            /* 새로운 사람이 등장한다 */
            else if ((theGet.populationMax + 1) * 4 >= thePasser.citizenCount)
            {
                for (int i = 0; i < theBuild.Buildings.Length; i++)
                {
                    if (theBuild.Buildings[i].has && theBuild.Buildings[i].stockIn && Random.Range(0, 100) < theBuild.Buildings[i].attractivePoint)
                    {
                        currentTime = Random.Range(1, 17 - theGet.populationMax);
                        var clone = Instantiate(passer_by_Building, Canvas.transform);

                        int img = 0;
                        int checkN = theStock.stockType[(int)theGet.stockTable[i]];
                        //IT
                        if (checkN == 0)
                            img = 14;
                        //제약
                        else if (checkN == 1)
                            img = Random.Range(9, 14);
                        //패션,화장품
                        else if (checkN == 2)
                            img = 17;
                        //요식업
                        else if (checkN == 3)
                            img = 6;
                        //영화,드라마
                        else if (checkN == 4)
                            img = 3;

                        string path = "PasserBy/Inverse/" + img.ToString();
                        Sprite newSprite = Resources.Load(path, typeof(Sprite)) as Sprite;

                        clone.GetComponent<Image>().sprite = newSprite;

                        //자전거
                        if (img == 1 || img == 2)
                        {
                            return;
                        }
                        //아닌거
                        else
                        {
                            Debug.Log("img >> " + img);
                            clone.transform.position = new Vector3(theBuild.Buildings[i].GetComponent<Transform>().position.x + 30,
                                    Random.Range(clone.transform.position.y + 210, clone.transform.position.y + 390), clone.transform.position.z);

                            theChat.passer_BuildList.Add(clone);
                            theChat.passer_BuildNumList.Add(i);

                            //나라별에서 타입별로 시민수 체크
                            theType.plusPersonTypeCount((int)theCountry.countrySlider.value-1 ,img);

                            thePasser.qPasserRType.Enqueue(img);

                            thePasser.citizenCount++;
                        }

                    }
                }
            }
        }
    }

}
