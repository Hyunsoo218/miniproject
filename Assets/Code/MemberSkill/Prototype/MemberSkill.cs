using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberSkill : MonoBehaviour
{
    public float _fDamage;
    public CardElement _eCE;
    virtual public void Go(float fDamage, CardElement eC) { }
}
