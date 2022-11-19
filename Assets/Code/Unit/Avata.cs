using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avata : Unit
{
    // 플레이어 캐릭터의 정보를 가지는 클래스. Unit을 상속함
    public int m_nCost = 15;
    [Header("Card")]
    public List<Card> m_vecMyCard = new List<Card>();
    //public List<Card> m_vecUseCard = new List<Card>();
    public List<List<Card>> m_vecUseCards = new List<List<Card>>();
    public int m_nUseCardIndex = 0;
    [Header("Member")]
    public List<Member> _vecMyMember = new List<Member>();
    public List<Member> _vecUseMember = new List<Member>();
    [Header("Show Cool Time")]
    public List<Image> m_vecCoolTimeFill = new List<Image>();
    [Header("Set Buff")]
    public List<SetBuff> _vecMyBuff = new List<SetBuff>();

    float m_fCoolTime = 0;
    SkillUseQueue m_qUseingSkill = new SkillUseQueue(9);

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
            case GameType.Defence: MoveDefence(); break;
            case GameType.Laid:
            case GameType.Boss: MoveBoss(); break;
        }
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
        m_vecMyCard.Add(addCard);
    }
    public void GetMember(Member addMember)
    {
        _vecMyMember.Add(addMember);
    }
    public void ReSet()
    {
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
}
