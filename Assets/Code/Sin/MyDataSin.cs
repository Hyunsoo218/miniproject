using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyDataSin : Sin
{
    public List<Image> _vecImages = new List<Image>();
    public Sprite _imgNull;
    public Text _txtId;
    public Text _txtName;
    public Sin _cLogoutSin;
    public override void Open()
    {
        base.Open();
        _cLogoutSin.Close();
        _txtId.text = GameManager.GM.m_cPlayer.m_strId;
        _txtName.text = GameManager.GM.m_cPlayer.m_strName;
        List<Card> tempDeck = new List<Card>();

        int nIndex = GameManager.GM.m_cPlayer.m_cAvata.m_nUseCardIndex;
        for (int i = 0; i < 9; i++)
        {
            tempDeck.Add(null);
            int tempInt = PlayerPrefs.GetInt(GameManager.GM.m_cPlayer.userno + "preset" + nIndex + i);
            if (tempInt != -1)
            {
                for (int j = 0; j < GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard.Count; j++)
                {
                    if (GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard[j].cardno == tempInt)
                    {
                        tempDeck[i] = GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard[j];
                        break;
                    }
                }
            }
        }
        for (int i = 0; i < tempDeck.Count; i++)
        {
            if (tempDeck[i] == null)
            {
                _vecImages[i].sprite = _imgNull;
            }
            else
            {
                _vecImages[i].sprite = tempDeck[i].m_imgImage;
            }
        }
    }
    public void Logout() 
    {
        Close();
        PlayerPrefs.SetString("lastLoginId","");
        PlayerPrefs.SetString("lastLoginPw","");
        BangchiManager._fBoost = 1;
        PlayerPrefs.SetString("lastdate" + GameManager.GM.m_cPlayer.userno, DateTime.Now.ToString("yyyy년MM월dd일HH시mm분"));
        SceneManager.LoadScene("Game");
    }

}
