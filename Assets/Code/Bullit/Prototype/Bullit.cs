using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullit : MonoBehaviour
{
    public CardElement m_eCE;
    public float m_fDamage;
    public int m_nTargetEa = 1;
    virtual public void Set(float fDamage)
    {
        m_fDamage = fDamage;
        Destroy(gameObject,5f);
    }
}
public enum CardElement
{
    Fire, Water, Wind, Stone, Light, Dark
}
public enum BullitType
{//  노말   관통 다단히트  범위
    Nomal, Drill, Shower, Storm
}