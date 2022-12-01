using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComposeSin : Sin
{

    //�������� ī�� ����
    public Transform m_tInventory; //�κ��丮
    public GameObject m_objSlot; 
    public List<Slot> m_vecMyCard = new List<Slot>(); //���� �������ִ� ī��
    //ī�� �ø���
    public List<Slot> m_vecMySelectCard = new List<Slot>(); //�ռ�ī�� ĭ
    //ī�� ������
    
    public override void Open() //â�� ������
    {
        base.Open();
        GameManager.GM.ShowText("���� ��ũ�� ī�带 �������� 5���� �־�\n�� �ܰ� ���׷��̵� �� ī�带 �������� ����ϴ�.");
        for (int i = 0; i < m_vecMySelectCard.Count; i++)
        {
            m_vecMySelectCard[i].Set(null);
        }
        //���� ī�� �ҷ����� ���
        for (int i = 0; i < GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard.Count; i++)
        {
            GameObject temp = Instantiate(m_objSlot, m_tInventory);
            Button tempButton = temp.GetComponent<Button>();
            Slot tempCard = temp.GetComponent<Slot>();
            m_vecMyCard.Add(tempCard);

            tempCard.Set(GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard[i]);
            tempButton.onClick.AddListener(() => SelectSlot(tempCard)); //ī�带 Ŭ���ϸ� ���� �ø���.
        }
        
        int nCount = m_vecMyCard.Count;
        
        float height;
        
        for (int i=0; i < m_vecMyCard.Count; i++)//S��ũ�� �� �߰��ϴ� ���
        {
            if (m_vecMyCard[i].m_cCard.m_eCardRank == CardRank.S)
            {
                nCount--;
                m_vecMyCard[i].gameObject.SetActive(false);
            }
        }
        print(nCount);
        //ī�彽�� ȭ�� ����
        if (m_vecMyCard.Count % 4 == 0)
            height = 410f * (nCount / 4f);
        else
            height = 410f * ((nCount - nCount % 4) / 4f) + 410f;
        m_tInventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, height);
    }
    public override void Close() //â�� ������
    {
        base.Close();
        //������Ʈ Ŭ�����ϴ� ���
        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            if (m_vecMyCard[i] != null)
            {
                Destroy(m_vecMyCard[i].gameObject);
            }
        }
        m_vecMyCard.Clear();
    }
    public void SelectSlot(Slot cSlot) //īƮ Ŭ���� �ø��� �ְ� �ϴ� ���
    {
        for (int i = 0; i < m_vecMySelectCard.Count; i++)
        {
            if (m_vecMySelectCard[i].m_cCard == null)
            {
                m_vecMySelectCard[i].Set(cSlot.m_cCard);
                cSlot.gameObject.SetActive(false);
                break;
            }
            else if (m_vecMySelectCard[i].m_cCard == cSlot.m_cCard)
            {
                break;
            }
            //�ߺ�üũ
            
        }
        int nCount = m_vecMyCard.Count;
        float height;
        //���� ��ũ�� �߰��ϴ� ���
        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            if (m_vecMyCard[i].m_cCard.m_eCardRank != cSlot.m_cCard.m_eCardRank)
            {   
                //���� ������ �ִ�ī�� �ȿ� �ִ� i��°�� ī�巩ũ�� ���� ������ ī�� ��ũ�� ����������
                nCount--;
                m_vecMyCard[i].gameObject.SetActive(false);
                //���� �������ִ� ī���� ������Ʈ�� ��Ȱ��ȭ�Ѵ�.
            }
        }
        if (m_vecMyCard.Count % 4 == 0)
            height = 410f * (nCount / 4f);
        else
            height = 410f * ((nCount - nCount % 4) / 4f) + 410f;
        m_tInventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, height);
    }
    public void BackSlot(Slot cSlot) //�����ִ� ī�带 Ŭ�������� ���
    {
        if (cSlot.m_cCard == null)
        {
            return;
        }
        CardRank eCR = CardRank.S;
        if (cSlot.m_cCard != null) //Ŭ���� ī��
        {
            for (int i = 0; i < m_vecMyCard.Count; i++) //���� ������ �ִ� ī�尡 i��° ����
            {
                if (m_vecMyCard[i].m_cCard == cSlot.m_cCard) //���� ������ �ִ� ī��� ���� Ŭ���� ī�尡 ������
                {

                    m_vecMyCard[i].gameObject.SetActive(true); //������Ʈ Ȱ��ȭ�� �����ش�
                    break;
                }
            }
            eCR = cSlot.m_cCard.m_eCardRank;
            cSlot.Set(null);
        }

        bool bCard = false;
        for (int i = 0; i < m_vecMySelectCard.Count; i++)
        {
            if (m_vecMySelectCard[i].m_cCard)
            {
                bCard = true;
                break;
            }
        }

        if (bCard)
        {
            int nCount = m_vecMyCard.Count;
            float height;
            //���� ��ũ�� �߰��ϴ� ���
            for (int i = 0; i < m_vecMyCard.Count; i++)
            {
                if (m_vecMyCard[i].m_cCard != null)
                {
                    if (m_vecMyCard[i].m_cCard.m_eCardRank != eCR)
                    {
                        //���� ������ �ִ�ī�� �ȿ� �ִ� i��°�� ī�巩ũ�� ���� ������ ī�� ��ũ�� ����������
                        nCount--;
                        m_vecMyCard[i].gameObject.SetActive(false);
                        //���� �������ִ� ī���� ������Ʈ�� ��Ȱ��ȭ�Ѵ�.
                    }
                }
            }
            
            if (m_vecMyCard.Count % 4 == 0)
                height = 410f * (nCount / 4f);
            else
                height = 410f * ((nCount - nCount % 4) / 4f) + 410f;
            m_tInventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, height);
        }
        else
        {
            int nCount = m_vecMyCard.Count;
            float height;
            for (int i = 0; i < m_vecMyCard.Count; i++)
            {
                m_vecMyCard[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < m_vecMyCard.Count; i++)//S��ũ�� �� �߰��ϴ� ���
            {
                if (m_vecMyCard[i].m_cCard.m_eCardRank == CardRank.S)
                {
                    nCount--;
                    m_vecMyCard[i].gameObject.SetActive(false);
                }
            }
            if (m_vecMyCard.Count % 4 == 0)
                height = 410f * (nCount / 4f);
            else
                height = 410f * ((nCount - nCount % 4) / 4f) + 410f;
            m_tInventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, height);
        }
       
    }
    public void Action()
    {
        for (int i = 0; i < m_vecMySelectCard.Count; i++)
        { 
            if (m_vecMySelectCard[i].m_cCard == null)
            {
                GameManager.GM.ShowText("ī�� 5���� ��� �����ϼ���!");
                return;
            }
        }

        CardRank eCR = m_vecMySelectCard[3].m_cCard.m_eCardRank;
        int nNextRank = (int)eCR;
        nNextRank--;
        Card temp = GameManager.GM.cCM.GetCard((CardType)Random.Range(0,24),(CardRank)nNextRank);
        GameManager.GM.GetCard(temp);

        for (int i = 0; i < m_vecMySelectCard.Count; i++)
        {
            GameManager.GM.m_cPlayer.m_cAvata.RemoveCard(m_vecMySelectCard[i].m_cCard);
        }
        Close();
        Open();

        return;
    }
}


