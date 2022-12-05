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
                GameManager.GM.ShowText("재화를 주기적으로 얻을 수 있는 것을 확인 할 수 있습니다.");
            }
            Neg++;
            PlayerPrefs.SetInt("Neg", Neg);
        }
    }
}
