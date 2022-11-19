using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBuffManager 
{
    private SetBuffManager() { }
    static public void Set(List<Slot> _vecDeck)
    {
        GameManager.GM.m_cPlayer.m_cAvata._vecMyBuff.Clear();
        int nFire = 0;
        int nWater = 0;
        int nWind = 0;
        int nStone = 0;
        int nLight = 0;
        int nDark = 0;
        for (int i = 0; i < _vecDeck.Count; i++)
        {
            if (_vecDeck[i].m_cCard != null)
            {
                Chack(i, _vecDeck);
                switch (GetCardElement(_vecDeck[i].m_cCard))
                {
                    case CardElement.Fire:  nFire++; break;
                    case CardElement.Water: nWater++; break;
                    case CardElement.Wind:  nWind++; break;
                    case CardElement.Stone: nStone++; break;
                    case CardElement.Light: nLight++; break;
                    case CardElement.Dark:  nDark++; break;
                }
            }
        }
        //////////////////////////////////////////////////////////////////////
        /// 다 속성
        if (nFire > 0 && nWater > 0 && nWind > 0 && nStone > 0 && nLight > 0 && nDark > 0)
        {
            GetBuffPlayer("그랜드 마스터", 0.5f);
        }
        if (nFire > 1 && nWater > 1 && nWind > 1 && nStone > 1) // 9 + 6 * 8 = 57
        {
            GetBuffPlayer("엘리멘탈 마스터", 0.6f);
        }
        ////////////////////////////////////////////////////////////////
        /// 2속성
        if (nFire > 3 && nDark > 3)
        {
            GetBuffPlayer("흑염 술사", 0.5f);
        }
        if (nFire > 3 && nLight > 3)
        {
            GetBuffPlayer("백염 술사", 0.5f);
        }
        //////////////////////////////////////////////////////////////////////
        if (nWater > 3 && nLight > 3)
        {
            GetBuffPlayer("여명의 빛", 0.5f);
        }
        if (nWater > 3 && nDark > 3)
        {
            GetBuffPlayer("심해의 포식자", 0.5f);
        }
        ///////////////////////////////////////////////////////////////
        if (nStone > 3 && nDark > 3)
        {
            GetBuffPlayer("지옥의 군주", 0.5f);
        }
        if (nStone > 3 && nLight > 3)
        {
            GetBuffPlayer("천계의 대천사", 0.5f);
        }
        //////////////////////////////////////////////////////////////
        if (nLight > 3 && nDark > 3)
        {
            GetBuffPlayer("음양의 조율자", 0.6f);
        }

        float fBoost = 1;
        for (int i = 0; i < GameManager.GM.m_cPlayer.m_cAvata._vecMyBuff.Count; i++)
        {
            fBoost += GameManager.GM.m_cPlayer.m_cAvata._vecMyBuff[i]._fBoost;
        }
        GameManager.GM.m_cPlayer.m_cAvata.m_fAp *= fBoost;
    }
    static void Chack(int nIndex, List<Slot> _vecDeck)
    {
        switch (nIndex)
        {
            case 0: ChackSlot_0(_vecDeck); break;
            case 1: ChackSlot_1(_vecDeck); break;
            case 2: ChackSlot_2(_vecDeck); break;
            case 3: ChackSlot_3(_vecDeck); break;
            case 6: ChackSlot_6(_vecDeck); break;
        }
    }
    static void ChackSlot_0(List<Slot> _vecDeck)
    {
        if (_vecDeck[1].m_cCard != null && _vecDeck[2].m_cCard != null)
        {
            CardElement card_0 = GetCardElement(_vecDeck[0].m_cCard);
            CardElement card_1 = GetCardElement(_vecDeck[1].m_cCard);
            CardElement card_2 = GetCardElement(_vecDeck[2].m_cCard);
            if (card_0 == card_1 && card_0 == card_2)
            {
                GetBuffPlayer(card_0, "마스터", 0.1f);
            }
        }
        if (_vecDeck[3].m_cCard != null && _vecDeck[6].m_cCard != null)
        {
            CardElement card_0 = GetCardElement(_vecDeck[0].m_cCard);
            CardElement card_3 = GetCardElement(_vecDeck[3].m_cCard);
            CardElement card_6 = GetCardElement(_vecDeck[6].m_cCard);
            if (card_0 == card_3 && card_0 == card_6)
            {
                GetBuffPlayer(card_0, "마스터", 0.1f);
            }
        }
        if (_vecDeck[4].m_cCard != null && _vecDeck[8].m_cCard != null)
        {
            CardElement card_0 = GetCardElement(_vecDeck[0].m_cCard);
            CardElement card_4 = GetCardElement(_vecDeck[4].m_cCard);
            CardElement card_8 = GetCardElement(_vecDeck[8].m_cCard);
            if (card_0 == card_4 && card_0 == card_8)
            {
                GetBuffPlayer(card_0, "마스터", 0.1f);
            }
        }
    }
    static void ChackSlot_1(List<Slot> _vecDeck)
    {
        if (_vecDeck[4].m_cCard != null && _vecDeck[7].m_cCard != null)
        {
            CardElement card_1 = GetCardElement(_vecDeck[1].m_cCard);
            CardElement card_4 = GetCardElement(_vecDeck[4].m_cCard);
            CardElement card_7 = GetCardElement(_vecDeck[7].m_cCard);
            if (card_1 == card_4 && card_1 == card_7)
            {
                GetBuffPlayer(card_1, "마스터", 0.1f);
            }
        }
    }
    static void ChackSlot_2(List<Slot> _vecDeck)
    {
        if (_vecDeck[4].m_cCard != null && _vecDeck[6].m_cCard != null)
        {
            CardElement card_2 = GetCardElement(_vecDeck[2].m_cCard);
            CardElement card_4 = GetCardElement(_vecDeck[4].m_cCard);
            CardElement card_6 = GetCardElement(_vecDeck[6].m_cCard);
            if (card_2 == card_4 && card_2 == card_6)
            {
                GetBuffPlayer(card_2, "마스터", 0.1f);
            }
        }
        if (_vecDeck[5].m_cCard != null && _vecDeck[8].m_cCard != null)
        {
            CardElement card_2 = GetCardElement(_vecDeck[2].m_cCard);
            CardElement card_5 = GetCardElement(_vecDeck[5].m_cCard);
            CardElement card_8 = GetCardElement(_vecDeck[8].m_cCard);
            if (card_2 == card_5 && card_2 == card_8)
            {
                GetBuffPlayer(card_2, "마스터", 0.1f);
            }
        }
    }
    static void ChackSlot_3(List<Slot> _vecDeck)
    {
        if (_vecDeck[4].m_cCard != null && _vecDeck[5].m_cCard != null)
        {
            CardElement card_3 = GetCardElement(_vecDeck[3].m_cCard);
            CardElement card_4 = GetCardElement(_vecDeck[4].m_cCard);
            CardElement card_5 = GetCardElement(_vecDeck[5].m_cCard);
            if (card_3 == card_4 && card_3 == card_5)
            {
                GetBuffPlayer(card_3, "마스터", 0.1f);
            }
        }
    }
    static void ChackSlot_6(List<Slot> _vecDeck)
    {
        if (_vecDeck[7].m_cCard != null && _vecDeck[8].m_cCard != null)
        {
            CardElement card_6 = GetCardElement(_vecDeck[6].m_cCard);
            CardElement card_7 = GetCardElement(_vecDeck[7].m_cCard);
            CardElement card_8 = GetCardElement(_vecDeck[8].m_cCard);
            if (card_6 == card_7 && card_6 == card_8)
            {
                GetBuffPlayer(card_6, "마스터", 0.1f);
            }
        }
    }
    static CardElement GetCardElement(Card cCard)
    {
        return cCard.m_objBullit.GetComponent<Bullit>().m_eCE;
    }
    static void GetBuffPlayer(CardElement eCE, string strName, float fBoost)
    {
        string strElement = "";
        SetBuffType eSBT = SetBuffType.ApUp_Dark;
        switch (eCE)
        {
            case CardElement.Fire:
                strElement = "화염의 ";    eSBT = SetBuffType.ApUp_Fire;
                break;
            case CardElement.Water:
                strElement = "유수의 ";    eSBT = SetBuffType.ApUp_Water;
                break;
            case CardElement.Wind:
                strElement = "폭풍의 ";    eSBT = SetBuffType.ApUp_Wind;
                break;
            case CardElement.Stone:
                strElement = "대지의 ";    eSBT = SetBuffType.ApUp_Stone;
                break;
            case CardElement.Light:
                strElement = "광명의 ";    eSBT = SetBuffType.ApUp_Light;
                break;
            case CardElement.Dark:
                strElement = "칠흑의 ";    eSBT = SetBuffType.ApUp_Dark;
                break;
        }
        int nCount = 1;
        for (int i = 0; i < GameManager.GM.m_cPlayer.m_cAvata._vecMyBuff.Count; i++)
        {
            if (GameManager.GM.m_cPlayer.m_cAvata._vecMyBuff[i]._eSBT == eSBT)
            {
                nCount++;
            }
        }
        GameManager.GM.GetBuff(new SetBuff(eSBT, strElement + strName + " " + nCount + "/8", fBoost));
    }
    static void GetBuffPlayer(string strName, float fBoost)
    {
        GameManager.GM.GetBuff(new SetBuff(SetBuffType.AuUp_AllElement, strName, fBoost));
    }
}
public enum SetBuffType
{
    AuUp_AllElement,
    ApUp_Fire,
    ApUp_Water,
    ApUp_Wind,
    ApUp_Stone,
    ApUp_Light,
    ApUp_Dark,
}