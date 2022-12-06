using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show10CardSin : Sin
{
    [SerializeField] List<Slot> _vecSlots = new List<Slot>();
    [SerializeField] Text _txtTitle; 
    [SerializeField] List<Sprite> _vecBoxing;
    List<Card> _vecCard;
    List<bool> _vecOpen = new List<bool>();
    public override void Open(string strData, List<Card> cCard)
    {
        base.Open(strData, cCard);
        while (_vecOpen.Count < 10) _vecOpen.Add(false);
        for (int i = 0; i < _vecOpen.Count; i++)  _vecOpen[i] = false; 
        _txtTitle.text = strData;
        _vecCard = cCard;
        for (int i = 0; i < _vecSlots.Count; i++)
        {
            _vecSlots[i].Set(cCard[i]);
            _vecSlots[i].Boxing(_vecBoxing[(int)cCard[i].m_eCardRank]);
            _vecSlots[i].transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
    public void OKBut()
    {
        bool bAllOpen = false;

        for (int i = 0; i < _vecOpen.Count; i++)
        {
            if (_vecOpen[i] == false)
            {
                if (_vecSlots[i].m_cCard.m_nCost == -1)
                {
                    bAllOpen = true;
                }
                else
                {
                    ShowBig(i);
                    bAllOpen = true;
                }
            }
        }
        if (bAllOpen) return;

        Close();
    }
    public void ShowBig(int nNum) 
    {
        if (_vecOpen[nNum] == false)
        {
            _vecOpen[nNum] = true;
            StartCoroutine(ShowCard(nNum));
        }
        else
        {
            GameManager.GM.ShowCard("", _vecSlots[nNum].m_cCard);
        }
    }
    IEnumerator ShowCard(int nNum)
    {
        GameManager.GM.cUM.OnDonTodhi();
        float count = 30;
        float time = 0.5f;
        for (int i = 0; i < count; i++)
        {
            _vecSlots[nNum].transform.localEulerAngles += new Vector3(0, 90f / count, 0);
            yield return new WaitForSeconds(time / count);
        }
        _vecSlots[nNum].Set(_vecCard[nNum]);
        if (_vecSlots[nNum].m_cCard.m_nCost == -1)
        {
            print("¸É¹ö È¹µæ : " + _vecSlots[nNum].m_cCard._eMT);
            int getMember = PlayerPrefs.GetInt(GameManager.GM.m_cPlayer.userno + "member" + _vecSlots[nNum].m_cCard._eMT);
            print(GameManager.GM.m_cPlayer.userno + "member" + _vecSlots[nNum].m_cCard._eMT + " : " + getMember);
            PlayerPrefs.SetInt(GameManager.GM.m_cPlayer.userno + "member" + _vecSlots[nNum].m_cCard._eMT, 1);
            print(GameManager.GM.m_cPlayer.userno + "member" + _vecSlots[nNum].m_cCard._eMT + " : 1·Î ÀúÀå");
            if (getMember == 0)
            {
                print("getMember : " + getMember + "  ÄÆ½Å½ÇÇà ");
                GameManager.GM.ShowMember("", _vecSlots[nNum].m_cCard);
            }
        }
        for (int i = 0; i < count; i++)
        {
            _vecSlots[nNum].transform.localEulerAngles -= new Vector3(0, 90f / count, 0);
            yield return new WaitForSeconds(time / count);
        }
        //if (_vecSlots[nNum].m_cCard.m_nCost != -1)
        //{
        //    GameManager.GM.ShowCard("", _vecSlots[nNum].m_cCard);
        //}
        GameManager.GM.cUM.OffDonTodhi();
    }
}
