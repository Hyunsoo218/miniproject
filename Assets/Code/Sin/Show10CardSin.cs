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
    public override void Open(string strData, List<Card> cCard)
    {
        base.Open(strData, cCard);
        _txtTitle.text = strData;
        _vecCard = cCard;
        for (int i = 0; i < _vecSlots.Count; i++)
        {
            _vecSlots[i].Set(cCard[i]);
            _vecSlots[i].Boxing(_vecBoxing[(int)cCard[i].m_eCardRank]);
        }
    }
    public void ShowBig(int nNum) 
    {
        StartCoroutine(ShowCard(nNum));

        
    }
    IEnumerator ShowCard(int nNum)
    {
        float count = 120f;
        float time = 0.5f;

        for (int i = 0; i < count; i++)
        {
            _vecSlots[nNum].transform.localEulerAngles += new Vector3(0, 90f / count, 0);
            yield return new WaitForSeconds(time / count);
        }
        _vecSlots[nNum].Set(_vecCard[nNum]);

        if (_vecSlots[nNum].m_cCard.m_nCost == -1)
        {
            GameManager.GM.ShowMember("", _vecSlots[nNum].m_cCard);
        }

        for (int i = 0; i < count; i++)
        {
            _vecSlots[nNum].transform.localEulerAngles -= new Vector3(0, 90f / count, 0);
            yield return new WaitForSeconds(time / count);
        }
        if (_vecSlots[nNum].m_cCard.m_nCost != -1)
        {
            GameManager.GM.ShowCard("", _vecSlots[nNum].m_cCard);
        }
    }
}
