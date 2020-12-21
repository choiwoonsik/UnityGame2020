using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSlightly : MonoBehaviour
{
	private int flag = -1;
	private float currentTime = 60.0f;
	private float initY;

	void Start()
	{
		currentTime = Random.Range(30.0f, 60.0f);
		initY = this.gameObject.transform.position.y;
	}

	// Update is called once per frame
	void Update()
	{
		currentTime += Time.deltaTime * flag * 50;
		if (currentTime < 0)
		{
			flag = 1;
		}
		else if (currentTime > 30.0f)
		{
			flag = -1;
		}

		this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, initY + currentTime, this.gameObject.transform.position.z);
	}
}
