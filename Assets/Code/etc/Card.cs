using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Card : Bullit
{
    public GameObject m_objBullit;
    public Sprite m_imgImage;
    public BullitType m_eBT;
    public CardType m_eCardType;    //
    public CardRank m_eCardRank;    //
    public CardElement m_eCardElement;
    public int m_nCost;             //
    public int m_nLevel = 1;        //
    public int m_nMaxLevel = 30;    //
    public int m_nUnlimite = 0;     //
    public long m_nLevelUpGold = 10;//
    public string m_strData;
    public float m_fAp = 3;         //
    public float m_fHp = 10;        //
    public float m_fDamege;
    public float m_fMaxCoolTime;
    public float m_fCoolTime;
    public bool _bCanUse = true;
    /// //////////////////////////////////////////
    public CardElement _eCE;
    public string _strName;
    public string _strData;
    public float _fDamege;
    public GameObject _objCutEffect;
    public GameObject _objSkill;
    public Sprite _imgFull;
    public Sprite _imgFace;
    public Sprite _imgCard;
    public MemberType _eMT;         //
    public int cardno;         //
    public string _strScript;

    private void Start()
    {
        if (m_objBullit != null)
        {
            m_eCardElement = m_objBullit.GetComponent<Bullit>().m_eCE;
        }
    }
    public bool CanUse()
    {
        if (m_fCoolTime >= m_fMaxCoolTime)
        {
            return true;
        }
        return false;
    }
    public virtual void Use()
    {
        if (m_fCoolTime >= m_fMaxCoolTime)
        {
            m_fCoolTime = 0;
            Attack(GameManager.GM.m_cPlayer.m_cAvata.m_fAp);
        }
    }
    public void CoolTime()
    {
        m_fCoolTime += Time.deltaTime;
    }
    public void Attack(float fPlayerAp)
    {
        // m_objBullit 발사 m_cPlayer
        GameObject tenp = Instantiate(m_objBullit, GameManager.GM.m_cPlayer.m_cAvata.transform.position, Quaternion.Euler(90, 0, 0));

        tenp.GetComponent<Bullit>().Set(fPlayerAp * m_fDamege);
    }
    public bool LvUp()
    {
        if (m_nLevel < m_nMaxLevel)
        {
            m_nLevel++;

            m_fAp = m_fAp * 1.1f;
            m_fHp = m_fHp * 1.1f;

            if (m_nLevel % 10 == 0) m_nLevelUpGold = m_nLevelUpGold * 5;
            else m_nLevelUpGold = (long)(m_nLevelUpGold * 1.3);

            GameManager.GM.cServer.UpdateCard(this);

            return true;
        }
        else
        {
            GameManager.GM.ShowText("최대 레벨 입니다!");
        }
        return false;
    }
    public void Copy(Card cCard)
    {
        m_nLevel = cCard.m_nLevel;
        m_nLevelUpGold = cCard.m_nLevelUpGold;
        m_fAp = cCard.m_fAp;
        m_fHp = cCard.m_fHp;
    }
}
