using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Monster
{
    public Slider m_slHp;
    public override void Respon()
    {
        m_slHp.maxValue = m_fMaxHp;
        m_slHp.value = m_fMaxHp;
        m_fHp = m_fMaxHp;
        transform.position = new Vector3(0, -0.1f, 6.5f);
    }

    public override bool Hit(float fDamage, CardElement CE)
    {
        base.Hit(fDamage, CE);
        tempObj.transform.position += new Vector3(0, -0.1f, -3f);
        m_slHp.value = m_fHp;
        return true;
    }
    public override void Die()
    {
        GameManager.GM.GoStageClear();
    }
}
