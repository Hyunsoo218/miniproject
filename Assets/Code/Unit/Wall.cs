using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wall : Unit
{
    public Slider m_slHp;

    public override bool Hit(float fDamage, CardElement CE)
    {
        m_fHp -= fDamage;
        m_slHp.value = m_fHp;

        if (m_fHp <= 0)
        {
            GameManager.GM.GoLose();
        }
        return true;
    }
    public override void Respon()
    {
        gameObject.SetActive(true);
        m_fMaxHp = GameManager.GM.m_cPlayer.m_cAvata.m_fMaxHp * 10f;
        m_slHp.maxValue = m_fMaxHp;
        m_slHp.value = m_fMaxHp;
        m_fHp = m_fMaxHp;
    }
}
