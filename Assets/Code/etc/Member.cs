using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : Card
{
    
    public Member()
    {
        m_imgImage = _imgFull;
    }

    public MemberSkill MemberSkill
    {
        get => default;
        set
        {
        }
    }

    public override void Use()
    {
        if (_bCanUse == true)
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Pattern");
            for (int i = 0; i < temp.Length; i++)
            {
                Destroy(temp[i]);
            }
            GameManager.GM.GoCutSin(this);

            _bCanUse = false;
        }
    }
}

