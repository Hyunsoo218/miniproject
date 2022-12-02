using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Sprite m_imgNull;
    public Card m_cCard;
    public Text m_txtCost;
    public Text m_txtLv;
    public Text m_txtUnlimitie;

    public virtual void Set(Card card)
    {
        m_cCard = card;
        if (m_cCard != null)
        {
            if (card.m_nCost == -1)
            {
                gameObject.GetComponent<Button>().image.sprite = card._imgFull;
                m_txtCost.text = "";
                m_txtLv.text = "";
                m_txtUnlimitie.text = "";
            }
            else
            {
                gameObject.GetComponent<Button>().image.sprite = card.m_imgImage;
                m_txtCost.text = "" + card.m_nCost;
                m_txtLv.text = "" + card.m_nLevel;
                if (card.m_nUnlimite > 0)
                {
                    m_txtUnlimitie.text = "+" + card.m_nUnlimite;
                }
                else
                {
                    m_txtUnlimitie.text = "";
                }
            }
        }
        else
        {
            m_txtCost.text = "";
            m_txtLv.text = "";
            m_txtUnlimitie.text = "";
            gameObject.GetComponent<Button>().image.sprite = m_imgNull;
        }
    }
    public void Boxing(Sprite img)
    {
        gameObject.GetComponent<Button>().image.sprite = img;
        m_txtCost.text = "";
        m_txtLv.text = "";
        m_txtUnlimitie.text = "";
    }
}