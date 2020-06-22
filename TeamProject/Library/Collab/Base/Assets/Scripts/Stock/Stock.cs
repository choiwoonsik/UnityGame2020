using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stock : MonoBehaviour
{
    public NoticeManager theNotice;

    //주식번호
    public int stockNum;

    //주식이름, 가격, 변동가 Text
    public TextMeshProUGUI stockNameT;
    public TextMeshProUGUI stockPriceT;
    public TextMeshProUGUI varPrice;
    public TextMeshProUGUI varPercent;
    
    //주식가격, 변동성, 변동가 변수
    public double thisStockPrice;
    public double averagePrice;
    public double ratingPrice;

    /*변동가격 체크*/
    private double upDownPrice;
    private double upDownPercent;
    private int upDownCheck;

    //뉴스에 영향 받는 주식
    private bool newsEffectActivated = false;
    private float newsEffectOffset;
    private int newsAccuracy;
    private bool rightEffect;

    //기타
    private int previousHour = -1;
    private DateManager theDate;

    int CheckError = 0;

    /*주식 목록 관리*/
    public string[,] stockNamePrice = new string[10, 2]
        {{"운식", 100000000.ToString()}, {"벼리", 30.ToString()},
        {"태연", 80.ToString()}, {"현아", 100.ToString()},
        {"이유", 130.ToString()}, {"보영", 200.ToString()},
        {"연아", 300.ToString()}, {"태리", 500.ToString()},
        {"다현", 700.ToString()}, {"슬기", 900.ToString()}};

    //stockNumber는 후에 외부에서 전달 예정 // ex) 신문, 쪽지등에서 특정 주식 언급시 해당 주식 번호를 전달
    //모든 주식의 가격 결정 함수, 변동가또한 offset을 통해 결정
    //offset의 범위는 -0.4 ~ + 0.4로 설정
    public void StockPriceManager(float offset, int N)
    {

        /*offset의 역활 - 등락의 정도 조절*/
        /*N의 역활 - 정확도 조절*/
        /*N의 범위 -55 ~ 45*/
        /* -55가 오면 100% 하락 <-> 45가오면 100% 상승 */
        /* ex) 정확도 75%의 상승예상 뉴스의 경우 N의 값으로 20이 오면됀다*/
        /* 상승은 45 - N 보다 큰 숫자가 0~100에서 나올 경우이므로 25보다 큰수가 나오면 75프로 확률 그러므로, N은 20으로 설정 */


        /* -0.4 ~~ +0.4의 값 * 100 */
        offset *= 100;

        /*가끔 나오는 복불복*/
        int JackPot = Random.Range(-777, 778);

        if (JackPot == 777)
            offset = 40;

        if (JackPot == -777)
            offset = -40;

        /* 0 ~ 99 의 값을 랜덤하게 뽑는다 */
        upDownCheck = Random.Range(0, 100);

        //기본 기준은45, 45%확률 하락 55%확률 상승 기본치를 적용

        //하락장
        if (upDownCheck < 45 - N)
        {
            if (offset > 0)
            {
                CheckError += 1;
                //Debug.Log("양수offset인데도 하락:" + upDownCheck+ "  /  "+ CheckError);
            }

            if (offset < 0)
                offset *= -1;

            /* 주식 가격이 10달러 이하일 경우*/
            if(thisStockPrice <= 10)
            {
                upDownPrice = thisStockPrice * Random.Range(0, 2) / 100;
                upDownPercent = upDownPrice / thisStockPrice * 100;
                thisStockPrice -= upDownPrice;
            }
            /*10달러 보다 높은 경우*/
            else
            {
                /*정상적인 경우 offset 그대로 적용*/
                if (N <= 0)
                    N = 0;
                else
                /*잘못 들어온 경우 offset 반만 적용*/
                    N = 1;
                upDownPrice = thisStockPrice * Random.Range(0, (int)offset - (N * offset/2)) / 100;
                upDownPercent = upDownPrice / thisStockPrice * 100;
                thisStockPrice -= upDownPrice;
            }

            if (upDownPrice / 10000 >= 1)
            {
                varPercent.text = "<color=#0000FF>" + "- " + string.Format("{0}", upDownPercent.ToString("F0")) + "</color>" + "%";
                varPrice.text = "<color=#0000FF>" + "- " + string.Format("{0}", (upDownPrice / 10000).ToString("n1") + "만" + "</color>");
            }
            else
            {
                varPercent.text = "<color=#0000FF>" + "- " + string.Format("{0}", upDownPercent.ToString("F0")) + "</color>" + "%";
                varPrice.text = "<color=#0000FF>" + "- " + string.Format("{0}", upDownPrice.ToString("n1") + "</color>");
            }

            upDownPercent *= -1;
        }


        //상승장
        else if(upDownCheck > 45 - N)
        {
            if (offset < 0)
            {
                CheckError += 1;
                //Debug.Log("음수offset인데도 상승:" + upDownCheck+"  /  "+CheckError);
            }

            if (offset < 0)
                offset *= -1;

            /*정상적인 경우 offset 그대로 적용*/
            if (N >= 0)
                N = 0;
            else
                /*잘못 들어온 경우 offset 반만 적용*/
                N = 1;

            upDownPrice = thisStockPrice * Random.Range(0, (int)offset + (N * offset/2)) / 100;
            upDownPercent = upDownPrice / thisStockPrice * 100;
            thisStockPrice += upDownPrice;

            if (upDownPrice / 10000 >= 1)
            {
                varPercent.text = "<color=#FF0000>" + "+ " + string.Format("{0}", upDownPercent.ToString("F0")) + "</color>" + "%";
                varPrice.text = "<color=#FF0000>" + "+ " + string.Format("{0}", (upDownPrice / 10000).ToString("n1") + "만" + "</color>");
            }
            else
            {
                varPercent.text = "<color=#FF0000>" + "+ " + string.Format("{0}", upDownPercent.ToString("F0")) + "</color>" + "%";
                varPrice.text = "<color=#FF0000>" + "+ " + string.Format("{0}", upDownPrice.ToString("n1") + "</color>");
            }
        }

        if (thisStockPrice / 10000 >= 1)
            stockPriceT.text = "$" + string.Format("{0}", (thisStockPrice / 10000).ToString("n1")) + "만";
        else
            stockPriceT.text = "$" + string.Format("{0}", thisStockPrice.ToString("n1"));
    }
    

    /* 뉴스 효력을 발생시킨다 */
    public void NewsEffectActivate(float offset, int accuracy, bool fromWhomAccu)
    {
        newsEffectActivated = true;
        newsEffectOffset = offset;
        newsAccuracy = accuracy;
        rightEffect = fromWhomAccu;
    }

    /* 뉴스 효력을 무력화 한다 */
    public void NewsEffectNotActivate()
    {
        newsEffectActivated = false;
    }

    void Start()
    {
        theDate = FindObjectOfType<DateManager>();
        stockNameT.text = stockNamePrice[stockNum, 0];
        thisStockPrice = int.Parse(stockNamePrice[stockNum, 1]);
    }

    void Update()
    {
        if (DateManager.activated) //시간이 흐르고 있을 때
        {
            if (previousHour != theDate.hour) //게임 시간 상 1시간만다 업데이트 한다.
            {
                previousHour = theDate.hour;

                /* 뉴스 효력이 없을 경우, offset을 랜덤으로 설정한다 */
                if (!newsEffectActivated)
                {
                    float news = Random.Range(-0.35f, 0.35f);

                    //등락이 좁은 확률이 높은 확률로 나오고 등락이 큰 확률이 낮은 확률로 나오게 한다

                    //0.8의 값 범위중 0.2범위가 나오면 다시 0.8의 범위중에서 뽑음
                    if ((-0.35f <= news && news <= -0.3f) || (0.3f <= news && news <= 0.35f))
                        news = Random.Range(-0.35f, 0.35f);

                    //0.8의 값 범위중 0.6범위가 나오면 0.2의 범위중에서 뽑음
                    /*변동률을 줄이는 역활*/
                    else if (-0.3f <= news && news <= 0.3f)
                        news = Random.Range(-0.06f, 0.06f);

                    if (news < 0)
                        /*45-(-10)이므로 50%확률*/
                        StockPriceManager(news, -5);
                    
                    else if (news > 0)
                        /*45 - 10 이므로 55%확률*/
                        StockPriceManager(news, 0);
                }
                else /*뉴스 효력이 있을 경우 뉴스에 설정된 offset으로 설정한다 */
                {
                    if(rightEffect)
                        StockPriceManager(newsEffectOffset, newsAccuracy);

                    else if (!rightEffect)
                    {
                        float news = Random.Range(-0.05f, 0.05f);

                        if (news < 0)
                            /*45 - (-5)이므로 50%확률*/
                            StockPriceManager(news, -5);

                        else if (news > 0)
                            /*45 - 0 이므로 55%확률*/
                            StockPriceManager(news, 0);
                    }
                }
            }
        }
    }
}
