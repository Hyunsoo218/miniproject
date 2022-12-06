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
                DateTime dtLast = DateTime.ParseExact(strLast, "yyyy��MM��dd��HH��mm��", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtNow = DateTime.Now;
                TimeSpan tsOffLine = dtNow - dtLast;
                int h = (int)tsOffLine.TotalHours;
                int m = (int)tsOffLine.TotalMinutes;
                _nGetGold = m * GameManager.GM.m_cPlayer.m_nBangchiGold;
                if (_nGetGold == 0) return;
                int gas = (int)m * 10;
                GameManager.GM.m_cPlayer.m_nGas += gas;
                if (GameManager.GM.m_cPlayer.m_nGas > 100) GameManager.GM.m_cPlayer.m_nGas = 100;
                _txtOffLineTime.text = h + "�ð� " + (m % 60) + "��";
                _txtOffLineGold.text = GameManager.GM.GoldToStr(_nGetGold);
                print("������ ���� : " + dtLast.ToString("yyyy��MM��dd��HH��mm��"));
                print("���� ���� : " + dtNow.ToString("yyyy��MM��dd��HH��mm��"));
                print((int)tsOffLine.TotalMinutes + "�� ���� ��������");
                base.Open();
            }
            catch (Exception)
            {

            }
            
        }
    }
    public void GetGold()
    {
        print("�������� ��� �׳� �ޱ�");
        GameManager.GM.m_cPlayer.m_nGold += _nGetGold;
        GameManager.GM.m_cPlayer.SetGold();
        Close();
    }
    public void GetGold2()
    {
        print("�������� ��� 2��� �ޱ�");
        GameManager.GM.m_cPlayer.m_nGold += _nGetGold * 2;
        GameManager.GM.m_cPlayer.SetGold();
        Close();
    }
}
