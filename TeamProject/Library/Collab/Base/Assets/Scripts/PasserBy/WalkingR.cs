using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingR : MonoBehaviour
{

    private Vector3 position;
    private int speed;
    private PasserByManager thePasser;
    private PasserType theType;

    void Start()
    {
        thePasser = FindObjectOfType<PasserByManager>();
        theType = FindObjectOfType<PasserType>();
        speed = Random.Range(250, 301);
    }

    // Update is called once per frame
    void Update()
    {
        position = this.transform.position;

        if (DateManager.activated)
        {
            if(position.x > -9000)
            {
                this.gameObject.transform.position = new Vector3(position.x - Time.deltaTime*speed, position.y, position.z);
            }
            else
            {
                Destroy(this.gameObject);

                if (thePasser.qPasserRType.Count > 0 && thePasser.citizenCount > 0)
                {
                    theType.minusPersonTypeCount(thePasser.qPasserRCountry.Dequeue() ,thePasser.qPasserRType.Dequeue());
                    thePasser.citizenCount--;
                }
            }
        }
    }
}
