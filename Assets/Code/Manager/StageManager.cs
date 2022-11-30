using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // 스테이지 정보를 가지는 클래스이다
    // m_vecStages는 Stage클래스를 가지는 프리팹을 저장라는 리스트 이다
    public List<Stage> m_vecStageDefan = new List<Stage>();
    public List<Stage> m_vecStageBoss = new List<Stage>();
    public List<Stage> m_vecStageLaidBoss = new List<Stage>();
    [SerializeField] Stage _cTutorialStage;
   
    public Stage GetStage(int nNum, GameType eGT)
    {
        switch (eGT)
        {
            case GameType.Defence: return m_vecStageDefan[nNum];

            case GameType.Laid: return m_vecStageLaidBoss[nNum];

            case GameType.Boss: return m_vecStageBoss[nNum];
        }
        return null;
    }
    public void OpanNextStage(Stage cStage)
    {
        int nIndex = 0;

        switch (cStage.m_eGameType)
        {
            case GameType.Defence:
                nIndex = m_vecStageDefan.IndexOf(cStage);
                nIndex++;
                m_vecStageDefan[nIndex].Open();
                break;
            case GameType.Boss:
                nIndex = m_vecStageBoss.IndexOf(cStage);
                nIndex++;
                m_vecStageBoss[nIndex].Open();
                break;
            case GameType.Laid:
                break;
        }
    }
    public Stage GetTutorialStage()
    {
        return _cTutorialStage;
    }
    public void SetStage(string m_strStage, bool m_bClear) 
    {
        //int index = 0;
        //for (int i = 0; i < m_vecStageDefan.Count; i++) 
        //{
        //    if (m_vecStageDefan[i].m_strStage == m_strStage)
        //    {
        //        index = i;
        //    }
        //}
        //for (int i = 0; i <= index; i++)
        //{
        //    m_vecStageDefan[i].Clear();
        //    OpanNextStage(m_vecStageDefan[i]);
        //}


        for (int i = 0; i < m_vecStageDefan.Count; i++)
        {
            if (m_strStage.Equals(m_vecStageDefan[i].m_strStage))
            {
                m_vecStageDefan[i].Open();
                m_vecStageDefan[i].m_bClear = m_bClear;
                OpanNextStage(m_vecStageDefan[i]);
                return;
            }
        }
        for (int i = 0; i < m_vecStageBoss.Count; i++)
        {
            if (m_strStage.Equals(m_vecStageDefan[i].m_strStage))
            {
                m_vecStageBoss[i].Open();
                m_vecStageBoss[i].m_bClear = m_bClear;
                OpanNextStage(m_vecStageDefan[i]);
                return;
            }
        }
    }
}
