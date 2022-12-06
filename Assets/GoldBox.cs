using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBox : MonoBehaviour
{
    [SerializeField] Transform _tSponPos;
    [SerializeField] NeglectSin sin;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (sin._nBuf1Ea > 0)
            {
                sin._nBuf1Ea--;
                GameManager.GM.m_cPlayer.m_nGold += GameManager.GM.m_cPlayer.m_nBangchiGold;
                sin.SetData();
            }
            if (sin._nBuf2Ea > 0)
            {
                sin._nBuf2Ea--;
                if (sin._nBuf2Ea == 0)
                {
                    //  속도 원래대로
                    BangchiManager._fBoost = 1;
                }
                else
                {
                    BangchiManager._fBoost = 2;
                }
                sin.SetData();
            }
            GameManager.GM.m_cPlayer.m_nGold += GameManager.GM.m_cPlayer.m_nBangchiGold;
            GameManager.GM.m_cPlayer.SetGold();
            transform.position = _tSponPos.position;
        }
    }
}
