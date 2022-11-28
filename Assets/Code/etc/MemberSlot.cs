using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberSlot : Slot
{
    public override void Set(Card card)
    {
        m_cCard = card;
        if (m_cCard != null)
        {
            gameObject.GetComponent<Button>().image.sprite = card.m_imgImage;
        }
    }
}
