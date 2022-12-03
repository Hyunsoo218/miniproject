using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionSin : Sin
{
    public GameObject m_objSlot;
    public Transform m_tInventory;
    public Slot m_cSelectSlot;
    public Slot m_cResultSlot;
    public Text m_txtSelectEa;
    List<Slot> m_vecMyCard = new List<Slot>();
    List<Slot> m_vecSelectCard = new List<Slot>();
    int m_cSelectCount = 0;
    Card m_cResult;
    public int Evolution;
    public override void Open()
    {
        base.Open();
        if (Evolution == 0)
        {
            Evolution = PlayerPrefs.GetInt("Evolution");
            if (Evolution == 0)
            {
            GameManager.GM.ShowText("같은 랭크인 동일한 카드를 5개 넣어\n카드랭크를 한 단계 올려줍니다.");
            }
            Evolution++;
            PlayerPrefs.SetInt("Evolution", Evolution);
        }
        //else
        //{
        //    Evolution = 0;
        //    PlayerPrefs.SetInt("Evolution", Evolution);
        //    print("초기화 했습니다.");
        //}
        //초기값으로 설정 (테스트 할때 else 부분 주석 처리 없애면 됨)

        for (int i = 0; i < GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard.Count; i++)
        {
            GameObject temp = Instantiate(m_objSlot, m_tInventory);
            Button tempButton = temp.GetComponent<Button>();
            Slot tempCard = temp.GetComponent<Slot>();
            m_vecMyCard.Add(tempCard);

            tempCard.Set(GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard[i]);
            tempButton.onClick.AddListener(() => SelectSlot(tempCard));
        }
        m_txtSelectEa.text = "선택 0 / 5";
        SortCard();
    }
    public void SelectSlot(Slot cSlot)
    {
        if (m_cSelectCount == 5) return;

        if (m_cSelectSlot.m_cCard == null)
        {
            m_cSelectSlot.Set(cSlot.m_cCard);
            CardRank temp = cSlot.m_cCard.m_eCardRank;
            int nNum = (int)temp;
            nNum--;
            print((CardRank)nNum);
            m_cResult = GameManager.GM.cCM.GetCard(cSlot.m_cCard.m_eCardType, (CardRank)nNum);
            m_cResult.Copy(cSlot.m_cCard);
            m_cResultSlot.Set(m_cResult);
            SortCard(cSlot.m_cCard);
            cSlot.gameObject.SetActive(false);
            m_cSelectCount = 1;
            m_txtSelectEa.text = "선택 " + m_cSelectCount + " / 5";
            m_vecSelectCard.Add(cSlot);
        }
        else
        {
            cSlot.gameObject.SetActive(false);
            m_cSelectCount++;
            m_txtSelectEa.text = "선택 " + m_cSelectCount + " / 5";
            m_vecSelectCard.Add(cSlot);
            for (int i = 0; i < m_vecSelectCard.Count; i++)
            {
                if (m_vecSelectCard[i].m_cCard.m_nLevel > m_cResult.m_nLevel)
                {
                    m_cResult.Copy(m_vecSelectCard[i].m_cCard);
                    m_cResultSlot.Set(m_cResult);
                }
            }
        }
    }
    public void CancelBut()
    {
        if (m_cResultSlot.m_cCard == null) return;
        m_cSelectSlot.Set(null);
        Destroy(m_cResultSlot.m_cCard.gameObject);
        m_cResultSlot.Set(null);
        m_vecSelectCard.Clear();
        SortCard();
        m_cSelectCount = 0;
        Close();
        Open();
    }
    public void Submit()
    {
        if (m_cSelectCount != 5)
        {
            GameManager.GM.ShowText("카드 5장을 모두 선택하세요!");
            return;
        }

        for (int i = 0; i < m_vecSelectCard.Count; i++)
        {
            GameManager.GM.m_cPlayer.m_cAvata.RemoveCard(m_vecSelectCard[i].m_cCard);
            Destroy(m_vecSelectCard[i].m_cCard.gameObject);
        }

        GameManager.GM.m_cPlayer.m_cAvata.GetCard(m_cResult);
        GameManager.GM.ShowCard("진화 성공", m_cResult);
        Close();
        Open();
    }
    void SortCard(Card cCard = null)
    {
        float height;
        int nCount = m_vecMyCard.Count - m_vecSelectCard.Count;

        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            if (m_vecMyCard[i].m_cCard.m_eCardRank == CardRank.S)
            {
                m_vecMyCard[i].gameObject.SetActive(false);
                nCount--;
            }
        }

        if (cCard != null)
        {
            for (int i = 0; i < m_vecMyCard.Count; i++)
            {
                if (m_vecMyCard[i].m_cCard.m_eCardRank == m_cSelectSlot.m_cCard.m_eCardRank)
                {
                    if (m_vecMyCard[i].m_cCard.m_eCardType == m_cSelectSlot.m_cCard.m_eCardType)
                    {
                        m_vecMyCard[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        nCount--;
                        m_vecMyCard[i].gameObject.SetActive(false);
                    }
                }
                else if (m_vecMyCard[i].m_cCard.m_eCardRank == CardRank.S) { }
                else
                {
                    nCount--;
                    m_vecMyCard[i].gameObject.SetActive(false);
                }
            }
            cCard.gameObject.SetActive(false);
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
        m_cSelectSlot.Set(null);
        m_cResultSlot.Set(null);
        m_vecSelectCard.Clear();
        SortCard();
        m_cSelectCount = 0;
        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            if (m_vecMyCard[i] != null)
            {
                Destroy(m_vecMyCard[i].gameObject);
            }
        }
        m_vecMyCard.Clear();
    }
}
