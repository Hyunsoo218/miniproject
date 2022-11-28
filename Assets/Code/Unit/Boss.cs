using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Monster
{
    public List<Pattern_Proto> pp = new List<Pattern_Proto>();
    public float attackTime;
    float timer = 0f;

    public Slider m_slHp;
    public override void Respon()
    {
        m_slHp.maxValue = m_fMaxHp;
        m_slHp.value = m_fMaxHp;
        m_fHp = m_fMaxHp;
        transform.position = new Vector3(0, 0, 6.5f);
    }
    public override bool Hit(float fDamage, CardElement CE)
    {
        base.Hit(fDamage, CE);
        tempObj.transform.position += new Vector3(0, 0, -3f);
        m_slHp.value = m_fHp;
        return true;
    }
    public override void Die()
    {
        GameManager.GM.GoStageClear();
    }
    public override void Attack()
    {
        if (pp.Count == 0) return;
        temp = Random.Range(0, pp.Count);
        pp[temp].UsePattern();
    }
    int temp;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= attackTime)
        {
            timer = 0;
            Attack();
        }
    }
}