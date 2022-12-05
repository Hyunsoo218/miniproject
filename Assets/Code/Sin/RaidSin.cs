using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaidSin : Sin
{
    public GameObject _objRankData;
    public RectTransform _tRanks;
    public List<GameObject> _vecRankData = new List<GameObject>();
    public Sin _cAllScoerSin;
    public Text _txtMyScore;
    public override void Open()
    {
        GameManager.GM.cServer.GetMyRaidScore(this);
    }
    public void OpenAllScoreSin()
    {
        GameManager.GM.cServer.GetAllRaidScore(this);
    }
}
