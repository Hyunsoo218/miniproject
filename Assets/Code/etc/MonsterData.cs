using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterData : MonoBehaviour
{
    public Image m_imgImage;
    public Text m_txtName;
    public Text m_txtElemant;
    public void Set(Monster cMonster)
    {
        m_imgImage.sprite = cMonster.m_imgImage;
        m_txtName.text = cMonster.m_strName;
        switch (cMonster.m_eElement)
        {
            case CardElement.Fire:
                m_txtElemant.text = "ºÒ";
                break;
            case CardElement.Water:
                m_txtElemant.text = "¹°";
                break;
            case CardElement.Wind:
                m_txtElemant.text = "¹Ù¶÷";
                break;
            case CardElement.Stone:
                m_txtElemant.text = "¶¥";
                break;
            case CardElement.Light:
                m_txtElemant.text = "ºû";
                break;
            case CardElement.Dark:
                m_txtElemant.text = "¾îµÒ";
                break;
        }
    }
}
