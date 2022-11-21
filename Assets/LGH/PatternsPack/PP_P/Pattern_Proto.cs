using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern_Proto : MonoBehaviour
{

    public GameObject p_goBlock;
    public float p_fSpeed;
    public float p_fDamage;
    virtual public void UsePattern() { }
}
public enum PatternElement
{
    Fire, Water, Wind, Stone, Light, Dark
}