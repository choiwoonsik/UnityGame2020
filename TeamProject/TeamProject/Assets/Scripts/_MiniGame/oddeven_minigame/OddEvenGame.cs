/**
 * @brief 홀짝 게임 Script
 * @file OddEvenGame.cs
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OddEvenGame : MonoBehaviour
{
    public GameObject gamePanel; /*게임 배경 Panel*/
    public GameObject oddEvenChoicePanel; /* 홀 짝 선택 Panel */
    public GameObject goStopPanel; /* 고 스톱 선택 Panel */
    public Animator dieAnimation; /* 주사위 Animator */
    public GameObject noticePanel; /* 알림 Panel */
    public GameObject oneMorePanel;
    public TextMeshProUGUI noticeText; /* 알림 Text */
    public TextMeshProUGUI multipleText; /* 몇배인지 알려주는 Text */

    public int inputMoney; /* 배팅한 금액 테스트를 위해서 200으로 설정함 */
    private int times = 2;

    private NoticeManager theNotice;
    private PlayerStatManager theStat;
    private MiniGameOnOff theMiniGame;
    /*레포트 미니게임 번 돈*/
    public StockReportManager theReport;

    // Start is called before the first frame update
    void Start()
    {
        theNotice = FindObjectOfType<NoticeManager>();
        theStat = FindObjectOfType<PlayerStatManager>();
        theMiniGame = FindObjectOfType<MiniGameOnOff>();
    }

    /* 초기화 하는 함수 */
    public void init()
    {
        times = 2;
        multipleText.text = "X2";
        goStopPanel.SetActive(false);
        oddEvenChoicePanel.SetActive(true);
        gamePanel.SetActive(true);
    }

    /* 주사위를 던질 때 실행되는 함수 */
    public void StartThrowDie()
    {
        gamePanel.SetActive(true);
        oddEvenChoicePanel.SetActive(false);
    }

    /* 성공하고 고 버튼을 누를 때 실행되는 함수 */
    public void GoButtonOnClick()
    {
        dieAnimation.SetBool("Throw", true); //주사위를 던지는 애니메이션
        goStopPanel.SetActive(false);
        oddEvenChoicePanel.SetActive(true); //홀짝을 선택하게 한다.
        times *= 2; //지금 숫자의 2배수가 된다.
        multipleText.text = "X" + times;
    }

    /* 성공하고 스탑 버튼을 누를 때 실행되는 함수 */
    public void StopButtonOnClick()
    {
        gamePanel.SetActive(false); //모든 패널을 닫는다.
        theNotice.NotificationAppear("미니게임에 성공하여 돈을 획득했습니다 : " + inputMoney + "$");
        theStat.myAllMoney += inputMoney; //돈을 획득
        theStat.playerCash += inputMoney;
        theMiniGame.ProfileSetting(); //text 설정
        /*레포트 미니게임 번 돈*/
        theReport.nMonthEarnMiniMoney += inputMoney;
    }

    /* 주사위를 던지고 홀수 버튼을 누를 때 실행되는 함수 */
    public void OddButtonOnClick()
    {
        int num = Random.Range(1, 7); //1부터 6까지의 난수를 생성한다.
        dieAnimation.SetInteger("Value", num); //주사위 눈을 보여주는 애니메이션
        dieAnimation.SetBool("Throw", false);

        if(num%2 == 1) //홀수일 경우 -> 성공 -> 다시 던질래 말래 물어본다.
        {
            inputMoney *= 2;
            StartCoroutine(SuccessCoroutine());
            StartCoroutine(ReplayCoroutine());
        }
        else //짝수일 경우 -> 실패 -> 프로그램 종료
        {
            theNotice.NotificationAppear("미니게임에 실패하였습니다");
            StartCoroutine(DelayCoroutine());
            theMiniGame.ProfileSetting(); //text 설정
            oneMorePanel.SetActive(true);
        }
    }

    /* 주사위를 던지고 짝수 버튼을 누를 때 실행되는 함수 */
    public void EvenButtonOnClick()
    {
        int num = Random.Range(1, 7); //1부터 6까지의 난수를 생성한다.
        dieAnimation.SetInteger("Value", num); //주사위 눈을 보여주는 애니메이션
        dieAnimation.SetBool("Throw", false);

        if (num % 2 == 0) //짝수일 경우 -> 성공 -> 다시 던질래 말래 물어본다.
        {
            inputMoney *= 2;
            StartCoroutine(SuccessCoroutine());
            StartCoroutine(ReplayCoroutine());
        }
        else //홀수일 경우 -> 실패 -> 프로그램 종료
        {
            theNotice.NotificationAppear("미니게임에 실패하였습니다");
            StartCoroutine(DelayCoroutine());
            theMiniGame.ProfileSetting(); //text 설정
        }
    }

    /* 성공임을 보여주는 noticePanel 활성화 시키고 끈다 */
    IEnumerator SuccessCoroutine()
    {
        noticeText.text = "대박!";
        noticePanel.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        noticePanel.SetActive(false);
    }

    /* 게임 종료 전 약간 지연시킨다. */
    IEnumerator DelayCoroutine()
    {
        oddEvenChoicePanel.SetActive(false);
        goStopPanel.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        gamePanel.SetActive(false);
    }

    /* 성공한 모습 보여주고(0.5초간) 다시 던질래 말래 보여준다 */
    IEnumerator ReplayCoroutine()
    {
        oddEvenChoicePanel.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        goStopPanel.SetActive(true);
        dieAnimation.SetBool("Throw", true);
    }
}
