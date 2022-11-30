using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    // �������� ������ ������ Ŭ�����̴�.
    // ������ �������� �����ϴ� List�̴�.
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
        m_bClear = true;
        GameManager.GM.m_cPlayer.m_nGold += m_nClearGold;
        GameManager.GM.m_cPlayer.SetGold();
        GameManager.GM.cSM.OpanNextStage(this);
    }
    public void Open()
    {
        print(m_strStage + " Open() ����");
        m_bOpen = true;
        GetComponent<Button>().image.sprite = null;
    }
}
