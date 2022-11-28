using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show10CardSin : Sin
{
    [SerializeField] List<Slot> _vecSlots = new List<Slot>();
    [SerializeField] Text _txtTitle; 
    public override void Open(string strData, List<Card> cCard)
    {
        base.Open(strData, cCard);
        _txtTitle.text = strData;
        for (int i = 0; i < _vecSlots.Count; i++)
        {
            _vecSlots[i].Set(cCard[i]);
        }
    }
    public void ShowBig(int nNum) 
    {
        if (_vecSlots[nNum].m_cCard.m_nCost == -1)
        {
            GameManager.GM.ShowMember("", _vecSlots[nNum].m_cCard);
        }
        else
        {
            GameManager.GM.ShowCard("", _vecSlots[nNum].m_cCard);
        }
    }
}
