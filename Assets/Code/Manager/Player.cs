using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    string m_strId, m_strPass;

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
    float _fCreGas = 0;
    float _fNowCreGasTime = 0;
    bool _bCreGas = false;
    private void Start()
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
    }
    public void SetDiamond()
    {
        m_txtDiamond.text = GameManager.GM.GoldToStr(m_nDiamond);
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
