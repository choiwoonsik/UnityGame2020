/* NoticeManager.cs
 * 알림창 관리 스크립트
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoticeManager : MonoBehaviour
{
    public Animator notificationAnimator; /* 나타나고 사라지는 애니메이션 */
    public GameObject notificationPanel; /* 알림창 Panel*/
    public TextMeshProUGUI notificationText; /* 알림창 Text */

    /* 알림창 나타나고 사라지게 이벤트 발생시킨다.
     * param _text : 알림창 Text 설정
     */
    public void NotificationAppear(string _text)
    {
        notificationAnimator.SetTrigger("Appear");
        notificationText.text = _text;
    }
}
