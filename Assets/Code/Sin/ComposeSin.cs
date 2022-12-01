using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComposeSin : Sin
{

    //보유중인 카드 오픈
    public Transform m_tInventory; //인벤토리
    public GameObject m_objSlot; 
    public List<Slot> m_vecMyCard = new List<Slot>(); //내가 가지고있는 카드
    //카드 올리기
    public List<Slot> m_vecMySelectCard = new List<Slot>(); //합성카드 칸
    //카드 내리기
    
    public override void Open() //창을 켰을때
    {
        base.Open();
        GameManager.GM.ShowText("같은 랭크인 카드를 무작위로 5개를 넣어\n한 단계 업그레이드 된 카드를 랜덤으로 얻습니다.");
        for (int i = 0; i < m_vecMySelectCard.Count; i++)
        {
            m_vecMySelectCard[i].Set(null);
        }
        //보유 카드 불러오는 기능
        for (int i = 0; i < GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard.Count; i++)
        {
            GameObject temp = Instantiate(m_objSlot, m_tInventory);
            Button tempButton = temp.GetComponent<Button>();
            Slot tempCard = temp.GetComponent<Slot>();
            m_vecMyCard.Add(tempCard);

            tempCard.Set(GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard[i]);
            tempButton.onClick.AddListener(() => SelectSlot(tempCard)); //카드를 클릭하면 위로 올린다.
        }
        
        int nCount = m_vecMyCard.Count;
        
        float height;
        
        for (int i=0; i < m_vecMyCard.Count; i++)//S랭크만 안 뜨게하는 기능
        {
            if (m_vecMyCard[i].m_cCard.m_eCardRank == CardRank.S)
            {
                nCount--;
                m_vecMyCard[i].gameObject.SetActive(false);
            }
        }
        print(nCount);
        //카드슬롯 화면 조정
        if (m_vecMyCard.Count % 4 == 0)
            height = 410f * (nCount / 4f);
        else
            height = 410f * ((nCount - nCount % 4) / 4f) + 410f;
        m_tInventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, height);
    }
    public override void Close() //창을 껏을때
    {
        base.Close();
        //오브젝트 클리어하는 기능
        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            if (m_vecMyCard[i] != null)
            {
                Destroy(m_vecMyCard[i].gameObject);
            }
        }
        m_vecMyCard.Clear();
    }
    public void SelectSlot(Slot cSlot) //카트 클릭시 올릴수 있게 하는 기능
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
            //중복체크
            
        }
        int nCount = m_vecMyCard.Count;
        float height;
        //같은 랭크만 뜨게하는 기능
        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            if (m_vecMyCard[i].m_cCard.m_eCardRank != cSlot.m_cCard.m_eCardRank)
            {   
                //내가 가지고 있는카드 안에 있는 i번째의 카드랭크가 내가 선택한 카드 랭크와 같지않으면
                nCount--;
                m_vecMyCard[i].gameObject.SetActive(false);
                //내가 가지고있는 카드의 오브젝트를 비활성화한다.
            }
        }
        if (m_vecMyCard.Count % 4 == 0)
            height = 410f * (nCount / 4f);
        else
            height = 410f * ((nCount - nCount % 4) / 4f) + 410f;
        m_tInventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, height);
    }
    public void BackSlot(Slot cSlot) //위에있는 카드를 클릭했을때 기능
    {
        if (cSlot.m_cCard == null)
        {
            return;
        }
        CardRank eCR = CardRank.S;
        if (cSlot.m_cCard != null) //클릭한 카드
        {
            for (int i = 0; i < m_vecMyCard.Count; i++) //내가 가지고 있는 카드가 i번째 돌때
            {
                if (m_vecMyCard[i].m_cCard == cSlot.m_cCard) //내가 가지고 있는 카드와 내가 클릭한 카드가 같을때
                {

                    m_vecMyCard[i].gameObject.SetActive(true); //오브젝트 활성화를 시켜준다
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
            //같은 랭크만 뜨게하는 기능
            for (int i = 0; i < m_vecMyCard.Count; i++)
            {
                if (m_vecMyCard[i].m_cCard != null)
                {
                    if (m_vecMyCard[i].m_cCard.m_eCardRank != eCR)
                    {
                        //내가 가지고 있는카드 안에 있는 i번째의 카드랭크가 내가 선택한 카드 랭크와 같지않으면
                        nCount--;
                        m_vecMyCard[i].gameObject.SetActive(false);
                        //내가 가지고있는 카드의 오브젝트를 비활성화한다.
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
            for (int i = 0; i < m_vecMyCard.Count; i++)//S랭크만 안 뜨게하는 기능
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
                GameManager.GM.ShowText("카드 5장을 모두 선택하세요!");
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


