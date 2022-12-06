using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeglectSin : Sin
{
    public Sin NegGuide;
    public int Neg;
    
    public override void Open()
    {
        base.Open();
        if (Neg == 0)
        {
            Neg = PlayerPrefs.GetInt(GameManager.GM.m_cPlayer.userno +"Neg");
            if (Neg == 0)
            {
                NegGuide.Open();
            }
            Neg++;
            PlayerPrefs.SetInt(GameManager.GM.m_cPlayer.userno +"Neg", Neg);
        }
        //else
        //{
        //    Neg = 0;
        //    PlayerPrefs.SetInt("Neg", Neg);
        //    print("초기화 했습니다.");
        //}
        //초기값으로 설정 (테스트 할때 else 부분 주석 처리 없애면 됨)
    }
}
