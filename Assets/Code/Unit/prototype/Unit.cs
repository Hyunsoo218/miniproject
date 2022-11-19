using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    // ������ ���� ������ ������ ������Ÿ�� Ŭ�����̴�
    public float m_fMaxHp, m_fHp;
    public float m_fAp;
    public float m_fSpeed;
    public GameObject m_objHitText;

    virtual public void Move() { }
    virtual public void Attack() { }
    virtual public bool Hit(float fDamage, CardElement CE) { return true; }
    virtual public void Die() { }
    virtual public void Respon() { }
}