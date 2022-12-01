using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class CardSin : Sin
{
    public List<Slot> m_vecMyDack;

    public GameObject m_objSlot;
    public Transform m_tInventory;
    public GameObject m_objAddDackBut;
    public List<Button> m_vecAddDackBut = new List<Button>();
    Slot m_cSelectcSlot;
    public List<Slot> m_vecMyCard = new List<Slot>();

    public Sprite m_imgPlus;
    public Sprite m_imgMius;
    public Sprite m_imgChang;

    public Text m_txtAp;
    public Text m_txtHp;
    public Text m_txtCost;

    public Sin m_cSortMenu;
    public Text m_txtOpenSortMenu;

    public List<Button> m_vecSortRankBut = new List<Button>();
    public List<Button> m_vecSortElementBut = new List<Button>();
    public List<Button> m_vecSortTypeBut = new List<Button>();

    public ShowBuffSin _cShowBuffSin;

    public Slot _cShowData;
    public Text _txtCardAp;
    public Text _txtCardHp;
    public Text _txtCardData;

    bool _bOpenSortMenu;
    List<bool> m_vecSortRank = new List<bool>();
    List<bool> m_vecSortElement = new List<bool>();
    List<bool> m_vecSortType = new List<bool>();
    List<CardRank> m_vecCardRank = new List<CardRank>();
    List<CardElement> m_vecCardElement = new List<CardElement>();
    List<BullitType> m_vecBullitType = new List<BullitType>();

    Thread _cThread;
    RectTransform _Inven;
    int nIndex;
    List<Card> _vecUseCard;

    public List<Button> m_vecFreeSetBut = new List<Button>();

    private void Awake()
    {
        _bOpenSortMenu = false;
        for (int i = 0; i < 5; i++) m_vecSortRank.Add(false); 
        for (int i = 0; i < 6; i++) m_vecSortElement.Add(false); 
        for (int i = 0; i < 4; i++) m_vecSortType.Add(false);
        _Inven = m_tInventory.gameObject.GetComponent<RectTransform>();
    }
    public override void Open()
    {
        GameManager.GM.ShowText("카드를 장착 / 해제 할 수 있으며\n세트효과를 확인할 수 있다.");
      
        base.Open();

        nIndex = GameManager.GM.m_cPlayer.m_cAvata.m_nUseCardIndex;

        List<Card> tempDeck = new List<Card>();

        for (int i = 0; i < 9; i++)
        {
            tempDeck.Add(null);

            int tempInt = PlayerPrefs.GetInt(GameManager.GM.m_cPlayer.userno + "preset" + nIndex + i);
            if (tempInt != -1)
            {
                for (int j = 0; j < GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard.Count; j++)
                {
                    if (GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard[j].cardno == tempInt)
                    {
                        tempDeck[i] = GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard[j];
                        break;
                    }
                }
            }
        }


        _vecUseCard = tempDeck;

        ColorBlock col = m_vecFreeSetBut[nIndex].colors;
        col.normalColor = new Color(0.3f, 0.7f, 0.85f);
        col.selectedColor = new Color(0.3f, 0.7f, 0.85f);
        col.highlightedColor = new Color(0.3f, 0.7f, 0.85f);
        col.pressedColor = new Color(0.3f, 0.7f, 0.85f);
        m_vecFreeSetBut[nIndex].colors = col;

        _cThread = null;
        CreatSlot();
        SortCardEnd();
        CloseSortMenu();
        UpdateData();
        SortCard();
        SortCardRank();
        _cShowBuffSin.Close();
        m_objAddDackBut.SetActive(false);
    }
    public void CreatSlot()
    {
        List<Slot> vecDelSlot = new List<Slot>();
        vecDelSlot.AddRange(m_vecMyCard);
        new Thread(() => 
        {
            MainThreadDispatcher.Instance.Enqueue(()=> 
            {
                for (int i = 0; i < vecDelSlot.Count; i++)
                {
                    if (vecDelSlot[i] != null)
                    {
                        Destroy(vecDelSlot[i].gameObject);
                    }
                }
            });
        }).Start();
        m_vecMyCard.Clear();

        List<Card> vecCreCard = new List<Card>();
        vecCreCard.AddRange(GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard);
        for (int i = 0; i < _vecUseCard.Count; i++)
        {
            if (_vecUseCard[i] != null)
            {
                vecCreCard.Add(_vecUseCard[i]);
            }
        }
        for (int i = 0; i < vecCreCard.Count; i++)
        {
            GameObject temp = Instantiate(m_objSlot, m_tInventory);
            Button tempButton = temp.GetComponent<Button>();
            Slot tempCard = temp.GetComponent<Slot>();
            m_vecMyCard.Add(tempCard);

            tempCard.Set(vecCreCard[i]);
            tempButton.onClick.AddListener(() => SelectSlot(tempCard));
        }
        for (int i = 0; i < _vecUseCard.Count; i++)
        {
            m_vecMyDack[i].Set(_vecUseCard[i]);
        }
    }
    public override void Close()
    {
        base.Close();
        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            if (m_vecMyCard[i] != null)
            {
                Destroy(m_vecMyCard[i].gameObject);
            }
        }
        m_vecMyCard.Clear();
        CloseSortMenu();
        GameManager.GM.cUM.ShowTextEnd();
    }
    public void SelectSlot( Slot cSlot )
    {
        if (cSlot.m_cCard == null) return;

        m_objAddDackBut.SetActive(true);
        m_cSelectcSlot = cSlot;
        CloseSortMenu();
        _cShowData.Set(cSlot.m_cCard);
        _txtCardAp.text = "" + cSlot.m_cCard.m_fAp;
        _txtCardHp.text = "" + cSlot.m_cCard.m_fHp;
        _txtCardData.text = cSlot.m_cCard.m_strData;

        for (int i = 0; i < m_vecMyDack.Count; i++)
        {
            if (m_vecMyDack[i] == m_cSelectcSlot)
            {
                for (int j = 0; j < m_vecMyDack.Count; j++)
                {
                    m_vecAddDackBut[j].image.sprite = m_imgChang;
                }
                m_vecAddDackBut[i].image.sprite = m_imgMius;
                return;
            }
            else if (m_vecMyDack[i].m_cCard != null)
            {
                m_vecAddDackBut[i].image.sprite = m_imgChang;
            }
            else
            {
                m_vecAddDackBut[i].image.sprite = m_imgPlus;
            }
        }
    }
    public void SelectSlot( int nNum )
    {
        m_objAddDackBut.SetActive(false);

        if (m_vecMyDack[nNum] == m_cSelectcSlot)
        {
            print("장착 카드 해제");
            for (int i = 0; i < m_vecMyCard.Count; i++)
            {
                if (m_vecMyCard[i].m_cCard == m_vecMyDack[nNum].m_cCard)
                {
                    m_vecMyCard[i].gameObject.SetActive(true);
                    GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard.Add(m_vecMyCard[i].m_cCard);
                    break;
                }
            }

            m_vecMyDack[nNum].Set(null);
            UpdateData();
            SortCardRank();
            SortCard();
            return;
        }
        for (int i = 0; i < m_vecMyDack.Count; i++)
        {
            if (m_vecMyDack[i] == m_cSelectcSlot)
            {
                for (int j = 0; j < m_vecMyDack.Count; j++)
                {
                    if (m_vecMyDack[j] == m_vecMyDack[nNum]) 
                    {
                        

                        print("덱 카드 바꾸기");
                        Card temp = m_vecMyDack[nNum].m_cCard;

                        m_vecMyDack[nNum].Set(m_cSelectcSlot.m_cCard);

                        m_cSelectcSlot.Set(temp);

                        int index = m_vecMyDack.IndexOf(m_cSelectcSlot);
                        _vecUseCard[index] = null;

                        UpdateData();
                        return;
                    }
                }
            }
        }

        int cost = 0;

        for (int i = 0; i < m_vecMyDack.Count; i++)
        {
            if (m_vecMyDack[i].m_cCard != null)
            {
                cost += m_vecMyDack[i].m_cCard.m_nCost;
            }
        }

        if (cost + m_cSelectcSlot.m_cCard.m_nCost > GameManager.GM.m_cPlayer.m_cAvata.m_nCost)
        {
            return;
        }

        ChangSlot(nNum);
    }
    void ChangSlot( int nNum )
    {
        if (m_vecMyDack[nNum].m_cCard == null)
        {
            print("�󽽷� ����");

            Card temp = m_vecMyDack[nNum].m_cCard;

            m_vecMyDack[nNum].Set(m_cSelectcSlot.m_cCard);
            m_cSelectcSlot.gameObject.SetActive(false);
        }
        else
        {
            print("�̹� �ִ� ���� ����");
            Card temp = m_vecMyDack[nNum].m_cCard;

            m_vecMyDack[nNum].Set(m_cSelectcSlot.m_cCard);

            m_cSelectcSlot.gameObject.SetActive(false);

            for (int i = 0; i < m_vecMyCard.Count; i++)
            {
                if (m_vecMyCard[i].m_cCard == temp)
                {
                    m_vecMyCard[i].gameObject.SetActive(true);
                    break;
                }
            }
            SortCardRank();
        }
        UpdateData();
        SortCard();
    }
    public void UpdateData()
    {
        int cost = 0;
        float ap = 0;
        float hp = 0;

        string key;
        for (int i = 0; i < m_vecMyDack.Count; i++)
        {
            key = GameManager.GM.m_cPlayer.userno +  "preset" + nIndex + i;

            if (m_vecMyDack[i].m_cCard != null)
            {
                cost += m_vecMyDack[i].m_cCard.m_nCost;
                ap += m_vecMyDack[i].m_cCard.m_fAp;
                hp += m_vecMyDack[i].m_cCard.m_fHp;

                PlayerPrefs.SetInt(key, m_vecMyDack[i].m_cCard.cardno);
            }
            else
            {
                PlayerPrefs.SetInt(key, -1);
            }
        }

        m_txtAp.text = "" + (int)ap;
        m_txtHp.text = "" + (int)hp;
        m_txtCost.text = "" + cost + "/" + GameManager.GM.m_cPlayer.m_cAvata.m_nCost;

        GameManager.GM.m_cPlayer.m_cAvata.m_fAp = ap;
        GameManager.GM.m_cPlayer.m_cAvata.m_fMaxHp = hp;

        for (int i = 0; i < m_vecMyDack.Count; i++)
        {
            m_vecMyDack[i].Set(m_vecMyDack[i].m_cCard);
            _vecUseCard[i] = m_vecMyDack[i].m_cCard;
            //if (m_vecMyDack[i].m_cCard != null)
            //{
            //    GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard.Remove(m_vecMyDack[i].m_cCard);

            //    GameManager.GM.m_cPlayer.m_cAvata.m_vecUseCard[i] = m_vecMyDack[i].m_cCard;

            //    m_vecMyDack[i].Set(m_vecMyDack[i].m_cCard); 
            //}
        }

        float height;
        if (m_vecMyCard.Count % 4 == 0)
            height = 410f * (m_vecMyCard.Count / 4f);
        else
            height = 410f * ((m_vecMyCard.Count - m_vecMyCard.Count % 4) / 4f) + 410f;
        _Inven.sizeDelta = new Vector2(1200f, height);
        SortCardRank();

        SetBuffManager.Set(m_vecMyDack);
        m_txtAp.text = "" + (int)GameManager.GM.m_cPlayer.m_cAvata.m_fAp;
    }
    public void OpenSortMenu()
    {
        if (_bOpenSortMenu == false)
        {
            _bOpenSortMenu = true;
            m_cSortMenu.Open();
            m_txtOpenSortMenu.text = "��";
        }
        else
        {
            _bOpenSortMenu = false;
            m_cSortMenu.Close();
            m_txtOpenSortMenu.text = "��";
        }
    }
    void CloseSortMenu()
    {
        _bOpenSortMenu = false;
        m_cSortMenu.Close();
        m_txtOpenSortMenu.text = "��";
    }
    public void SortRank(int nNum)
    {
        CardRank temp = CardRank.D;
        switch (nNum)
        {
            case 0: temp = CardRank.S; break;
            case 1: temp = CardRank.A; break;
            case 2: temp = CardRank.B; break;
            case 3: temp = CardRank.C; break;
            case 4: temp = CardRank.D; break;
            default: return;
        }
        if (m_vecSortRank[nNum] == true) 
        {
            m_vecSortRank[nNum] = false;
            ColorBlock col = m_vecSortRankBut[nNum].colors;
            col.normalColor = Color.white;
            col.selectedColor = Color.white;
            col.highlightedColor = Color.white;
            col.pressedColor = Color.white;
            m_vecSortRankBut[nNum].colors = col;

            m_vecCardRank.Remove(temp);
        }
        else
        {
            m_vecSortRank[nNum] = true;
            ColorBlock col = m_vecSortRankBut[nNum].colors;
            col.normalColor = new Color(0.3f, 0.7f, 0.85f);
            col.selectedColor = new Color(0.3f, 0.7f, 0.85f);
            col.highlightedColor = new Color(0.3f, 0.7f, 0.85f);
            col.pressedColor = new Color(0.3f, 0.7f, 0.85f);
            m_vecSortRankBut[nNum].colors = col;

            m_vecCardRank.Add(temp);
        }
        if (m_vecCardRank.Count == 5)
        {
            for (int i = 0; i < 5; i++)
            {
                m_vecSortRank[i] = false;
                ColorBlock col = m_vecSortRankBut[i].colors;
                col.normalColor = Color.white;
                col.selectedColor = Color.white;
                col.highlightedColor = Color.white;
                col.pressedColor = Color.white;
                m_vecSortRankBut[i].colors = col;
            }
            m_vecCardRank.Clear();
        }
    }
    public void SortElement(int nNum)
    {
        CardElement temp = CardElement.Fire;
        switch (nNum)
        {
            case 0: temp = CardElement.Fire; break;
            case 1: temp = CardElement.Water; break;
            case 2: temp = CardElement.Wind; break;
            case 3: temp = CardElement.Stone; break;
            case 4: temp = CardElement.Dark; break;
            case 5: temp = CardElement.Light; break;
            default: return;
        }
        if (m_vecSortElement[nNum] == true)
        {
            m_vecSortElement[nNum] = false;
            ColorBlock col = m_vecSortElementBut[nNum].colors;
            col.normalColor = Color.white;
            col.selectedColor = Color.white;
            col.highlightedColor = Color.white;
            col.pressedColor = Color.white;
            m_vecSortElementBut[nNum].colors = col;

            m_vecCardElement.Remove(temp);
        }
        else
        {
            m_vecSortElement[nNum] = true;
            ColorBlock col = m_vecSortElementBut[nNum].colors;
            col.normalColor = new Color(0.3f, 0.7f, 0.85f);
            col.selectedColor = new Color(0.3f, 0.7f, 0.85f);
            col.highlightedColor = new Color(0.3f, 0.7f, 0.85f);
            col.pressedColor = new Color(0.3f, 0.7f, 0.85f);
            m_vecSortElementBut[nNum].colors = col;

            m_vecCardElement.Add(temp);
        }
        if (m_vecCardElement.Count == 6)
        {
            for (int i = 0; i < 6; i++)
            {
                m_vecSortElement[i] = false;
                ColorBlock col = m_vecSortElementBut[i].colors;
                col.normalColor = Color.white;
                col.selectedColor = Color.white;
                col.highlightedColor = Color.white;
                col.pressedColor = Color.white;
                m_vecSortElementBut[i].colors = col;
            }
            m_vecCardElement.Clear();
        }
    }
    public void SortType(int nNum)
    {
        BullitType temp = BullitType.Nomal;
        switch (nNum)
        {
            case 0: temp = BullitType.Nomal; break;
            case 1: temp = BullitType.Drill; break;
            case 2: temp = BullitType.Shower; break;
            case 3: temp = BullitType.Storm; break;
            default: return;
        }
        if (m_vecSortType[nNum] == true)
        {
            m_vecSortType[nNum] = false;
            ColorBlock col = m_vecSortTypeBut[nNum].colors;
            col.normalColor = Color.white;
            col.selectedColor = Color.white;
            col.highlightedColor = Color.white;
            col.pressedColor = Color.white;
            m_vecSortTypeBut[nNum].colors = col;

            m_vecBullitType.Remove(temp);
        }
        else
        {
            m_vecSortType[nNum] = true;
            ColorBlock col = m_vecSortTypeBut[nNum].colors;
            col.normalColor = new Color(0.3f, 0.7f, 0.85f);
            col.selectedColor = new Color(0.3f, 0.7f, 0.85f);
            col.highlightedColor = new Color(0.3f, 0.7f, 0.85f);
            col.pressedColor = new Color(0.3f, 0.7f, 0.85f);
            m_vecSortTypeBut[nNum].colors = col;

            m_vecBullitType.Add(temp);
        }
        if (m_vecBullitType.Count == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                m_vecSortType[i] = false;
                ColorBlock col = m_vecSortTypeBut[i].colors;
                col.normalColor = Color.white;
                col.selectedColor = Color.white;
                col.highlightedColor = Color.white;
                col.pressedColor = Color.white;
                m_vecSortTypeBut[i].colors = col;
            }
            m_vecBullitType.Clear();
        }
    }
    void SortCardEnd()
    {
        for (int i = 0; i < m_vecSortRank.Count; i++)
        {
            if (m_vecSortRank[i])
            {
                SortRank(i);
            }
        }
        for (int i = 0; i < m_vecSortElement.Count; i++)
        {
            if (m_vecSortElement[i])
            {
                SortElement(i);
            }
        }
        for (int i = 0; i < m_vecSortType.Count; i++)
        {
            if (m_vecSortType[i])
            {
                SortType(i);
            }
        }
    }
    public void SortCard()
    {
        CloseSortMenu();
        if (_cThread != null)
        {
            _cThread = null;
            MainThreadDispatcher.Instance.Clear();
        }
        if (m_vecCardRank.Count == 0 && m_vecCardElement.Count == 0 && m_vecBullitType.Count == 0)
        {
            _cThread = new Thread(ThreadAllCardSort);
            _cThread.Start();
        }
        else
        {
            _cThread = new Thread(ThreadCardSort);
            _cThread.Start();
        }
    }
    void ThreadCardSort()
    {
        print("ThreadCardSort ������ ����");
        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            int index = i;
            MainThreadDispatcher.Instance.Enqueue(() =>
            {
                m_vecMyCard[index].gameObject.SetActive(false);
            });
        }
        float height;

        List<Slot> vecShowSlot = sortRank(m_vecMyCard);
        vecShowSlot = sortType(vecShowSlot);
        vecShowSlot = sortElem(vecShowSlot);
        vecShowSlot = sortUseing(vecShowSlot);

        for (int i = 0; i < vecShowSlot.Count; i++)
        {
            int index = i;
            MainThreadDispatcher.Instance.Enqueue(() =>
            {
                vecShowSlot[index].gameObject.SetActive(true);
            });
        }

        if (vecShowSlot.Count % 4 == 0)
            height = 410f * (vecShowSlot.Count / 4f);
        else
            height = 410f * ((vecShowSlot.Count - vecShowSlot.Count % 4) / 4f) + 410f;
        MainThreadDispatcher.Instance.Enqueue(() => { _Inven.sizeDelta = new Vector2(1200f, height); });

        _cThread = null;
    }
    void ThreadAllCardSort()
    {
        float height;
        List<Slot> vecShowSlot = sortUseing(m_vecMyCard);
        MainThreadDispatcher.Instance.Enqueue(() =>
        {
            for (int i = 0; i < m_vecMyCard.Count; i++)
            {
                m_vecMyCard[i].gameObject.SetActive(false);
            }
        });
        MainThreadDispatcher.Instance.Enqueue(() =>
        {
            for (int i = 0; i < vecShowSlot.Count; i++)
            {
                vecShowSlot[i].gameObject.SetActive(true);
            }
        });
        if (vecShowSlot.Count % 4 == 0)
            height = 410f * (vecShowSlot.Count / 4f);
        else
            height = 410f * (((vecShowSlot.Count) - ((vecShowSlot.Count) % 4)) / 4f) + 410f;
        MainThreadDispatcher.Instance.Enqueue(() => { _Inven.sizeDelta = new Vector2(1200f, height); });
        _cThread = null;
    }
    List<Slot> sortRank(List<Slot> temp)
    {
        if (m_vecCardRank.Count == 0) return temp;
        List<Slot> retList = new List<Slot>();
        for (int i = 0; i < temp.Count; i++)
        {
            for (int j = 0; j < m_vecCardRank.Count; j++)
            {
                if (temp[i].m_cCard.m_eCardRank == m_vecCardRank[j])
                {
                    retList.Add(temp[i]);
                    break;
                }
            }
        }
        return retList;
    }
    List<Slot> sortType(List<Slot> temp)
    {
        if (m_vecBullitType.Count == 0) return temp;
        List<Slot> retList = new List<Slot>();
        for (int i = 0; i < temp.Count; i++)
        {
            for (int j = 0; j < m_vecBullitType.Count; j++)
            {
                if (temp[i].m_cCard.m_eBT == m_vecBullitType[j])
                {
                    retList.Add(temp[i]);
                    break;
                }
            }
        }
        return retList;
    }
    List<Slot> sortElem(List<Slot> temp)
    {
        if (m_vecCardElement.Count == 0) return temp;
        List<Slot> retList = new List<Slot>();
        for (int i = 0; i < temp.Count; i++)
        {
            for (int j = 0; j < m_vecCardElement.Count; j++)
            {
                if (temp[i].m_cCard.m_eCardElement == m_vecCardElement[j])
                {
                    retList.Add(temp[i]);
                    break;
                }
            }
        } 
        return retList;
    }
    List<Slot> sortUseing(List<Slot> temp)
    {
        List<Slot> retList = new List<Slot>();
        for (int i = 0; i < temp.Count; i++)
        {
            bool eq = false;
            Card tempCard = temp[i].m_cCard;
            for (int j = 0; j < 9; j++)
            {
                if (_vecUseCard[j] != null)
                {
                    if (tempCard == _vecUseCard[j])
                    {
                        eq = true;
                        break;
                    }
                }
            }
            if (eq == false)
            {
                retList.Add(temp[i]);
            }
        }
        return retList;
    }
    public void SortCardRank()
    {
        for (int i = 0; i < m_vecMyDack.Count; i++)
        {
            if (m_vecMyDack[i].m_cCard != null)
            {
                m_vecMyDack[i].Set(m_vecMyDack[i].m_cCard);
            }
        }
        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            m_vecMyCard[i].Set(m_vecMyCard[i].m_cCard);
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < m_vecMyCard.Count; j++)
            {
                CardRank temp = m_vecMyCard[j].m_cCard.m_eCardRank;
                int ntemp = (int)temp;
                if (ntemp == i)
                {
                    m_vecMyCard[j].transform.SetAsLastSibling();
                }
            }
        }
        for (int i = 2; i <= 80; i++)
        {
            for (int j = 0; j < m_vecMyCard.Count; j++)
            {
                if (m_vecMyCard[j].m_cCard.m_nLevel == i)
                {
                    m_vecMyCard[j].transform.SetAsFirstSibling();
                }
            }
        }
    }
    public void CloseSelctBut()
    {
        m_objAddDackBut.SetActive(false);
    }
    public void ChangFreeSet(int nNum)
    {
        ColorBlock col = m_vecFreeSetBut[nIndex].colors;
        col.normalColor = Color.white;
        col.selectedColor = Color.white;
        col.highlightedColor = Color.white;
        col.pressedColor = Color.white;
        m_vecFreeSetBut[nIndex].colors = col;
        GameManager.GM.m_cPlayer.m_cAvata.m_nUseCardIndex = nNum;
        Close();
        Open();
    }
}
