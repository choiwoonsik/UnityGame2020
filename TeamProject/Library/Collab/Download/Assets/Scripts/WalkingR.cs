using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingR : MonoBehaviour
{

    private Vector3 position;
    private int speed;

    void Start()
    {
        speed = Random.Range(250, 301);
    }

    // Update is called once per frame
    void Update()
    {
        position = this.transform.position;

        if (DateManager.activated)
        {
            if(position.x > -5550)
            {
                this.gameObject.transform.position = new Vector3(position.x - Time.deltaTime*speed, position.y, position.z);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
