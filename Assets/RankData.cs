using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankData : MonoBehaviour
{
    [SerializeField] Text _txtRank;
    [SerializeField] Text _txtName;
    [SerializeField] Text _txtScore;
    public void Set(RaidScore data)
    {
        print(data.Name + " Set ½ÇÇà");
        _txtRank.text = data.Ranking + "";
        _txtName.text = data.Name + "";
        _txtScore.text = data.Score + "";
    }
}
