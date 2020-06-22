using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasserByManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject passer_by;
    public GameObject Canvas;
    private float currentTime;

    void Start()
    {
        currentTime = Random.Range(1, 2);
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
            else
            {
                currentTime = Random.Range(1, 7);
                var clone = Instantiate(passer_by, Canvas.transform);
                int img = Random.Range(0, 9);
                string path = "PasserBy/" + img.ToString();
                Sprite newSprite = Resources.Load(path, typeof(Sprite)) as Sprite;
                clone.GetComponent<Image>().sprite = newSprite;
                if(img == 1 || img == 2)
                {
                    clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y - 80, clone.transform.position.z);
                }
                else
                {
                    clone.transform.position = new Vector3(clone.transform.position.x, Random.Range(clone.transform.position.y, clone.transform.position.y+50), clone.transform.position.z);
                }
            }
        }
    }
}
