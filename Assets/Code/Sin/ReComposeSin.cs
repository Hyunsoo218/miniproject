using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReComposeSin : Sin
{
    public GameObject m_objSlot;
    public GameObject m_objSelectCardPel;
    public Transform m_tInventory;
    public List<Slot> m_vecRandom = new List<Slot>();
    public List<Slot> m_vecConf = new List<Slot>();
    public List<Slot> m_vecCardMenu = new List<Slot>();
    public List<Text> m_vecCardData = new List<Text>();
    public Slot m_cShowCard;
    List<Slot> m_vecMyCard = new List<Slot>();
    bool m_bRandom;
    bool Click = true;
    private void Start()
    {
        for (int i = 0; i < m_vecCardMenu.Count; i++)
        {
            m_vecCardMenu[i].Set(GameManager.GM.cCM.GetCard((CardType)i,CardRank.S));
        }
    }
    public override void Open()
    {
        base.Open();
        if (Click == true)
        {
            GameManager.GM.ShowText("S랭크 카드를 소모하여 랜덤/확정으로 S랭크 카드를 얻습니다.\n2장을 소모 할 경우는 랜덤\n5장을 소모 할 경우엔\n원하는 카드를 얻을 수 있습니다.");
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
        SortCard();
        m_bRandom = false;
        ChageMode();
        m_cShowCard.Set(null);
        m_vecCardData[0].text = "-";
        m_vecCardData[1].text = "-";
        m_vecCardData[2].text = "-";
        m_vecCardData[3].text = "-";
        m_vecCardData[4].text = "-";
    }
    public void SelectSlot(Slot cSlot)
    {
        if (m_bRandom == false)
        {
            for (int i = 0; i < m_vecConf.Count; i++)
            {
                if (m_vecConf[i].m_cCard == null)
                {
                    m_vecConf[i].Set(cSlot.m_cCard);
                    cSlot.gameObject.SetActive(false);
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < m_vecRandom.Count; i++)
            {
                if (m_vecRandom[i].m_cCard == null)
                {
                    m_vecRandom[i].Set(cSlot.m_cCard);
                    cSlot.gameObject.SetActive(false);
                    break;
                }
            }
        }
        SortCard();
    }
    public void CancelBut()
    {
        SortCard();
        Close();
        Open();
    }
    public void Submit()
    {
        if (m_bRandom == false)
        {
            for (int i = 0; i < m_vecConf.Count; i++)
            {
                if (m_vecConf[i].m_cCard == null)
                {
                    GameManager.GM.ShowText("카드 5장을 모두 선택하세요!");
                    return;
                }
            }

            m_objSelectCardPel.SetActive(true);
            return;
        }
        else
        {
            for (int i = 0; i < m_vecRandom.Count; i++)
            {
                if (m_vecRandom[i].m_cCard == null)
                {
                    GameManager.GM.ShowText("카드 2장을 모두 선택하세요!");
                    return;
                }
            }
            GameManager.GM.cServer.ReComposeRedon(this);
        }
    }
    public void Cancel(Slot cSlot)
    {
        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            if (m_vecMyCard[i].m_cCard == cSlot.m_cCard)
            {
                cSlot.Set(null);
                m_vecMyCard[i].gameObject.SetActive(true);
                return;
            }
        }
    }
    public void ChageMode()
    {
        for (int i = 0; i < m_vecConf.Count; i++)
        {
            m_vecConf[i].Set(null);
        }
        for (int i = 0; i < m_vecRandom.Count; i++)
        {
            m_vecRandom[i].Set(null);
        }
        SortCard();

        if (m_bRandom == false)
        {
            m_vecRandom[0].gameObject.transform.parent.gameObject.SetActive(true);
            m_vecConf[0].gameObject.transform.parent.gameObject.SetActive(false);
            m_bRandom = true;
        }
        else
        {
            m_vecRandom[0].gameObject.transform.parent.gameObject.SetActive(false);
            m_vecConf[0].gameObject.transform.parent.gameObject.SetActive(true);
            m_bRandom = false;
        }
    }
    public void ClickCard(Slot cSlot)
    {
        m_cShowCard.Set(cSlot.m_cCard);
        m_vecCardData[0].text = cSlot.m_cCard.m_eCardRank + "";
        m_vecCardData[1].text = cSlot.m_cCard.m_nLevel + "";
        m_vecCardData[2].text = cSlot.m_cCard.m_fAp + "";
        m_vecCardData[3].text = cSlot.m_cCard.m_fHp + "";
        m_vecCardData[4].text = cSlot.m_cCard.m_strData + "";
    }
    public void SelctCard()
    {
        if (m_cShowCard.m_cCard == null)
        {
            GameManager.GM.ShowText("획득할 카드를 선택 하세요!");
            return;
        }
        GameManager.GM.cServer.ReComposeConf(this);
    }
    void SortCard(Card cCard = null)
    {
        int nSelectCardEa = 0;
        for (int i = 0; i < m_vecConf.Count; i++) 
            if (m_vecConf[i].m_cCard != null) 
                nSelectCardEa++;
        for (int i = 0; i < m_vecRandom.Count; i++)
            if (m_vecRandom[i].m_cCard != null)
                nSelectCardEa++;
        float height;
        int nCount = m_vecMyCard.Count - nSelectCardEa;

        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            if (m_vecMyCard[i].m_cCard.m_eCardRank != CardRank.S)
            {
                m_vecMyCard[i].gameObject.SetActive(false);
                nCount--;
            }
        }
        print(nCount);
        if (nCount % 4 == 0)
            height = 410f * (nCount / 4f);
        else
            height = 410f * ((nCount - nCount % 4) / 4f) + 410f;
        m_tInventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, height);
    }
    public override void Close()
    {
        base.Close();
        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            Destroy(m_vecMyCard[i].gameObject);
        }
        for (int i = 0; i < m_vecConf.Count; i++)
        {
            m_vecConf[i].Set(null);
        }
        for (int i = 0; i < m_vecRandom.Count; i++)
        {
            m_vecRandom[i].Set(null);
        }
        m_vecMyCard.Clear();
        m_objSelectCardPel.SetActive(false);
    }
}
