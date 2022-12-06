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
        //    print("�ʱ�ȭ �߽��ϴ�.");
        //}
        //�ʱⰪ���� ���� (�׽�Ʈ �Ҷ� else �κ� �ּ� ó�� ���ָ� ��)
    }
}
