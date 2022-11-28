using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMemberSin : Sin
{
    [SerializeField] Image _imgShowMember;
    [SerializeField] GameObject _objMemberData;
    [SerializeField] Text _txtTitle;
    [SerializeField] Text _txtName;
    [SerializeField] Text _txtData;
    bool _bMemnerData;
    public override void Open(string strData, Card cMember)
    {
        base.Open(strData, cMember);
        _objMemberData.SetActive(false);
        _bMemnerData = false;
        _imgShowMember.sprite = cMember._imgFull;
        _txtTitle.text = strData;
        _txtName.text = cMember._strName;
        _txtData.text = cMember._strData;
    }
    public void Change()
    {
        if (_bMemnerData)
        {
            _bMemnerData = false;
            _objMemberData.SetActive(false);
        }
        else
        {
            _bMemnerData = true;
            _objMemberData.SetActive(true);
        }
    }
}
