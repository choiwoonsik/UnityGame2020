using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 효력이 없는 간단한 쪽지 */
public class SimpleNote
{
    public string fromWhom; //누구로부터 왔는가
    public string content; //내용
    private int year; //보낸 날짜
    private int month;
    private int day;
    
    public SimpleNote(string _content)
    {
        content = _content;
    }

    public SimpleNote(SimpleNote newNote)
    {
        content = newNote.content;
    }

    public void SetDate(int _year, int _month, int _day)
    {
        year = _year;
        month = _month;
        day = _day;
    }

    public void SetWhom(string _whom)
    {
        fromWhom = _whom;
    }
}
