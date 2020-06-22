using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasserByManagerR : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject passer_by_R;
    public GameObject Canvas;

    private float currentTime;
    private GetCompanyManager theGet;
    private PasserByManager thePasser;
    private CitizenChatManager theChat;
    private ChangeCountryName theCountry;
    private PasserType theType;

    void Start()
    {
        currentTime = Random.Range(1, 2);
        theGet = FindObjectOfType<GetCompanyManager>();
        thePasser = FindObjectOfType<PasserByManager>();
        theChat = FindObjectOfType<CitizenChatManager>();
        theCountry = FindObjectOfType<ChangeCountryName>();
        theType = FindObjectOfType<PasserType>();
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
                currentTime = Random.Range(1, 21 - theGet.populationMax);

                var clone = Instantiate(passer_by_R, Canvas.transform);

                int img = Random.Range(0, 3);

                string path = "PasserBy/Inverse/" + img.ToString(); 
                Sprite newSprite = Resources.Load(path, typeof(Sprite)) as Sprite;
                clone.GetComponent<Image>().sprite = newSprite;

                if (img == 1 || img == 2)
                {
                    clone.transform.position = new Vector3(clone.transform.position.x, Random.Range(clone.transform.position.y+10, clone.transform.position.y + 70), clone.transform.position.z);

                    theChat.passer_List.Add(clone);

                    //나라별에서 타입별로 시민수 체크
                    theType.plusPersonTypeCount((int)theCountry.countrySlider.value-1, img);

                    thePasser.qPasserRCountry.Enqueue((int)theCountry.countrySlider.value-1);
                    thePasser.qPasserRType.Enqueue(img);
                    thePasser.citizenCount++;
                }
                else
                {
                    clone.transform.position = new Vector3(clone.transform.position.x, Random.Range(clone.transform.position.y+210, clone.transform.position.y + 390), clone.transform.position.z);

                    theChat.passer_List.Add(clone);

                    //나라별에서 타입별로 시민수 체크
                    theType.plusPersonTypeCount((int)theCountry.countrySlider.value-1, img);

                    thePasser.qPasserRCountry.Enqueue((int)theCountry.countrySlider.value-1);
                    thePasser.qPasserRType.Enqueue(img);
                    thePasser.citizenCount++;
                }
            }
        }
    }

}
