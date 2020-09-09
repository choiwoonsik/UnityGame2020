using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{

    private Vector3 position;
    private int speed;
    private PasserByManager thePasser;

    void Start()
    {
        thePasser = FindObjectOfType<PasserByManager>();
        /* 걸어다니는 속도 250-300사이에서 랜덤으로 설정 */
        speed = Random.Range(250, 301);
    }

    // Update is called once per frame
    void Update()
    {
        position = this.transform.position;

        if (DateManager.activated)
        {
            if(position.x < 9000)
            {
                /* 같은 속도로 일정하게 오른쪽으로 이동 */
                this.gameObject.transform.position = new Vector3(position.x + Time.deltaTime*speed, position.y, position.z);
            }
            else /* 끝에 도달하면 사라진다. */
            {
                Destroy(this.gameObject);

                if (thePasser.citizenCount > 0 && thePasser.qPasserLCountry.Count > 0)
                    thePasser.citizenCount--;
            }
        }
    }
}
