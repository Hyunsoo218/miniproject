using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSponManager : MonoBehaviour
{
    static List<Monster> _vecSponWaitMonster = new List<Monster>();

    float _fTime = 0;
    void Update()
    {
        if (GameManager.GM.m_eGT == GameType.None) return;
        if (_vecSponWaitMonster.Count == 0) return;
        _fTime += Time.deltaTime;

        if (_fTime >= 0.3f)
        {
            _fTime = 0;
            _vecSponWaitMonster[0].Respon();
            _vecSponWaitMonster.Remove(_vecSponWaitMonster[0]);
        }
    }
    static public void AddSponWaitMonster(Monster cMonster)
    {
        _vecSponWaitMonster.Add(cMonster);
    }
    static public void Clear() { _vecSponWaitMonster.Clear(); }
}
