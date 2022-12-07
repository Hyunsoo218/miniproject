using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avata : Unit
{
    // 플레이어 캐릭터의 정보를 가지는 클래스. Unit을 상속함
    public int m_nCost = 15;
    public int _nLife = 5;
    [Header("Card")]
    public List<Card> m_vecMyCard = new List<Card>();
    //public List<Card> m_vecUseCard = new List<Card>();
    public List<List<Card>> m_vecUseCards = new List<List<Card>>();
    public int m_nUseCardIndex = 0;
    [Header("Member")]
    public List<Card> _vecMyMember = new List<Card>();
    public List<Card> _vecUseMember = new List<Card>();
    [Header("Show Cool Time")]
    public List<Image> m_vecCoolTimeFill = new List<Image>();
    [Header("Set Buff")]
    public List<SetBuff> _vecMyBuff = new List<SetBuff>();
    SkillUseQueue m_qUseingSkill = new SkillUseQueue(9);
    [Header("Game Sin")]
    [SerializeField] Game _cGame;

    RaycastHit _cHit;
    Vector3 _vClick;
    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            m_vecUseCards.Add(new List<Card>());
            for (int j = 0; j < 9; j++)
            {
                m_vecUseCards[i].Add(null);
            }
        }
    }
    private void Update()
    {
        if (GameManager.GM.m_eGS != GameState.Game) return;
        
        Move();
        Attack();
    }
    public override void Attack()
    {
        if (GameManager.GM.m_eGT == GameType.None) return;

        for (int i = 0; i < m_vecUseCards[m_nUseCardIndex].Count; i++)
        {
            if (m_vecUseCards[m_nUseCardIndex][i] != null)
            {
                m_vecCoolTimeFill[i].fillAmount = 1 - m_vecUseCards[m_nUseCardIndex][i].m_fCoolTime / m_vecUseCards[m_nUseCardIndex][i].m_fMaxCoolTime;

                if (m_vecUseCards[m_nUseCardIndex][i].CanUse() == true)  m_qUseingSkill.Add(m_vecUseCards[m_nUseCardIndex][i]); 
                else m_vecUseCards[m_nUseCardIndex][i].CoolTime(); 
            }
        }

        m_qUseingSkill.Update();
    }
    public override void Move()
    {
        switch (GameManager.GM.m_eGT)
        {
            case GameType.TutorialStage:
            case GameType.Defence: MoveDefence(); break;
            case GameType.Laid:
            case GameType.Boss: MoveBoss(); break;
        }
    }
    public override bool Hit(float fDamage=0, CardElement CE = CardElement.Dark)
    {
        _nLife--;
        _cGame.PlayerHit(_nLife);
        if (_nLife <= 0)
        {
            Respon();
        }
        return true;
    }
    public override void Respon()
    {
        GameManager.GM.cSoM.PlayTheme((Themes)4);
        GameManager.GM.GoLose();
    }
    void MoveDefence()
    {
        if (Input.GetMouseButton(0))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _cHit);
            _vClick = _cHit.point;
            transform.position = new Vector3(_vClick.x, 0, _vClick.z);

            if (transform.position.x < -4f)
                transform.position = new Vector3(-4f, 0, transform.position.z);

            else if (transform.position.x > 4f)
                transform.position = new Vector3(4f, 0, transform.position.z);

            if (transform.position.z > -4f)
                transform.position = new Vector3(transform.position.x, 0, -4f);

            else if (transform.position.z < -9f)
                transform.position = new Vector3(transform.position.x, 0, -9f);
        }
    }
    void MoveBoss()
    {
        if (Input.GetMouseButton(0))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _cHit);
            _vClick = _cHit.point;
            transform.position = new Vector3(_vClick.x, 0, _vClick.z);

            if (transform.position.x < -4f)
                transform.position = new Vector3(-4f, 0, transform.position.z);

            else if (transform.position.x > 4f)
                transform.position = new Vector3(4f, 0, transform.position.z);

            if (transform.position.z > 3f)
                transform.position = new Vector3(transform.position.x, 0, 3f);

            else if (transform.position.z < -9f)
                transform.position = new Vector3(transform.position.x, 0, -9f);
        }
    }
    public void GetCard(Card addCard)
    {
        CardData temp = new CardData();

        temp.m_eCardType = (int)addCard.m_eCardType;
        temp.m_eCardRank = (int)addCard.m_eCardRank;
        temp._eMT = (int)addCard._eMT;
        temp.m_nCost = addCard.m_nCost;
        temp.m_nLevel = addCard.m_nLevel;
        temp.m_nUnlimite = addCard.m_nUnlimite;
        temp.m_nLevelUpGold = addCard.m_nLevelUpGold;
        temp.m_fAp = addCard.m_fAp;
        temp.m_nMaxLevel = addCard.m_nMaxLevel;
        temp.m_fHp = addCard.m_fHp;
        temp.cardno = addCard.cardno;

        GameManager.GM.cServer.GetBewCard(temp);
        GameManager.GM.cServer.SetUserCard();
    }
    public void GetMember(Card addMember)
    {
        CardData temp = new CardData();

        temp.m_eCardType = (int)addMember.m_eCardType;
        temp.m_eCardRank = (int)addMember.m_eCardRank;
        temp._eMT = (int)addMember._eMT;
        temp.m_nCost = addMember.m_nCost;
        temp.m_nLevel = addMember.m_nLevel;
        temp.m_nUnlimite = addMember.m_nUnlimite;
        temp.m_nLevelUpGold = addMember.m_nLevelUpGold;
        temp.m_fAp = addMember.m_fAp;
        temp.m_nMaxLevel = addMember.m_nMaxLevel;
        temp.m_fHp = addMember.m_fHp;
        temp.cardno = addMember.cardno;

        GameManager.GM.cServer.GetBewCard(temp);
        GameManager.GM.cServer.SetUserCard();
    }
    public void ReSet()
    {
        _nLife = 5;
        List<Card> vecUseCards = m_vecUseCards[m_nUseCardIndex];
        for (int i = 0; i < vecUseCards.Count; i++)
        {
            if (vecUseCards[i] == null) { }
            else
            {
                vecUseCards[i].m_fCoolTime = 1000f;
            }
        }
        transform.position = new Vector3(0, 0, -7f);
        m_qUseingSkill.Clear();
        for (int i = 0; i < _vecUseMember.Count; i++)
        {
            if (_vecUseMember[i] != null)
            {
                _vecUseMember[i]._bCanUse = true;
            }
        }
    }
    public void GetBuff(SetBuff cSB)
    {
        _vecMyBuff.Add(cSB);
    }
    public void RemoveCard(Card delCard)
    {
        GameManager.GM.cServer.DeleteCard(delCard.cardno);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pattern")
        {
            Hit();
        }
    }
}
