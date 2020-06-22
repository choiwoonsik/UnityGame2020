using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gradient_Test : MonoBehaviour
{
    public Gradient gradient;
    private DateManager theDate;

    [Range(0, 1)]
    public float t;

    public Image img;

    private int flag = -1;

    // Start is called before the first frame update
    void Start()
    {
        img = transform.GetComponent<Image>();
        t = 1.0f;
        theDate = FindObjectOfType<DateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DateManager.activated)
        {
            t += 0.09f * flag * Time.deltaTime / theDate.updateTime;
            img.color = gradient.Evaluate(t);
            if (t >= 1.0f)
            {
                t -= 0.09f * Time.deltaTime;
                flag = -1;
            }
            else if (t <= 0.0f)
            {
                t += 0.09f * Time.deltaTime;
                flag = 1;
            }
            if(theDate.hour == 0)
            {
                t = 1.0f;
            }
        }
    }
}
