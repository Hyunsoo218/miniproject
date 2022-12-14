using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    // 스테이지 정보를 가지는 클래스이다.
    // 몬스터의 프리팹을 저장하는 List이다.
    public GameType m_eGameType;
    public List<GameObject> m_vecMonster = new List<GameObject>();
    public List<int> m_vecMonsterEa = new List<int>();
    public GameObject m_objBoss; 
    public bool m_bClear = false;   // 1
    public bool m_bOpen = false;    
    public string m_strStage;       // 3
    public int m_nClearGold;
    public Sprite m_imgLook;

    public void Clear()
    {
        if (m_bClear == false)
        {
            m_bClear = true;

            StageData data = new StageData();
            data.m_bClear = true;
            data.m_strStage = m_strStage;
            GameManager.GM.cServer.StageClear(data);
            print("클리어함 서버 등록");
            // 플레이어 정보 저장
        }
        GameManager.GM.m_cPlayer.m_nBangchiGold += m_nClearGold;
        GameManager.GM.m_cPlayer.m_nGold += m_nClearGold;
        GameManager.GM.m_cPlayer.SetGold();
        GameManager.GM.cSM.OpanNextStage(this);
    }
    public void Open()
    {
        print(m_strStage + " Open() 실행");
        m_bOpen = true;
        GetComponent<Button>().image.sprite = null;
    }
}
