using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : MonoBehaviour
{
    public CardElement _eCE;
    public string _strName;
    public string _strData;
    public float _fDamege;
    public bool _bCanUse = true;
    public GameObject _objCutEffect;
    public GameObject _objSkill;
    public Sprite _imgFull;
    public Sprite _imgFace;
    public Sprite _imgCard;
    public Member() { }
    public Member(Member cMember)
    {
        _strName = cMember._strName;
        _fDamege = cMember._fDamege;
        _bCanUse = cMember._bCanUse;
        _objCutEffect = cMember._objCutEffect;
        _imgFull = cMember._imgFull;
        _imgFace = cMember._imgFace;
        _imgCard = cMember._imgCard;
    }
    public void Use()
    {
        if (_bCanUse == true)
        {
            // ����
            GameManager.GM.GoCutSin(this);
            _bCanUse = false;
        }
    }
    public Member Clone()
    {
        return new Member(this);
    }
}

