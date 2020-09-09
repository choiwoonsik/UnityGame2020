using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasserByManager : MonoBehaviour
{
    public GameObject passer_by; /*Prefab*/
    public GameObject Canvas;
    public int citizenCount = 0;
    public TextMeshProUGUI CitizenCountT;
    public Queue<int> qPasserLType = new Queue<int>();
    public Queue<int> qPasserRType = new Queue<int>();
    public Queue<int> qPasserLCountry = new Queue<int>();
    public Queue<int> qPasserRCountry = new Queue<int>();

    private float currentTime;
    private int countryNum = -1;
    private GetCompanyManager theGet;
    private CitizenChatManager theChat;
    private PasserType theType;
    private ChangeCountryName theCountry;
    private LandmarksHandler theHandler;

    void Start()
    {
        /* 첫 1-2초 사이에 등장 */
        currentTime = Random.Range(1, 2);
        theGet = FindObjectOfType<GetCompanyManager>();
        theChat = FindObjectOfType<CitizenChatManager>();
        theType = FindObjectOfType<PasserType>();
        theCountry = FindObjectOfType<ChangeCountryName>();
        theHandler = FindObjectOfType<LandmarksHandler>();
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
            else if ( (theGet.populationMax+1) * 4 >= citizenCount)
            {
                /* 다음 사람은 1-20 초 후 등장한다 */
                currentTime = Random.Range(1, 21 - theGet.populationMax);
                /* Instantiate 함수를 통해 Prefab 형태로 새로운 GameObject를 만든다.
                 * 부모는 Cavas.transform으로 설정한다.
                 */
                var clone = Instantiate(passer_by, Canvas.transform);

                /* 이미지 랜덤 설정 */
                int img = Random.Range(0, 3);

                string path = "PasserBy/" + img.ToString();
                Sprite newSprite = Resources.Load(path, typeof(Sprite)) as Sprite;
                clone.GetComponent<Image>().sprite = newSprite;

                if (theHandler.allCountryAttractivePoint == 0)
                    countryNum = Random.Range(0, 5);
                else
                {
                    while (countryNum < 0 && theHandler.allCountryAttractivePoint > 0)
                    {
                        if (Random.Range(0, 100) < theHandler.countryAttractivePoint[0] / theHandler.allCountryAttractivePoint * 100)
                            countryNum = 0;
                        else if (Random.Range(0, 100) < theHandler.countryAttractivePoint[1] / theHandler.allCountryAttractivePoint * 100)
                            countryNum = 1;
                        else if (Random.Range(0, 100) < theHandler.countryAttractivePoint[2] / theHandler.allCountryAttractivePoint * 100)
                            countryNum = 2;
                        else if (Random.Range(0, 100) < theHandler.countryAttractivePoint[3] / theHandler.allCountryAttractivePoint * 100)
                            countryNum = 3;
                        else if (Random.Range(0, 100) < theHandler.countryAttractivePoint[4] / theHandler.allCountryAttractivePoint * 100)
                            countryNum = 4;
                    }
                }

                /* 처음 위치 설정 */
                if (img == 1 || img == 2 || img == 16)
                {
                    clone.transform.position = new Vector3(clone.transform.position.x, Random.Range(clone.transform.position.y - 70, clone.transform.position.y), clone.transform.position.z);

                    theChat.passer_List.Add(clone);

                    //나라별에서 타입별로 시민수 체크
                    theType.plusPersonTypeCount(countryNum, img);

                    qPasserLType.Enqueue(img);
                    qPasserLCountry.Enqueue(countryNum);
                    citizenCount++;
                }
                else
                {
                    clone.transform.position = new Vector3(clone.transform.position.x, Random.Range(clone.transform.position.y+130, clone.transform.position.y+280), clone.transform.position.z);

                    theChat.passer_List.Add(clone);

                    //나라별에서 타입별로 시민수 체크
                    theType.plusPersonTypeCount(countryNum, img);

                    qPasserLType.Enqueue(img);
                    qPasserLCountry.Enqueue(countryNum);
                    citizenCount++;
                }
                countryNum = -1;
            }
        }
    }
}
