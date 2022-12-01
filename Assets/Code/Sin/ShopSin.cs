using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSin : Sin
{ 
    [SerializeField] List<Machine> _vecPanel;

    int _nPanelIndex = 0;
    public override void Open()
    {
        base.Open();
        GameManager.GM.ShowText("재화 / 다이아를 사용(소모)하여\n랜덤 뽑기(카드/동료)를 할 수 있습니다.");
    }
    public void GoNextPanel()
    {
        _nPanelIndex++;
        if (_nPanelIndex >= _vecPanel.Count)  _nPanelIndex = 0;
        for (int i = 0; i < _vecPanel.Count; i++) _vecPanel[i].Close();
        _vecPanel[_nPanelIndex].Open();
    }
    public void GoPrePanel()
    {
        _nPanelIndex--;
        if (_nPanelIndex < 0)  _nPanelIndex = _vecPanel.Count - 1;
        for (int i = 0; i < _vecPanel.Count; i++) _vecPanel[i].Close();
        _vecPanel[_nPanelIndex].Open();
    }
    public void Card_1() 
    {
        int nNum = Random.Range(0,99999) % 100;
        if (nNum < 3)
        {
            int nMemberNum;
            if (_vecPanel[_nPanelIndex].PicupList.Count == 0)
            {
                nMemberNum = Random.Range(0, 99999) % 6;
                GameManager.GM.GetMember((MemberType)nMemberNum);
            }
            else
            {
                nMemberNum = Random.Range(0, 2);
                if (nMemberNum == 1)
                {
                    GameManager.GM.GetMember(_vecPanel[_nPanelIndex].PicupList[0]);
                }
                else
                {
                    nMemberNum = Random.Range(0, 99999) % 6;
                    GameManager.GM.GetMember((MemberType)nMemberNum);
                }
            }
        }
        else
        {
            GameManager.GM.GetCard(GameManager.GM.cCM.GetRendom_1());
        }
    }
    public void Card_10() 
    {
        List<Card> temp = new List<Card>();

        for (int i = 0; i < 10; i++)
        {
            int nNum = Random.Range(0, 99999) % 100;
            if (nNum < 50)
            {
                int nMemberNum;
                Member mTemp = null;
                if (_vecPanel[_nPanelIndex].PicupList.Count == 0)
                {
                    nMemberNum = Random.Range(0, 99999) % 6;
                    mTemp = GameManager.GM.cMM.GetMember((MemberType)nMemberNum);
                }
                else
                {
                    nMemberNum = Random.Range(0, 2);
                    if (nMemberNum == 1)
                    {
                        mTemp = GameManager.GM.cMM.GetMember(_vecPanel[_nPanelIndex].PicupList[0]);
                    }
                    else
                    {
                        nMemberNum = Random.Range(0, 99999) % 6;
                        mTemp = GameManager.GM.cMM.GetMember((MemberType)nMemberNum);
                    }
                }
                temp.Add(mTemp);
            }
            else
            {
                temp.Add(GameManager.GM.cCM.GetRendom_1());
            }
        }
        GameManager.GM.GetCard(temp);
    }
    public void ShowProbability()
    {

    }
}

