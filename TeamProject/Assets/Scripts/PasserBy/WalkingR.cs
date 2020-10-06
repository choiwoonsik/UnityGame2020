using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingR : MonoBehaviour
{

    private Vector3 position;
    private int speed;
    private PasserByManager thePasser;
    private PasserType theType;

    private float pos = -9000f;
    private float currentTime;

    void Start()
    {
        currentTime = Random.Range(15, 19);
        theType = FindObjectOfType<PasserType>();
        thePasser = FindObjectOfType<PasserByManager>();
        speed = Random.Range(230, 301);
    }

    // Update is called once per frame
    void Update()
    {
        position = this.transform.position;

        if (DateManager.activated)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                if (position.x > pos)
                {
                    this.gameObject.transform.position = new Vector3(position.x - Time.deltaTime * speed, position.y, position.z);
                }
                //else
                //{
                //    if (thePasser.citizenCount > 0 && thePasser.qPasserRCountry.Count > 0)
                //    {
                //        theType.minusPersonTypeCount(thePasser.qPasserRCountry.Dequeue());
                //        thePasser.citizenCount--;
                //    }
                //    else if (thePasser.qPasserRCountry.Count == 0 && thePasser.citizenCount > 0)
                //        theType.minusPersonTypeCount(Random.Range(0, 5));
                //    Destroy(this.gameObject);
                //}
            }
            else
            {
                if (thePasser.citizenCount > 0 && thePasser.qPasserRCountry.Count > 0)
                {
                    theType.minusPersonTypeCount(thePasser.qPasserRCountry.Dequeue());
                    thePasser.citizenCount--;
                }
                else if (thePasser.qPasserRCountry.Count == 0 && thePasser.citizenCount > 0)
                {
                    int max = 0;
                    int countryN = 0;
                    for (int i = 0; i < 5; i++) {
                        if (theType.personType[i] > max){
                            max = theType.personType[i];
                            countryN = i;
                        }
                    }
                    theType.minusPersonTypeCount(countryN);
                    thePasser.citizenCount--;
                }
                Destroy(this.gameObject);
            }
        }
    }
}
