using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* 천천히 올라가면서 1.5초 후 사라지는 텍스트 */
public class FloatingText : MonoBehaviour
{

    public TextMeshProUGUI text;
    private float currentTime = 1.5f;

    private void Update()
    {
        if (currentTime > 0.0f)
        {
            currentTime -= Time.deltaTime;
            this.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            Color color = text.color;
            color.a -= 3 / 255.0f; //점점 투명해진다.
            text.color = color;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    /* text 내용 바꾸기 */
    public void SetText(string _text)
    {
        text.text = _text;
    }
}
