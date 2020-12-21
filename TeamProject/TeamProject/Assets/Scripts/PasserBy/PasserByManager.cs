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
    public Queue<int> qPasserLCountry = new Queue<int>();
    public Queue<int> qPasserRCountry = new Queue<int>();

    private float currentTime;
    private CitizenChatManager theChat;

    void Start()
    {
        /* 첫 1-2초 사이에 등장 */
        currentTime = Random.Range(1, 2);
        theChat = FindObjectOfType<CitizenChatManager>();
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
            else if (Random.Range(0,100) < 15)
            {
                /* 다음 사람은 1-20 초 후 등장한다 */
                currentTime = Random.Range(4, 15);
                /* Instantiate 함수를 통해 Prefab 형태로 새로운 GameObject를 만든다.
                 * 부모는 Cavas.transform으로 설정한다.
                 */
                var clone = Instantiate(passer_by, Canvas.transform);

                /* 이미지 랜덤 설정 */
                int img = Random.Range(0, 18);

                string path = "PasserBy/" + img.ToString();
                Sprite newSprite = Resources.Load(path, typeof(Sprite)) as Sprite;
                clone.GetComponent<Image>().sprite = newSprite;

                /* 처음 위치 설정 */
                if (img == 1 || img == 2 || img == 16)
                {
                    clone.transform.position = new Vector3(Random.Range(clone.transform.position.x - 3000, clone.transform.position.x + 3000), Random.Range(clone.transform.position.y - 70, clone.transform.position.y), clone.transform.position.z);

                    theChat.passer_List.Add(clone);
                }
                else
                {
                    clone.transform.position = new Vector3(Random.Range(clone.transform.position.x - 3000, clone.transform.position.x + 3000), Random.Range(clone.transform.position.y+130, clone.transform.position.y+280), clone.transform.position.z);

                    theChat.passer_List.Add(clone);
                }
            }
        }
    }
}
