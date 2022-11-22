using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBox : MonoBehaviour
{
    [SerializeField] Transform _tSponPos;
    public long _nGold;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.GM.m_cPlayer.m_nGold += _nGold;
            GameManager.GM.m_cPlayer.SetGold();
            transform.position = _tSponPos.position;
        }
    }
}
