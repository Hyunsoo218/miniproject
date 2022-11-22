using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public UiManager cUM;
    public StageManager cSM;
    public CardManager cCM;
    public MemberManager cMM;
    public Server cServer;
    public Player m_cPlayer;
    public GameState m_eGS;
    public GameType m_eGT;
    Stage m_cStage;
    Member _cMember;

    private void Awake() {  GM = this; }
    private void Start()
    {
        cUM = GetComponent<UiManager>();
        cSM = GetComponent<StageManager>();
        cCM = GetComponent<CardManager>();
        cMM = GetComponent<MemberManager>();
        m_cPlayer = GetComponent<Player>();
        cServer = GetComponent<Server>();

        GoLogin();

        GetMember(MemberType.Pheonix);
        GetMember(MemberType.Naiad);
        GetMember(MemberType.Ariel);
        GetMember(MemberType.Oread);
        GetMember(MemberType.Ignis);
        GetMember(MemberType.Archane);

    }
    public void GetCard(Card cCard)
    {
        m_cPlayer.m_cAvata.GetCard(cCard);
        ShowCard("카드 획득", cCard);
    }
    public void GetCard(List<Card> vecCard)
    {
        for (int i = 0; i < vecCard.Count; i++)
        {
            m_cPlayer.m_cAvata.GetCard(vecCard[i]);
        }
        Show10Card("10장 획득!", vecCard);
    }
    public void GetCard(CardType ct, CardRank cr) // 수정필요 매개변수로 종류랑 랭크 받아야함
    {
        Card temp = cCM.GetCard(ct, cr);
        m_cPlayer.m_cAvata.GetCard(temp);
        ShowCard("카드 획득", temp);
    }
    public void GetMember(MemberType mt) // 수정필요 매개변수로 맴버 종류 받아야함
    {
        m_cPlayer.m_cAvata.GetMember(cMM.GetMember(mt));
    }
    public void GetBuff(SetBuff cSetBuff)
    {
        m_cPlayer.m_cAvata.GetBuff(cSetBuff);
    }
    public void GoTitle()
    {
        m_eGS = GameState.Title;
        cUM.SetSin(m_eGS);
    }
    public void GoLogin()
    {
        m_eGS = GameState.Login;
        cUM.SetSin(m_eGS);
    }
    public void GoLobe()
    {
        m_eGS = GameState.Lobe;
        m_eGT = GameType.None;
        cUM.SetSin(m_eGS);
    }
    public void GoDefence(Stage cStage)
    {
        m_cStage = cStage;
        m_eGS = GameState.Game;
        m_eGT = GameType.Defence;
        cUM.SetSin(cStage);
    }
    public void GoBoss(Stage cStage)
    {
        m_cStage = cStage;
        m_eGS = GameState.Game;
        m_eGT = GameType.Boss;
        cUM.SetSin(cStage);
    }
    public void GoLaid(Stage cStage)
    {
        m_cStage = cStage;
        m_eGS = GameState.Game;
        m_eGT = GameType.Laid;
        cUM.SetSin(cStage);
    }
    public void GoStop()    { m_eGS = GameState.Stop;                               cUM.SetSin(m_eGS); }
    public void GoReStart() { m_eGS = GameState.ReStart;                            cUM.SetSin(m_eGS); }
    public void GoWin()     { m_eGS = GameState.Win;                                cUM.SetSin(m_eGS); }
    public void GoLose()    { m_eGS = GameState.Lose;                               cUM.SetSin(m_eGS); }
    public void GoStageClear()
    {
        m_cStage.Clear();
        GoWin();
    }
    public void GoLaidEnd() { m_eGS = GameState.LaidEnd;                            cUM.SetSin(m_eGS); }
    public void GoCutSin(Member cMember)
    {
        _cMember = cMember;
        m_eGS = GameState.CutSin;
        cUM.CloseUI();
        GameObject temp = Instantiate(cMember._objCutEffect, new Vector3(0,1,0), Quaternion.Euler(90,0,0));
        Transform tMagic = temp.transform.GetChild(4);
        tMagic.parent = null;
        tMagic.localScale = new Vector3(5f, 5f, 1f);
        Destroy(tMagic.gameObject,1.65f);
        Destroy(temp,1.65f);
        Invoke("OffCutSin", 1.65f);
    }
    void OffCutSin()
    {
        cUM.OpenUI();
        m_eGS = GameState.Game;
        GameObject temp = Instantiate(_cMember._objSkill , Vector3.zero, Quaternion.Euler(0,0,0));
        MemberSkill skill = temp.GetComponent<MemberSkill>();
        skill.Go(_cMember._fDamege, _cMember._eCE);
    }
    public string GoldToStr(long gold)
    {
        string strGold = gold.ToString();

        int end = strGold.Length;

        if (gold >= 1000000000000000)
        {
            end -= 15;
            strGold = strGold.Substring(0, end) + "e";
        }
        else if(gold >= 1000000000000)
        {
            end -= 12;
            strGold = strGold.Substring(0, end) + "d";
        }
        else if(gold >= 1000000000)
        {
            end -= 9;
            strGold = strGold.Substring(0, end) + "c";
        }
        else if (gold >= 1000000)
        {
            end -= 6;
            strGold = strGold.Substring(0, end) + "b";
        }
        else if (gold >= 1000)
        {
            end -= 3;
            strGold = strGold.Substring(0, end) + "a";
        }

        return strGold;
    }
    public void ShowText(string strText)
    {
        cUM.ShowText(strText);
    }
    public void ShowCard(string strText, Card cCard)
    {
        cUM.ShowCard(strText, cCard);
    }
    public void Show10Card(string strText, List<Card> cCard)
    {
        cUM.Show10Card(strText, cCard);
    }
    public void ShowMember(string strText, Member cMember)
    {
        cUM.ShowMember(strText, cMember);
    }
}
public enum GameState
{
    Title, Lobe, Game, Stop, ReStart, Win, Lose, LaidEnd, Login, CutSin
}
public enum GameType
{
    None, Defence, Boss, Laid
}

