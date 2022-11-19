using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowBuffSin : Sin
{
    [SerializeField] Text _txtBuffName;
    [SerializeField] Text _txtBuffBoost;
    public override void Open()
    {
        base.Open();
        List<SetBuff> vecBuff = GameManager.GM.m_cPlayer.m_cAvata._vecMyBuff;
        string strName = "";
        string strBoost = "";

        for (int i = 0; i < vecBuff.Count; i++)
        {
            strName += vecBuff[i]._strName + "\n\n";
            strBoost += "+" + (vecBuff[i]._fBoost * 100f) + "%\n\n";
        }

        _txtBuffName.text = strName;
        _txtBuffBoost.text = strBoost;
    }
}
