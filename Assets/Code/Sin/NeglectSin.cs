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
                GameManager.GM.ShowText("��ȭ�� �ֱ������� ���� �� �ִ� ���� Ȯ�� �� �� �ֽ��ϴ�.");
            }
            Neg++;
            PlayerPrefs.SetInt("Neg", Neg);
        }
        if (_bFirst)
        {
            _bFirst = false;

            string strLast = PlayerPrefs.GetString("lastdate" + GameManager.GM.m_cPlayer.userno);
            DateTime dtLast = DateTime.ParseExact(strLast, "yyyy��MM��dd��HH��mm��", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtNow = DateTime.Now;
            TimeSpan tsOffLine = dtNow - dtLast;
            int h = (int)tsOffLine.TotalHours;
            int m = (int)tsOffLine.TotalMinutes;
            _nGetGold = m * GameManager.GM.m_cPlayer.m_nBangchiGold;
            _txtOffLineTime.text = h + "�ð� " + (m % 60) + "��";
            _txtOffLineGold.text = GameManager.GM.GoldToStr(_nGetGold);
            _cOffLineGoldSin.Open();
            print("������ ���� : " + dtLast.ToString("yyyy��MM��dd��HH��mm��"));
            print("���� ���� : " + dtNow.ToString("yyyy��MM��dd��HH��mm��"));
            print((int)tsOffLine.TotalMinutes + "�� ���� ��������");
        }
    }
    
    public void GetGold()
    {
        print("�������� ��� �׳� �ޱ�");
        GameManager.GM.m_cPlayer.m_nGold += _nGetGold;
        _cOffLineGoldSin.Close();
    }
    public void GetGold2()
    {
        print("�������� ��� 2��� �ޱ�");
        GameManager.GM.m_cPlayer.m_nGold += _nGetGold * 2;
        _cOffLineGoldSin.Close();
    }
}
