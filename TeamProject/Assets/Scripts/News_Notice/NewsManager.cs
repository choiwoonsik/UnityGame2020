/**
 * @brief 뉴스나 쪽지의 전반적 관리
 * @date 2020/03/06 마지막수정
 * @file DatabaseManager.cs
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewsManager : MonoBehaviour
{
    private DateManager theDate; //뉴스 발행 시 날짜에 대한 정보를 가져오려고
    private NoticeManager theNotice;
    private StockManager theStock;

    public GameObject newsGO; //메일함 창 Panel
    public TextMeshProUGUI dateText; //~년 ~월 ~일 표시 Text
    public TextMeshProUGUI contentText; //뉴스 내용 Text
    public TextMeshProUGUI fromWhomText; //뉴스 출처 Text
    public TextMeshProUGUI pageText; //페이지 Text 예) 10/100
    public GameObject previousButton; //이전뉴스 전환버튼
    public GameObject nextButton; //다음뉴스 전환버튼
    public GameObject removeButton; //현재 뉴스 삭제 버튼

    public GameObject newsAlert; //메일함 위 빨간색 알림 Panel
    public TextMeshProUGUI newsAlertText; //알림 Panel Text
    public GameObject newsAlertUpper;
    public TextMeshProUGUI newsAlertUpperText;

    private bool newsArrive = false;

    private List<News> newsList; //메일함 뉴스 List로 저장
    private int newsIndex = -1; //현재 뉴스 index, 없을 경우 -1

    private bool activated = false; //메일함 Panel(newsGO)이 활성화 되었을 경우 true

    private bool newsEffectDelay = false;
    private bool newsEffectActivate = false;
    private float newsEffectCurrentTime = 40f;
    private int newsEffectIndex;
    private float newsEffectOffset;
    private int newsAccuracy;
    private string[] fromWhom = {"운식신문", "구닥다리신문", "믿어봐신문", "구라신문", "여우신문", "상명신문", "대한민국신문", "서강신문", "세종대왕신문", "벼리신문"};
    private int[] fromWhomAccu = {25, 30, 35, 40, 50, 60, 65, 70, 75, 85};
    private bool rightEffect;
    /**
     * @brief
     * pageText 설정하는 함수
     * 메일함이 비었을 경우 ""으로 설정, 있을 경우 "현재페이지/전체페이지"로 설정
     */
    private void SetPage()
    {
        if (newsList.Count <= 0)
        {
            pageText.text = "";
        }
        else
        {
            pageText.text = (newsIndex + 1) + "/" + newsList.Count;
        }
    }

    /**
     * @brief
     * 삭제 버튼 클릭시 실행되는 함수
     */
    public void RemoveOnClick()
    {
        if (newsIndex < 1) //첫 뉴스를 지울 경우
        {
            newsList.RemoveAt(newsIndex);
            newsIndex--;
            LoadLatestNews(); //마지막 뉴스가 나타나도록
        }
        else //첫 뉴스가 아니라면
        {   //전 뉴스를 보여주고 현재 뉴스를 지운다.
            PreviousButtonOnClick();
            newsList.RemoveAt(newsIndex + 1);
        }
        SetAlert();
        SetPage();
    }

    /**
     * @brief
     * 이전버튼 누르면 실행되는 함수
     * 바로 전 뉴스를 Load한다.
     */
    public void PreviousButtonOnClick()
    {
        if (newsIndex > 0) //이전 뉴스가 있는 경우
        {
            newsIndex--;
            dateText.text = newsList[newsIndex].GetDateString(); //날짜
            contentText.text = newsList[newsIndex].content;  //내용
            fromWhomText.text = newsList[newsIndex].fromWhom; //출처
            newsList[newsIndex].notRead = false; //읽음 체크를 해준다.

            SetPage();
            SetAlert();
        }
    }

    /**
     * @brief
     * 다음 버튼을 누를 경우 실행되는 함수
     */
    public void NextButtonOnClick()
    {
        if (newsIndex < newsList.Count - 1) //다음 뉴스가 있을 경우
        {
            newsIndex++;
            dateText.text = newsList[newsIndex].GetDateString(); //날짜
            contentText.text = newsList[newsIndex].content; //내용
            fromWhomText.text = newsList[newsIndex].fromWhom; //출처
            newsList[newsIndex].notRead = false; //읽음 체크를 해준다.

            SetPage();
            SetAlert();
        }
    }

    /**
     * x 버튼을 누르면 메일함을 닫는다
     */
    public void CloseButtonOnClick()
    {
        activated = false;
        newsGO.SetActive(false);
        theDate.Activate();
    }

    /**
     * @brief
     * 메일함 버튼을 누를때 실행되는 함수
     * Panel이 Canvas에 토글된다.
     * */
    public void MailButtonOnClick()
    {
        activated = !activated;

        if (activated)
        {
            LoadLatestNews();
            theDate.NotActivate();
        }
        else
        {
            newsGO.SetActive(false);
            theDate.Activate();
        }
    }

    /**
     * @brief
     * 메일함을 열면 가장 최근에 온 뉴스를 보여준다.
     */
    public void LoadLatestNews()
    {
        newsGO.SetActive(true);

        if (newsList.Count <= 0) //메일함이 비었을 경우
        {
            /* 내용Text를 제외하고 다른 UI는 안보이게 설정 */
            dateText.text = "";
            contentText.text = "메일함이 비었습니다.";
            fromWhomText.text = "";
            previousButton.SetActive(false);
            nextButton.SetActive(false);
            removeButton.SetActive(false);
        }
        else
        {
            newsIndex = newsList.Count - 1; //맨 마지막으로 인덱스를 설정
            dateText.text = newsList[newsIndex].GetDateString(); //날짜Text
            contentText.text = newsList[newsIndex].content; //내용Text
            fromWhomText.text = newsList[newsIndex].fromWhom; //출처Text
            previousButton.SetActive(true);
            nextButton.SetActive(true);
            removeButton.SetActive(true);

            newsList[newsIndex].notRead = false; //읽음 체크를 해준다.
            SetAlert();
        }
        SetPage();
    }

    /**
     * @brief
     * 메일함 버튼 위 조그마한 빨간색 패널과 글씨 설정하는 함수
     * 안 읽은 메일이 몇 개인지 알림 표시 기능을 한다.
     */
    public void SetAlert()
    {
        /* 뉴스가 10개 초과할 경우 자동 삭제 */
        while (newsList.Count > 10)
        {
            newsList.RemoveAt(0);
        }

        int count = 0;

        /* 처음부터 끝까지 안읽은 뉴스 개수를 센다 */
        for (int i = 0; i < newsList.Count; i++)
        {
            if (newsList[i].notRead)
            {
                count++;
            }
        }

        if (count > 0)
        {
            newsAlert.SetActive(true);
            newsAlertUpper.SetActive(true);
            newsAlertText.text = count.ToString();
            newsAlertUpperText.text = count.ToString();
        }
        else //안읽은 뉴스가 없을 경우, Panel을 안보이게 설정한다.
        {
            newsAlert.SetActive(false);
            newsAlertUpper.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        theDate = FindObjectOfType<DateManager>();
        theNotice = FindObjectOfType<NoticeManager>();
        theStock = FindObjectOfType<StockManager>();
        newsList = new List<News>();
        SetAlert();
    }

    // Update is called once per frame
    void Update()
    {
        if (DateManager.activated)
        {
            if (theDate.hour == 6 && !newsArrive) //새로운 뉴스를 하나 받는다.
            {
                newsArrive = true;
                StartCoroutine(DelayCoroutine()); /* TO DO */

                /* Database 안에 있는 뉴스 데이터에서 랜덤으로 뽑아서 newsList에 추가한다. */
                int n = DatabaseManager.newsDataList.Count;
                News new_news = new News(DatabaseManager.newsDataList[Random.Range(0, n)]);

                int who = Random.Range(0, 10);
                /*발행지 랜덤으로 정함*/
                new_news.SetfromWhom(fromWhom[who]);
                /*발행지의 정확도에 대한 계산*/
                if (Random.Range(0, 100) < fromWhomAccu[who])
                    rightEffect = true;
                else
                    rightEffect = false;

                new_news.SetDate(theDate.year, theDate.month, theDate.day);
                newsList.Add(new_news);

                SetAlert();

                theNotice.NotificationAppear("새로운 메일이 도착했습니다. 확인해보세요");

                /* 메일이 발행된 후에 약간의 딜레이를 주고 효력을 발생시킨다 */
                newsEffectDelay = true;
                newsEffectCurrentTime = 15f;
                newsEffectIndex = new_news.whichStock;
                newsEffectOffset = new_news.offset;
                newsAccuracy = new_news.accuracy;
            }
            else if (theDate.hour == 8) /* 더 깔끔하게 수정 예정 */
            {
                newsArrive = false;
            }

            /* 메일이 발행된 후에 약간의 딜레이를 준다 */
            if (newsEffectDelay)
            {
                newsEffectCurrentTime -= Time.deltaTime;

                /* 딜레이가 끝나면 효력을 발생시킨다 */
                if (newsEffectCurrentTime < 0)
                {
                    newsEffectDelay = false;
                    newsEffectActivate = true;
                    newsEffectCurrentTime = 50f;

                    //if (false/*onlyThisType*/)
                    //{
                    //    for (int i = 0; i < theStock.stockType.Length; i++)
                    //    {
                    //        if (theStock.stockType[i/*특정 업종 대상*/] == 0/*특정 업종대상*/)
                    //        {
                    //            theStock.stockSc[i].NewsEffectActivate(newsEffectOffset, newsAccuracy, rightEffect);
                    //        }
                    //    }
                    //}
                    //else
                        theStock.stockSc[newsEffectIndex].NewsEffectActivate(newsEffectOffset, newsAccuracy, rightEffect);

                    Debug.Log("뉴스 효과 시작" + newsEffectIndex + "번 주식에 " + newsEffectOffset + "만큼의 오프셋 적용");
                }
            }
            /* 뉴스 효력 발생 */
            if (newsEffectActivate)
            {
                newsEffectCurrentTime -= Time.deltaTime;

                /* 일정 시간이 끝나면 뉴스 효력 끝 */
                if (newsEffectCurrentTime < 0)
                {
                    newsEffectActivate = false;
                    theStock.stockSc[newsEffectIndex].NewsEffectNotActivate();
                    Debug.Log("뉴스 효과 끝");
                }
            }
        }
    }

    IEnumerator DelayCoroutine()
    {
        yield return new WaitUntil(() => theDate.hour != 7);
    }

    /* 간단한 쪽지를 받는다 (뉴스 연계 안된다) */
    public void SetNewNote(SimpleNote newNote)
    {
        newsList.Add(new News(-1, newNote.fromWhom, newNote.content, -1, -1, 0)); //newNote 타입을 news타입으로 변환
        newsList[newsList.Count - 1].SetDate(theDate.year, theDate.month, theDate.day);
        SetAlert();
        theNotice.NotificationAppear("누군가 쪽지를 보냈습니다.");
    }
}