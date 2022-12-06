using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeglectSin : Sin
{
    public Sin NegGuide;
    public int Neg;
    public Text _txtReward;
    public Text _txtBuf1Ea;
    public Text _txtBuf2Ea;
    public int _nBuf1Ea;
    public int _nBuf2Ea;
    private void Start()
    {
        
    }
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
        _nBuf1Ea = PlayerPrefs.GetInt(GameManager.GM.m_cPlayer.userno + "buf1");
        _nBuf2Ea = PlayerPrefs.GetInt(GameManager.GM.m_cPlayer.userno + "buf2");
        if (_nBuf2Ea > 0) BangchiManager._fBoost = 2f;
        SetData();
    }
    public void GetBuf_1() 
    {
        _nBuf1Ea += 100;
        SetData();
    }
    public void GetBuf_2() 
    {
        BangchiManager._fBoost = 2f;
        _nBuf2Ea += 100;
        SetData();
    }
    public void SetData() 
    {
        _txtReward.text = GameManager.GM.GoldToStr(GameManager.GM.m_cPlayer.m_nBangchiGold);
        _txtBuf1Ea.text = "接食 ( " + GameManager.GM.GoldToStr(_nBuf1Ea) + " )";
        _txtBuf2Ea.text = "接食 ( " + GameManager.GM.GoldToStr(_nBuf2Ea) + " )";
        PlayerPrefs.SetInt(GameManager.GM.m_cPlayer.userno + "buf1", _nBuf1Ea);
        PlayerPrefs.SetInt(GameManager.GM.m_cPlayer.userno + "buf2", _nBuf2Ea);
    }
}
