using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string m_strId, m_strPass, m_strName;
    public int userno;
    public Avata m_cAvata;
    public long m_nGold;     // 골드
    public int m_nDiamond;  // 다이아
    public int m_nGas;      // 피로도
    float m_fGasTimer = 10f;
    [Header("Text")]
    public Text m_txtGold;
    public Text m_txtDiamond;
    public Text m_txtGas;
    public Text m_txtGasTimer;
    [Header("State")]
    public bool _bFirst = false; //
    float _fCreGas = 0;
    float _fNowCreGasTime = 0;
    bool _bCreGas = false;

    public override string ToString()
    {
        string data = "";

        data += "userno : " + userno + "\n";
        data += "m_strId : " + m_strId + "\n";
        data += "m_strPass : " + m_strPass + "\n";
        data += "m_strName : " + m_strName + "\n";
        data += "m_nGold : " + m_nGold + "\n";
        data += "m_nDiamond : " + m_nDiamond + "\n";
        data += "m_nGas : " + m_nGas + "\n";
        data += "_bFirst : " + _bFirst + "\n";

        return data;
    }
    private void Start()
    {
        SetGold();
        SetDiamond();
        SetGas();
    }
    public void AllSet() 
    {
        SetGold();
        SetDiamond();
        SetGas();
    }
    public bool UseGold(long Gold)
    {
        if (m_nGold >= Gold)
        {
            m_nGold -= Gold;
            SetGold();
            return true;
        }
        return false;
    }
    public bool UseDiamond(int Diamond)
    {
        if (m_nDiamond >= Diamond)
        {
            m_nDiamond -= Diamond;
            SetDiamond();
            return true;
        }
        return false;
    }
    public bool UseGas(int Gas)
    {
        if (m_nGas >= Gas)
        {
            m_nGas -= Gas;
            SetGas();
            return true;
        }
        return false;
    }
    public void SetGold()
    {
        m_txtGold.text = GameManager.GM.GoldToStr(m_nGold);
        GameManager.GM.cServer.goldSend(m_nGold);
    }
    public void SetDiamond()
    {
        m_txtDiamond.text = GameManager.GM.GoldToStr(m_nDiamond);
        GameManager.GM.cServer.diaSend(m_nDiamond);
    }
    void SetGas()
    {
        if (m_nGas < 100 && _bCreGas == false)
        {
            _bCreGas = true;
            m_txtGasTimer.gameObject.SetActive(true);
            StartCoroutine(CreGas());
        }
        else if(m_nGas >= 100)
        {
            _bCreGas = false;
        }
        m_txtGas.text = m_nGas.ToString();
    }
    void SetGasTimer()
    {
        float temp = m_fGasTimer - _fNowCreGasTime;
        int min = (int)(temp / 60);
        int sec = (int)(temp % 60);
        m_txtGasTimer.text = min + ":" + sec;
    }
    IEnumerator CreGas()
    {
        _fNowCreGasTime = 0;

        while (m_nGas < 100)
        {
            yield return new WaitForSeconds(1f);
            _fNowCreGasTime++;
            if (_fNowCreGasTime >= m_fGasTimer)
            {
                _fNowCreGasTime = 0;
                m_nGas++;
                SetGas();
            }
            SetGasTimer();
        }
        m_txtGasTimer.gameObject.SetActive(false);
    }
}
