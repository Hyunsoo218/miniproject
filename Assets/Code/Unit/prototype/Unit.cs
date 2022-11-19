using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    // 유닛이 가질 정보를 가지는 프로토타입 클래스이다
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