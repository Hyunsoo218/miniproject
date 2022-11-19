using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // �������� ������ ������ Ŭ�����̴�
    // m_vecStages�� StageŬ������ ������ �������� ������ ����Ʈ �̴�
    public List<Stage> m_vecStageDefan = new List<Stage>();
    public List<Stage> m_vecStageBoss = new List<Stage>();
    public List<Stage> m_vecStageLaidBoss = new List<Stage>();

    public Stage Stage
    {
        get => default;
        set
        {
        }
    }

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
}
