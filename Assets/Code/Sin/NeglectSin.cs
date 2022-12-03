using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeglectSin : Sin
{
    public int Neg;
    [SerializeField] bool _bFirst = true;
    [SerializeField] Sin _cOffLineGoldSin;
    [SerializeField] Text _txtOffLineTime;
    [SerializeField] Text _txtOffLineGold;
    long _nGetGold;
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
        if (_bFirst)
        {
            _bFirst = false;

            string strLast = PlayerPrefs.GetString("lastdate" + GameManager.GM.m_cPlayer.userno);
            DateTime dtLast = DateTime.ParseExact(strLast, "yyyy년MM월dd일HH시mm분", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtNow = DateTime.Now;
            TimeSpan tsOffLine = dtNow - dtLast;
            int h = (int)tsOffLine.TotalHours;
            int m = (int)tsOffLine.TotalMinutes;
            _nGetGold = m * GameManager.GM.m_cPlayer.m_nBangchiGold;
            _txtOffLineTime.text = h + "시간 " + (m % 60) + "분";
            _txtOffLineGold.text = GameManager.GM.GoldToStr(_nGetGold);
            _cOffLineGoldSin.Open();
            print("마지막 접속 : " + dtLast.ToString("yyyy년MM월dd일HH시mm분"));
            print("현재 접속 : " + dtNow.ToString("yyyy년MM월dd일HH시mm분"));
            print((int)tsOffLine.TotalMinutes + "분 동안 오프라인");
        }
    }
    
    public void GetGold()
    {
        print("오프라인 골드 그냥 받기");
        GameManager.GM.m_cPlayer.m_nGold += _nGetGold;
        _cOffLineGoldSin.Close();
    }
    public void GetGold2()
    {
        print("오프라인 골드 2배로 받기");
        GameManager.GM.m_cPlayer.m_nGold += _nGetGold * 2;
        _cOffLineGoldSin.Close();
    }
}
