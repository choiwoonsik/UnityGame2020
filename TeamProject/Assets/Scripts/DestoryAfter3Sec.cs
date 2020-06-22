using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAfter3Sec : MonoBehaviour
{
    private float currentTime = 3.0f;

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0) {
            Destroy(this.gameObject);
        }
    }
}
