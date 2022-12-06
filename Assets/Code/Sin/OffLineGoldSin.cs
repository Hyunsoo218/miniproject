using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffLineGoldSin : Sin
{
    [SerializeField] Text _txtOffLineTime;
    [SerializeField] Text _txtOffLineGold;
    bool _bFirst = true;
    long _nGetGold;
    public override void Open()
    {
        if (_bFirst)
        {
            try
            {
                _bFirst = false;
                string strLast = PlayerPrefs.GetString("lastdate" + GameManager.GM.m_cPlayer.userno);
                if (strLast == null) return;
                DateTime dtLast = DateTime.ParseExact(strLast, "yyyy년MM월dd일HH시mm분", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtNow = DateTime.Now;
                TimeSpan tsOffLine = dtNow - dtLast;
                int h = (int)tsOffLine.TotalHours;
                int m = (int)tsOffLine.TotalMinutes;
                _nGetGold = m * GameManager.GM.m_cPlayer.m_nBangchiGold;
                if (_nGetGold == 0) return;
                int gas = (int)m * 10;
                GameManager.GM.m_cPlayer.m_nGas += gas;
                if (GameManager.GM.m_cPlayer.m_nGas > 100) GameManager.GM.m_cPlayer.m_nGas = 100;
                _txtOffLineTime.text = h + "시간 " + (m % 60) + "분";
                _txtOffLineGold.text = GameManager.GM.GoldToStr(_nGetGold);
                print("마지막 접속 : " + dtLast.ToString("yyyy년MM월dd일HH시mm분"));
                print("현재 접속 : " + dtNow.ToString("yyyy년MM월dd일HH시mm분"));
                print((int)tsOffLine.TotalMinutes + "분 동안 오프라인");
                base.Open();
            }
            catch (Exception)
            {

            }
            
        }
    }
    public void GetGold()
    {
        print("오프라인 골드 그냥 받기");
        GameManager.GM.m_cPlayer.m_nGold += _nGetGold;
        GameManager.GM.m_cPlayer.SetGold();
        Close();
    }
    public void GetGold2()
    {
        print("오프라인 골드 2배로 받기");
        GameManager.GM.m_cPlayer.m_nGold += _nGetGold * 2;
        GameManager.GM.m_cPlayer.SetGold();
        Close();
    }
}
