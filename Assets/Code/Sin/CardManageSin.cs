using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManageSin : Sin
{
    public List<Sin> m_vecPanel = new List<Sin>();
    public Transform m_tInventory;
    public List<Slot> m_vecMyCard = new List<Slot>();
    List<Slot> m_vecMyUseingCard = new List<Slot>();
    [Header("Prefab")]
    public GameObject m_objSlot;
    public Sprite m_imgUseing;
    [Header("Sort")]
    public List<Sin> m_vecSortMenu = new List<Sin>();
    public List<Text> m_vecOpenSortMenuTxt = new List<Text>();
    public List<Button> m_vecSortRankBut = new List<Button>();
    public List<Button> m_vecSortElementBut = new List<Button>();
    public List<Button> m_vecSortTypeBut = new List<Button>();
    [Header("Card_Data")]
    public Image m_imgCard;
    public Text m_txtLv;
    public Text m_txtRank;
    public Text m_txtHp;
    public Text m_txtAp;
    public Text m_txtData;
    [Header("Card_UpGrade")]
    public Text m_txtLevelUpGold;
    public Text m_txtLv_val;
    public Text m_txtHp_val;
    public Text m_txtAp_val;
    public int Manage;
    Slot m_cSelectSlot;

    List<bool> m_vecOpenSortMenu = new List<bool>();
    List<bool> m_vecSortRank = new List<bool>();
    List<bool> m_vecSortElement = new List<bool>();
    List<bool> m_vecSortType = new List<bool>();
    List<CardRank> m_vecCardRank = new List<CardRank>();
    List<CardElement> m_vecCardElement = new List<CardElement>();
    List<BullitType> m_vecBullitType = new List<BullitType>();
    private void Awake()
    {
        for (int i = 0; i < 3; i++) m_vecOpenSortMenu.Add(false);
        for (int i = 0; i < 5; i++) m_vecSortRank.Add(false);
        for (int i = 0; i < 6; i++) m_vecSortElement.Add(false);
        for (int i = 0; i < 4; i++) m_vecSortType.Add(false);
    }
    public void OnPanel(int nNum)
    {
        for (int i = 0; i < m_vecPanel.Count; i++)
        {
            m_vecPanel[i].Close();
        }
        m_vecPanel[nNum].Open();
    }
    public override void Open()
    {
        base.Open();
        if (Manage == 0)
        {
            Manage = PlayerPrefs.GetInt("Manage");
            print("불러왔습니다" + Manage);
            if (Manage == 0)
            {
            GameManager.GM.ShowText("카드 관리칸은 강화, 합성, 진화, 초월, 재구성 총 5가지로 이루어져있으며\n\n\n현재 칸은 원하는 카드를 재화를 소모해 강화를 하여, 능력치를 강화 할 수 있다.");
            }
            Manage++;
            PlayerPrefs.SetInt("Manage", Manage);
            print("저장했습니다." + Manage);
        }
        //else
        //{
        //    Manage = 0;
        //    PlayerPrefs.SetInt("Manage", Manage);
        //    print("초기화 했습니다.");
        //}


        List<Card> tempMyCard = new List<Card>();
        tempMyCard.AddRange(GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard);
        int index = GameManager.GM.m_cPlayer.m_cAvata.m_nUseCardIndex;
        List<Card> tempUseCard = GameManager.GM.m_cPlayer.m_cAvata.m_vecUseCards[index];
        for (int i = 0; i < 9; i++)
        {
            if (tempUseCard[i] != null)
            {
                tempMyCard.Remove(tempUseCard[i]);
            }
        }

        for (int i = 0; i < tempMyCard.Count; i++)
        {
            GameObject temp = Instantiate(m_objSlot, m_tInventory);
            Button tempButton = temp.GetComponent<Button>();
            Slot tempCard = temp.GetComponent<Slot>();
            m_vecMyCard.Add(tempCard);

            tempCard.Set(tempMyCard[i]);
            tempButton.onClick.AddListener(() => SelectSlot(tempCard));
        }
        

        bool bCardUse = false;

        int nIndex = GameManager.GM.m_cPlayer.m_cAvata.m_nUseCardIndex;
        List<Card> vecUseCard = GameManager.GM.m_cPlayer.m_cAvata.m_vecUseCards[nIndex];

        for (int i = 0; i < vecUseCard.Count; i++)
        {
            if (vecUseCard[i] != null)
            {
                bCardUse = true;
                GameObject temp = Instantiate(m_objSlot, m_tInventory);
                Button tempButton = temp.GetComponent<Button>();
                Slot tempCard = temp.GetComponent<Slot>();
                tempCard.Set(vecUseCard[i]);
                temp.transform.SetAsFirstSibling();
                m_vecMyCard.Add(tempCard);
                m_vecMyUseingCard.Add(tempCard);

                //////////////////////////////////
                GameObject temp2 = Instantiate(m_objSlot, temp.transform);
                Button tempButton2 = temp2.GetComponent<Button>();
                Slot tempCard2 = temp2.GetComponent<Slot>();
                //tempCard2.Set(GameManager.GM.m_cPlayer.m_cAvata.m_vecUseCard[i]);
                tempCard2.Set(null);

                tempButton2.onClick.AddListener(() => SelectSlot(tempCard));
                tempButton2.image.sprite = m_imgUseing;

                temp2.GetComponent<RectTransform>().sizeDelta = new Vector2(250f, 360f);
            }
        }

        if (bCardUse)
            SelectSlot(m_vecMyCard[m_vecMyCard.Count - 1]);
        else
        {
            GameObject temp = m_tInventory.GetChild(0).gameObject;
            SelectSlot(temp.GetComponent<Slot>());
        }
        OnPanel(0);
        SortCardEnd();
        CloseSortMenu();
        SortCardRank();
        SortCard();
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
        transform.parent.gameObject.GetComponent<CardSin>().SortCardRank();
        m_vecMyUseingCard.Clear();
    }
    public void SelectSlot(Slot cSlot)
    {
        m_cSelectSlot = cSlot;

        SetData();
    }
    public void OpenSortMenu(int nNum)
    {
        if (m_vecOpenSortMenu[nNum])
        {
            m_vecOpenSortMenu[nNum] = false;
            m_vecSortMenu[nNum].Close();
            m_vecOpenSortMenuTxt[nNum].text = "▲";
        }
        else
        {
            m_vecOpenSortMenu[nNum] = true;
            m_vecSortMenu[nNum].Open();
            m_vecOpenSortMenuTxt[nNum].text = "▼";
        }
    }
    void CloseSortMenu()
    {
        for (int i = 0; i < m_vecOpenSortMenu.Count; i++)
        {
            if (m_vecOpenSortMenu[i])
            {
                m_vecOpenSortMenu[i] = false;
                m_vecSortMenu[i].Close();
                m_vecOpenSortMenuTxt[i].text = "▲";
            }
        }
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
        if (m_vecSortRank[nNum])
        {
            m_vecSortRank[nNum] = false;
            ColorBlock col = m_vecSortRankBut[nNum].colors;
            col.normalColor = Color.white;
            col.selectedColor = Color.white;
            col.highlightedColor = Color.white;
            col.pressedColor = Color.white;
            m_vecSortRankBut[nNum].colors = col;

            m_vecCardRank.Add(temp);
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
            m_vecCardRank.Remove(temp);
        }

        int x = 0;

        for (int i = 0; i < m_vecSortRank.Count; i++)
        {
            if (!m_vecSortRank[i])
            {
                x++;
            }
        }

        if (x == m_vecSortRank.Count)
        {
            for (int i = 0; i < m_vecSortRank.Count; i++)
            {
                //SortRank(i);
            }
        }

        SortCard();
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

        if (m_vecSortElement[nNum])
        {
            m_vecSortElement[nNum] = false;
            ColorBlock col = m_vecSortElementBut[nNum].colors;
            col.normalColor = Color.white;
            col.selectedColor = Color.white;
            col.highlightedColor = Color.white;
            col.pressedColor = Color.white;
            m_vecSortElementBut[nNum].colors = col;

            m_vecCardElement.Add(temp);
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

            m_vecCardElement.Remove(temp);
        }

        int x = 0;

        for (int i = 0; i < m_vecSortElement.Count; i++)
        {
            if (!m_vecSortElement[i])
            {
                x++;
            }
        }

        if (x == m_vecSortElement.Count)
        {
            for (int i = 0; i < m_vecSortElement.Count; i++)
            {
                //SortElement(i);
            }
        }

        SortCard();
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
        if (m_vecSortType[nNum])
        {
            m_vecSortType[nNum] = false;
            ColorBlock col = m_vecSortTypeBut[nNum].colors;
            col.normalColor = Color.white;
            col.selectedColor = Color.white;
            col.highlightedColor = Color.white;
            col.pressedColor = Color.white;
            m_vecSortTypeBut[nNum].colors = col;

            m_vecBullitType.Add(temp);
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

            m_vecBullitType.Remove(temp);
        }

        int x = 0;

        for (int i = 0; i < m_vecSortType.Count; i++)
        {
            if (!m_vecSortType[i])
            {
                x++;
            }
        }

        if (x == m_vecSortType.Count)
        {
            for (int i = 0; i < m_vecSortType.Count; i++)
            {
                //SortType(i);
            }
        }

        SortCard();
    }
    void SortCardEnd()
    {
        for (int i = 0; i < m_vecSortRank.Count; i++)
        {
            if (!m_vecSortRank[i])
            {
                //SortRank(i);
            }
        }
        for (int i = 0; i < m_vecSortElement.Count; i++)
        {
            if (!m_vecSortElement[i])
            {
                //SortElement(i);
            }
        }
        for (int i = 0; i < m_vecSortType.Count; i++)
        {
            if (!m_vecSortType[i])
            {
                //SortType(i);
            }
        }
    }
    void SortCard()
    {
        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            m_vecMyCard[i].gameObject.SetActive(true);
        }

        int nCount = m_vecMyCard.Count;

        for (int i = 0; i < m_vecMyCard.Count; i++)
        {
            for (int x = 0; x < m_vecCardRank.Count; x++)
            {
                if (m_vecMyCard[i].m_cCard.m_eCardRank == m_vecCardRank[x])
                {
                    nCount--;
                    m_vecMyCard[i].gameObject.SetActive(false);
                }
            }
            for (int x = 0; x < m_vecBullitType.Count; x++)
            {
                if (m_vecMyCard[i].m_cCard.m_eBT == m_vecBullitType[x])
                {
                    nCount--;
                    m_vecMyCard[i].gameObject.SetActive(false);
                }
            }
            for (int x = 0; x < m_vecCardElement.Count; x++)
            {
                if (m_vecMyCard[i].m_cCard.m_objBullit.GetComponent<Bullit>().m_eCE == m_vecCardElement[x])
                {
                    nCount--;
                    m_vecMyCard[i].gameObject.SetActive(false);
                }
            }
        }

        float height;
        if (nCount % 4 == 0)
            height = 410f * (nCount / 4f);
        else
            height = 410f * ((nCount - nCount % 4) / 4f) + 410f;
        m_tInventory.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, height);
    }
    void SortCardRank()
    {
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
        for (int i = 0; i < m_vecMyUseingCard.Count; i++)
        {
            m_vecMyUseingCard[i].transform.SetAsFirstSibling();
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < m_vecMyUseingCard.Count; j++)
            {
                CardRank temp = m_vecMyUseingCard[j].m_cCard.m_eCardRank;
                int ntemp = (int)temp;
                if (ntemp == i)
                {
                    m_vecMyUseingCard[j].transform.SetAsFirstSibling();
                }
            }
        }
        for (int i = 2; i <= 80; i++)
        {
            for (int j = 0; j < m_vecMyUseingCard.Count; j++)
            {
                if (m_vecMyUseingCard[j].m_cCard.m_nLevel == i)
                {
                    m_vecMyUseingCard[j].transform.SetAsFirstSibling();
                }
            }
        }
    }
    void SetData()
    {
        Card temp = m_cSelectSlot.m_cCard;

        m_imgCard.sprite = temp.m_imgImage;
        m_txtLv.text = temp.m_nLevel.ToString();
        m_txtRank.text = temp.m_eCardRank.ToString();
        m_txtHp.text = ((int)temp.m_fHp).ToString();
        m_txtAp.text = ((int)temp.m_fAp).ToString();
        m_txtData.text = temp.m_strData;

        if (temp.m_nLevel == temp.m_nMaxLevel)
        {
            m_txtLv.text += "(최대)";
            m_txtLv_val.text = "-";
            m_txtHp_val.text = "-";
            m_txtAp_val.text = "-";
            m_txtLevelUpGold.text = "-";
        }
        else
        {
            m_txtLv_val.text = (temp.m_nLevel + 1).ToString();
            m_txtHp_val.text = ((int)(temp.m_fHp * 1.1f)).ToString();
            m_txtAp_val.text = ((int)(temp.m_fAp * 1.1f)).ToString();
            m_txtLevelUpGold.text = GameManager.GM.GoldToStr(temp.m_nLevelUpGold);
        }
        SortCardRank();
    }
    public void LevelUp()
    {
        if (GameManager.GM.m_cPlayer.UseGold(m_cSelectSlot.m_cCard.m_nLevelUpGold))
        {
            if (m_cSelectSlot.m_cCard.LvUp())
            {
                m_cSelectSlot.Set(m_cSelectSlot.m_cCard);

                int nIndex = GameManager.GM.m_cPlayer.m_cAvata.m_nUseCardIndex;
                List<Card> vecUseCard = GameManager.GM.m_cPlayer.m_cAvata.m_vecUseCards[nIndex];

                for (int i = 0; i < vecUseCard.Count; i++)
                {
                    if (vecUseCard[i] == m_cSelectSlot.m_cCard)
                    {
                        print("사용중인 카드 강화");
                        transform.parent.gameObject.GetComponent<CardSin>().UpdateData();
                    }
                }
                SetData();
            }
        }
        else
        {
            GameManager.GM.ShowText("골드가 부족합니다!!!");
        }
    }
}
