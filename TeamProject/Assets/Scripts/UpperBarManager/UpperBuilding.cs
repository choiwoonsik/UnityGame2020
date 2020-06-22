using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpperBuilding : MonoBehaviour
{
    /* 배열 부분 필요 없다고 생각하여 삭제 -> 배열은 PlayerStatManager에서만 관리 */
    //하트, 하트 색깔 조절위한 렌더러 참조
    private new SpriteRenderer renderer;

    void Start()
    {
        /*자기 자신만 세팅하도록 수정 */
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color(60 / 255f, 60 / 255f, 60 / 255f);
    }
}
