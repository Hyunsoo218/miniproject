using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeglectSin : Sin
{
    public int Neg;
    
    public override void Open()
    {
        base.Open();
        if (Neg == 0)
        {
            Neg = PlayerPrefs.GetInt("Neg");
            if (Neg == 0)
            {
                GameManager.GM.ShowText("��ȭ�� �ֱ������� ���� �� �ִ� ���� Ȯ�� �� �� �ֽ��ϴ�.");
            }
            Neg++;
            PlayerPrefs.SetInt("Neg", Neg);
        }
    }
}
