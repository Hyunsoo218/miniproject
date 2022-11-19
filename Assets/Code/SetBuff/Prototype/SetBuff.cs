using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBuff
{
    public SetBuffType _eSBT;
    public string _strName;
    public float _fBoost;
    public SetBuff(SetBuffType eSBT, string strName, float fBoost)
    {
        _eSBT = eSBT;
        _strName = strName;
        _fBoost = fBoost;
    }
}
