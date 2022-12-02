using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlimitSin : Sin
{
    public GameObject m_objSlot;
    public Transform m_tInventory;
    public Slot m_cSelectSlot;
    public Text m_txtSelectEa;
    public Text m_txtNowLv;
    public Text m_txtAfterEa;
    List<Slot> m_vecMyCard = new List<Slot>();
    public List<Card> m_vecSelectCard = new List<Card>();
    int m_nMaxSelecyEa;
    bool Click = true;
    public override void Open()
    {
        base.Open();
        if (Click == true)
        {
            GameManager.GM.ShowText("S랭크 카드의 최대 레벨(Max Level)을\n1 증가 시켜줍니다.");
            Click = false;
        }
        else if (Click == false)
        {

        }
        for (int i = 0; i < GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard.Count; i++)
        {
            GameObject temp = Instantiate(m_objSlot, m_tInventory);
            Button tempButton = temp.GetComponent<Button>();
            Slot tempCard = temp.GetComponent<Slot>();
            m_vecMyCard.Add(tempCard);

            tempCard.Set(GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard[i]);
            tempButton.onClick.AddListener(() => SelectSlot(tempCard));
        }
        m_txtSelectEa.text = "선택 0";
        SortCard();
    }
    public void SelectSlot(Slot cSlot)
    {
        if (cSlot.m_cCard.m_nUnlimite >= 10)
        {
            GameManager.GM.ShowText("이미 초월단계가 최대입니다!!!");
            return;
        }
        if (m_cSelectSlot.m_cCard == null)
        {
            m_cSelectSlot.Set(cSlot.m_cCard);
            SortCard(cSlot.m_cCard);
            m_nMaxSelecyEa = 10 - cSlot.m_cCard.m_nUnlimite;
            m_txtNowLv.text = cSlot.m_cCard.m_nMaxLevel.ToString();
            m_txtAfterEa.text = cSlot.m_cCard.m_nMaxLevel.ToString();
            cSlot.gameObject.SetActive(false);
        }
        else
        {
            if (m_vecSelectCard.Count >= m_nMaxSelecyEa) return;

            m_vecSelectCard.Add(cSlot.m_cCard);
            m_txtSelectEa.text = "선택 " + m_vecSelectCard.Count;
            if (cSlot.m_cCard.m_nUnlimite > 0)
            {
                m_nMaxSelecyEa -= cSlot.m_cCard.m_nUnlimite ;
            }
            int nUpLv = 0;
            for (int i = 0; i < m_vecSelectCard.Count; i++)
            {
                nUpLv += m_vecSelectCard[i].m_nUnlimite + 1;
            }
            if (nUpLv > 10 - (m_cSelectSlot.m_cCard.m_nMaxLevel - 70)) nUpLv = 10 - (m_cSelectSlot.m_cCard.m_nMaxLevel - 70);
            m_txtAfterEa.text = "" + (m_cSelectSlot.m_cCard.m_nMaxLevel + nUpLv);
            cSlot.gameObject.SetActive(false);
            SortCard(cSlot.m_cCard);
        }
    }
    public void CancelBut()
    {
        m_cSelectSlot.Set(null);
        m_vecSelectCard.Clear();
        SortCard();
        Close();
        Open();
    }
    public void Submit()
    {
        GameManager.GM.cServer.UnlimitCard(this);
        
    }
    public void Cancel()
    {
        Close();
        Open();
    }
    void SortCard(Card cCard = null)
    {
        float height;
        int nCount = m_vecMyCard.Count - m_vecSelectCard.Count - 1;

        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            if (m_vecMyCard[i].m_cCard.m_eCardRank != CardRank.S)
            {
                m_vecMyCard[i].gameObject.SetActive(false);
                nCount--;
            }
        }

        if (cCard != null)
        {
            for (int i = 0; i < m_vecMyCard.Count; i++)
            {
                if (m_vecMyCard[i].m_cCard.m_eCardType == m_cSelectSlot.m_cCard.m_eCardType)
                {

                }
                else if (m_vecMyCard[i].m_cCard.m_eCardRank != CardRank.S)
                {

                }
                else
                {
                    nCount--;
                    m_vecMyCard[i].gameObject.SetActive(false);
                }
            }
        }
        if (nCount % 4 == 0)
            height = 410f * (nCount / 4f);
        else
            height = 410f * ((nCount - nCount % 4) / 4f) + 410f;
        m_tInventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, height);
    }
    public override void Close()
    {
        base.Close();
        m_nMaxSelecyEa = 10;
        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            Destroy(m_vecMyCard[i].gameObject);
        }
        m_vecMyCard.Clear();
        m_vecSelectCard.Clear();
        m_cSelectSlot.Set(null);
        m_txtSelectEa.text = "-";
        m_txtNowLv.text = "-";
        m_txtAfterEa.text = "-";
    }
}
