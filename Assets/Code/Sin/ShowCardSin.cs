using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCardSin : Sin
{
    [SerializeField] Slot _cShowSlot;
    [SerializeField] Text _txtTitle;
    [SerializeField] Text _txtAp;
    [SerializeField] Text _txtHp;
    [SerializeField] Text _txtData;
    public override void Open(string strData, Card cCard)
    {
        base.Open(strData, cCard);
        _cShowSlot.Set(cCard);
        _txtTitle.text = strData;
        _txtAp.text = cCard.m_fAp + "";
        _txtHp.text = cCard.m_fHp + "";
        _txtData.text = cCard.m_strData;
    }
}
