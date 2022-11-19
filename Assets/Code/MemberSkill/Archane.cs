using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archane : MemberSkill
{
    public override void Go(float fDamage, CardElement eC)
    {
        _fDamage = fDamage;
        _eCE = eC;
        transform.position = new Vector3(0, 0, 2f);
        transform.eulerAngles = new Vector3(90f, 0, 0);
        Destroy(gameObject,1.75f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Unit>().Hit(_fDamage * GameManager.GM.m_cPlayer.m_cAvata.m_fAp, _eCE);
        }
    }
}

