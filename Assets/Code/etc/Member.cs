using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : Card
{
    
    public Member()
    {
        m_imgImage = _imgFull;
    }
    public override void Use()
    {
        if (_bCanUse == true)
        {
            GameManager.GM.GoCutSin(this);
            _bCanUse = false;
        }
    }
}

