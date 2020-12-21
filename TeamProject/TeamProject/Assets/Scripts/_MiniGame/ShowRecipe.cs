using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRecipe : MonoBehaviour
{
	public GameObject Panel;
	private int flag = -1;

	private bool buttonFlag = false;
	private float currentTime = 60.0f;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime * flag * 50;
		if(currentTime < 0){
			flag = 1;
		}
		else if(currentTime > 30.0f){
			flag = -1;
		}

		this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, -263+ currentTime, this.gameObject.transform.position.z);
    }

	public void ButtonOnClick(){
		buttonFlag = !buttonFlag;
		if(buttonFlag) Panel.SetActive(true);
		else Panel.SetActive(false);
	}
}
