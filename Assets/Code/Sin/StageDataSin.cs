using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageDataSin : Sin
{
    Stage m_cStage;
    public Text m_txtStageName;
    public Transform m_tMonsterData;
    public GameObject m_objMonsterData;
    List<GameObject> m_vecMonsterData = new List<GameObject>();
    public MonsterData m_cBossData;

    public override void Open(Stage cStage)
    {
        if (!cStage.m_bOpen)
        {
            return;
        }
        base.Open(cStage);

        m_cStage = cStage;
        m_txtStageName.text = m_cStage.m_strStage;
        for (int i = 0; i < m_vecMonsterData.Count; i++)
        {
            Destroy(m_vecMonsterData[i]);
        }
        m_vecMonsterData.Clear();
        for (int i = 0; i < m_cStage.m_vecMonster.Count; i++)
        {
            GameObject temp = Instantiate(m_objMonsterData, m_tMonsterData);
            temp.GetComponent<MonsterData>().Set(m_cStage.m_vecMonster[i].GetComponent<Monster>());
            m_vecMonsterData.Add(temp);
        }
        m_tMonsterData.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(-777f, m_vecMonsterData.Count * 260f);
        m_cBossData.Set(m_cStage.m_objBoss.GetComponent<Monster>());
    }
    public void StartGame()
    {
        int nUseCardEa = 0;

        int nIndex = GameManager.GM.m_cPlayer.m_cAvata.m_nUseCardIndex;
        List<Card> vecUseCard = GameManager.GM.m_cPlayer.m_cAvata.m_vecUseCards[nIndex];
        print(nIndex + " 번째 프리셋 사용중");
        for (int i = 0; i < 9; i++)
        {
            if (vecUseCard[i] != null)
            {
                nUseCardEa++;
            }
        }
        if (nUseCardEa == 0)
        {
            GameManager.GM.ShowText("장착된 카드가 없습니다!!!");
            return;
        }
        if (GameManager.GM.m_cPlayer.UseGas(10))
        {
            switch (m_cStage.m_eGameType)
            {
                case GameType.Defence:
                    GameManager.GM.GoDefence(m_cStage);
                    break;
                case GameType.Boss:
                    GameManager.GM.GoBoss(m_cStage);
                    break;
                case GameType.Laid:
                    GameManager.GM.GoLaid(m_cStage);
                    break;
            }
            Close();
        }
        else
        {
            GameManager.GM.ShowText("피로도가 부족합니다!");
        }
    }
    public void AutoGame()
    {
        if (m_cStage.m_bClear)
        {
            if (GameManager.GM.m_cPlayer.UseGas(20))
            {
                GameManager.GM.m_cPlayer.m_nGold += m_cStage.m_nClearGold;
                GameManager.GM.m_cPlayer.SetGold();
            }
            else
            {
                GameManager.GM.ShowText("피로도가 부족합니다!");
            }
        }
        else
        {
            GameManager.GM.ShowText("클리어한 스테이지만 소탕이 가능합니다!!");
        }
    }
}
