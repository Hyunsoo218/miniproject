using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : Sin
{
    Stage m_cStage;
    int m_nMonsterNumMax;
    int m_nMonsterNum;
    int m_nMonsterEa;
    float m_fSponTimeMax = .5f, m_fSponTime = 2;
    List<GameObject> m_vecMonster = new List<GameObject>();
    public GameObject m_objBoss;
    public Text m_txtTime;
    public Text m_txtScore;
    public Text m_txtScore_total;
    float m_fTime=1000f;
    bool m_bBossSpon;
    public Slider m_slBossHp;
    public Slider m_slWallHp;
    public Wall m_cWall;
    public List<Image> m_vecSlot = new List<Image>();
    public List<Image> m_vecMemberSlot = new List<Image>();
    public List<Image> m_vecMemberSlotUsed = new List<Image>();
    [SerializeField] Sin _cGguideSin;
    [SerializeField] Sin _cGguideBossSin;
    bool _bGguideBoss = false;
    [SerializeField] GameObject _objStopBut;
    [SerializeField] GameObject _objSkipBut;
    //게임 화면에서 싱행되는 것들을 관리함. Sin을 상속함
    public override void Open(Stage stage)
    {
        base.Open(stage);
        m_slWallHp.gameObject.SetActive(false);
        m_cWall.gameObject.SetActive(false);
        m_cStage = stage;
        m_nMonsterNumMax = stage.m_vecMonster.Count;
        m_nMonsterNum = 0;
        m_nMonsterEa = 0;
        m_bBossSpon = false;
        m_txtScore.text = "";
        switch (GameManager.GM.m_eGT)
        {
            case GameType.TutorialStage:
            case GameType.Defence:
                _objStopBut.SetActive(true);
                m_slBossHp.gameObject.SetActive(false);
                m_cWall.gameObject.SetActive(true);
                m_cWall.Respon();
                m_slWallHp.gameObject.SetActive(true);
                _objSkipBut.SetActive(false);
                m_fTime = 180f;
                break;
            case GameType.Boss:
                m_slBossHp.gameObject.SetActive(true);
                _objStopBut.SetActive(true);
                _objSkipBut.SetActive(false);
                m_fTime = 600f;
                break;
            case GameType.Laid:
                m_slBossHp.gameObject.SetActive(false);
                _objStopBut.SetActive(true);
                _objSkipBut.SetActive(false);
                m_fTime = 30f;
                break;
        }
        if (GameManager.GM.m_eGT == GameType.TutorialStage)
        {
            _objStopBut.SetActive(false);
            _objSkipBut.SetActive(true);
        }
        int nIndex = GameManager.GM.m_cPlayer.m_cAvata.m_nUseCardIndex;
        List<Card> vecUseCard = GameManager.GM.m_cPlayer.m_cAvata.m_vecUseCards[nIndex];
        for (int i = 0; i < vecUseCard.Count; i++)
        {
            if (vecUseCard[i] != null)
            {
                m_vecSlot[i].gameObject.SetActive(true);
                m_vecSlot[i].sprite = vecUseCard[i].m_imgImage;
            }
            else
            {
                m_vecSlot[i].gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < GameManager.GM.m_cPlayer.m_cAvata._vecUseMember.Count; i++)
        {
            m_vecMemberSlotUsed[i].gameObject.SetActive(false);
            if (GameManager.GM.m_cPlayer.m_cAvata._vecUseMember[i] != null)
            {
                m_vecMemberSlot[i].gameObject.SetActive(true);
                m_vecMemberSlot[i].sprite = GameManager.GM.m_cPlayer.m_cAvata._vecUseMember[i]._imgFace;
            }
            else
            {
                m_vecMemberSlot[i].gameObject.SetActive(false);
            }
        }

        GameManager.GM.m_cPlayer.m_cAvata.ReSet();
        print("게임 타입 : " + GameManager.GM.m_eGT);
        _cGguideSin.Close();
        if (GameManager.GM.m_eGT == GameType.TutorialStage)
        {
            GameManager.GM.m_eGS = GameState.Tutorial;
            _cGguideSin.Open();
        }
    }
    private void Update()
    {
        if (GameManager.GM.m_eGS != GameState.Game) return;
        if (GameManager.GM.m_eGS == GameState.Tutorial) return;
        m_fTime -= Time.deltaTime;

        int min = (int)(m_fTime / 60f);
        int sec = (int)m_fTime - (min * 60);
        m_txtTime.text = min + ":" + sec;

        if (m_fTime <= 0)
        {
            switch (GameManager.GM.m_eGT)
            {
                case GameType.TutorialStage:
                case GameType.Defence:
                    GameManager.GM.GoStageClear();
                    break;
                case GameType.Boss:
                    GameManager.GM.GoLose();
                    break;
                case GameType.Laid:
                    int sco = (int)m_objBoss.GetComponent<LaidBoss>().m_fScore;
                    GameManager.GM.cServer.UpdateRaidScore(sco);
                    m_txtScore_total.text = sco + " 점" ;
                    break;
            }
        } 
        switch (GameManager.GM.m_eGT)
        {
            case GameType.TutorialStage:
            case GameType.Defence:
                SponMonster();
                if (!m_bBossSpon && m_fTime <= 150f )
                {
                    m_bBossSpon = true;
                    m_slBossHp.gameObject.SetActive(true);
                    m_objBoss = Instantiate(m_cStage.m_objBoss);
                    m_objBoss.GetComponent<Boss>().m_slHp = m_slBossHp.GetComponent<Slider>();
                    m_objBoss.GetComponent<Boss>().Respon();
                }
                break;
            case GameType.Boss:
                if (!m_bBossSpon)
                {
                    m_bBossSpon = true;
                    m_objBoss = Instantiate(m_cStage.m_objBoss);
                    m_objBoss.GetComponent<Boss>().m_slHp = m_slBossHp.GetComponent<Slider>();
                    m_objBoss.GetComponent<Boss>().Respon();
                }
                break;
            case GameType.Laid:
                if (!m_bBossSpon)
                {
                    print("실행");
                    m_bBossSpon = true;
                    m_objBoss = Instantiate(m_cStage.m_objBoss);
                    m_objBoss.GetComponent<LaidBoss>().m_txtScore = m_txtScore;
                    m_objBoss.GetComponent<LaidBoss>().Respon();
                }
                break;
        }
        if (GameManager.GM.m_eGT == GameType.TutorialStage)
        {
            if (m_fTime <= 149.8f && !_bGguideBoss)
            {
                _bGguideBoss = true;
                GameManager.GM.m_eGS = GameState.Tutorial;
                _cGguideBossSin.Open();
            }
        }
    }
    void SponMonster()
    {
        if (m_nMonsterNumMax == m_nMonsterNum)
        {
            return;
        }

        m_fSponTime += Time.deltaTime;

        if (m_fSponTime >= m_fSponTimeMax)
        {
            m_fSponTime = 0;

            if (m_nMonsterEa < m_cStage.m_vecMonsterEa[m_nMonsterNum])
            {
                m_nMonsterEa++;
                GameObject temp = Instantiate(m_cStage.m_vecMonster[m_nMonsterNum]);
                temp.GetComponent<Unit>().Respon();

                m_vecMonster.Add(temp);
            }
            else if (m_nMonsterNum < m_nMonsterNumMax)
            {
                m_nMonsterNum++;
                m_nMonsterEa = 0;
            }
        }
    }
    public override void Close()
    {
        base.Close();
        for (int i = 0; i < m_vecMonster.Count; i++)
        {
            Destroy(m_vecMonster[i]);
        }
        if (m_objBoss)
        {
            print("시랭");
            Destroy(m_objBoss);
            m_objBoss = null;
        }
        m_vecMonster = new List<GameObject>();
        for (int i = 0; i < m_vecSlot.Count; i++)
        {
            m_vecSlot[i].sprite = null;
            
        }
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Skill");
        for (int i = 0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
        MonsterSponManager.Clear();
    }
    public void UseMember(int nNum)
    {
        m_vecMemberSlotUsed[nNum].gameObject.SetActive(true);
        GameManager.GM.m_cPlayer.m_cAvata._vecUseMember[nNum].Use();
    }
    public void ReStartGame()
    {
        GameManager.GM.m_eGS = GameState.Game;
    }
}
