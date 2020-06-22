/**
 * @brief 뉴스나 쪽지 관리를 쉽게하기 위해 만든 class
 * @date 2020/03/05 마지막수정
 * @file News.cs
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class News
{
    public int newsID; //뉴스 출처에 대한 분류
    public string fromWhom; //뉴스 출처 문자열
    public string content; //뉴스 내용

    public bool notRead; //읽은 뉴스인지 아닌지 기록

    public int whichStock;
    public float offset;
    public int accuracy;

    /// 뉴스 발행 날짜를 저장하기 위한 변수들이다
    int year;
    int month;
    int day;

    /**
     * @brief
     * 생성자 
     * @param 초기화 할 값을 입력받는다.
     */
    public News(int _newsID, string _fromWhom, string _content, int _whichStock, float _offset, int _accuracy)
    {
        notRead = true;
        newsID = _newsID;
        fromWhom = _fromWhom;
        content = _content;
        whichStock = _whichStock;
        offset = _offset;
        accuracy = _accuracy;
        year = 0;
        month = 0;
        day = 0;
    }

    /**
     * @brief
     * 생성자2, 기존 만들어진 객체의 값을 넣어서 마치 복사하는 것처럼
     * @param 기존 만들어진 객체
     */
    public News(News item)
    {
        notRead = true;
        newsID = item.newsID;
        fromWhom = item.fromWhom;
        content = item.content;
        whichStock = item.whichStock;
        offset = item.offset;
        accuracy = item.accuracy;
        year = item.year;
        month = item.month;
        day = item.day;
    }

    /**
     * @brief
     * int로 저장된 날짜를 출력하기 쉽게 string으로 변환하여 반환하는 함수이다.
     * @return
     * '3년 10월 31일' 과 같은 형태로 문자열을 return한다.
     */
    public string GetDateString()
    {
        return year + "년 " + month + "월 " + day + "일";
    }

    /**
     * @brief
     * 뉴스 발행시, 발행 날짜 설정 함수
     * @param 설명 생략
     */
    public void SetDate(int _year, int _month, int _day)
    {
        year = _year;
        month = _month;
        day = _day;
    }

    /*어디서왔든 새로 발행지 설정(newsManager에서)*/
    public void SetfromWhom(string _fromWhom)
    {
        fromWhom = _fromWhom;
    }
}