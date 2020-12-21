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
    private CitizenChatManager theChat;

    void Start()
    {
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
                currentTime = Random.Range(4, 15);

                var clone = Instantiate(passer_by_R, Canvas.transform);

                int img = Random.Range(0, 18);

                string path = "PasserBy/Inverse/" + img.ToString(); 
                Sprite newSprite = Resources.Load(path, typeof(Sprite)) as Sprite;
                clone.GetComponent<Image>().sprite = newSprite;

                if (img == 1 || img == 2 || img == 16)
                {
                    clone.transform.position = new Vector3(Random.Range(clone.transform.position.x - 3000, clone.transform.position.x + 3000), Random.Range(clone.transform.position.y+10, clone.transform.position.y + 70), clone.transform.position.z);

                    theChat.passer_List.Add(clone);
                }
                else
                {
                    clone.transform.position = new Vector3(Random.Range(clone.transform.position.x-3000, clone.transform.position.x+3000), Random.Range(clone.transform.position.y+210, clone.transform.position.y + 390), clone.transform.position.z);

                    theChat.passer_List.Add(clone);
                }
            }
        }
    }

}
