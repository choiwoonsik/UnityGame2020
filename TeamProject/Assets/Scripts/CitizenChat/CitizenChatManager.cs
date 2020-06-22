using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenChatManager : MonoBehaviour
{
    public GameObject passer_chat; /*prefab*/
    public GameObject passer_buildingChat;

    public List<GameObject> passer_List;
    public List<GameObject> passer_BuildList;
    public List<int> passer_BuildNumList;

    private float currentTime;
    private float currentTimeBuild;
    private CitizenChat theChat;
    private CitizenBuildingChat theBuildCaht;
    private LandmarksHandler theHandler;

    // Start is called before the first frame update
    void Start()
    {
        theChat = FindObjectOfType<CitizenChat>();
        theBuildCaht = FindObjectOfType<CitizenBuildingChat>();
        theHandler = FindObjectOfType<LandmarksHandler>();
        passer_List = new List<GameObject>();
        passer_BuildList = new List<GameObject>();
        passer_BuildNumList = new List<int>();
        currentTime = Random.Range(4, 7);
        currentTimeBuild = Random.Range(4, 7);
    }

    // Update is called once per frame
    void Update()
    {
        if (DateManager.activated)
        {
            if(currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else if (passer_List.Count > 0 && currentTime <= 0)
            {
                if(passer_List[0] != null)
                {
                    currentTime = Random.Range(3, 5.5f);
                    GameObject tmpPos = passer_List[0];
                    passer_List.Remove(tmpPos);

                    var parent = tmpPos.transform.position.x;
                    if (parent < -6200 || parent > 6200)
                    {
                        currentTime = 0;
                        return;
                    }

                    if (Random.Range(0, 10) < 6f)
                    {
                        GameObject clone = Instantiate(passer_chat, tmpPos.transform) as GameObject;
                        clone.transform.position = new Vector3(tmpPos.transform.position.x, tmpPos.transform.position.y + 80, tmpPos.transform.position.z);

                        theChat = FindObjectOfType<CitizenChat>();
                        theChat.setTime();
                    }
                }
                else
                {
                    passer_List.RemoveAt(0);
                    currentTime = 0;
                }
            }

            if (currentTimeBuild > 0)
            {
                currentTimeBuild -= Time.deltaTime;
            }
            else if (passer_BuildList.Count > 0 && currentTimeBuild <= 0)
            {
                currentTimeBuild = Random.Range(0.5f, 3.5f);

                if(passer_BuildList[0] != null)
                {
                    GameObject tmpPosBuild = passer_BuildList[0];
                    int tmpNum = passer_BuildNumList[0];
                    passer_BuildList.Remove(tmpPosBuild);
                    passer_BuildNumList.RemoveAt(0);

                    var parent = tmpPosBuild.transform.position.x;
                    if (parent < -6000 || parent > 6000)
                    {
                        currentTimeBuild = 0;
                        return;
                    }

                    if (theHandler.Buildings[tmpNum].attractivePoint > 30)
                    {
                        GameObject clone = Instantiate(passer_buildingChat, tmpPosBuild.transform) as GameObject;
                        clone.transform.position = new Vector3(tmpPosBuild.transform.position.x, tmpPosBuild.transform.position.y + 80, tmpPosBuild.transform.position.z);

                        theBuildCaht = FindObjectOfType<CitizenBuildingChat>();
                        string[] nomalText = { "운식", "운시기", "시기시기", "식식", "웅싯", "웅식", "웅" };
                        theBuildCaht.setChatBuildingInText(nomalText[Random.Range(0, nomalText.Length)]);
                        theBuildCaht.setTime();
                    }
                    else
                    {
                        GameObject clone = Instantiate(passer_buildingChat, tmpPosBuild.transform) as GameObject;
                        clone.transform.position = new Vector3(tmpPosBuild.transform.position.x, tmpPosBuild.transform.position.y + 80, tmpPosBuild.transform.position.z);

                        theBuildCaht = FindObjectOfType<CitizenBuildingChat>();
                        theBuildCaht.setTime();
                        if (theHandler.Buildings[tmpNum].buildingCondition <= 0.1f)
                        {
                            theBuildCaht.setChatBuildingInText("여기 " + "<color=#FF0000>" + theHandler.Buildings[tmpNum].buildingName + "</color>" + " 냄새 나서 못살겠어!");
                        }
                        else {
                            theBuildCaht.setChatBuildingInText("<color=#FF0000>" + theHandler.Buildings[tmpNum].buildingName + "</color>" + " 참 살기 좋아 ~!");
                        }
                    }
                }
                else
                {
                    passer_BuildList.RemoveAt(0);
                    passer_BuildNumList.RemoveAt(0);
                    currentTimeBuild = 0;
                }
            }
        }
    }
}
