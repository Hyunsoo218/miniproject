using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] List<Sin> m_vecSin = new List<Sin>();
    [SerializeField] RectTransform m_rScaleControll;
    [SerializeField] GameObject _objCamvas;
    [SerializeField] List<Sin> _cShowSin = new List<Sin>();
    [SerializeField] Sin _cMemberCutSin;
    [SerializeField] Text _txtMemberCutSinScript;
    [SerializeField] Image _imgMemberCutSin;
    [SerializeField] Sin _cOffLineGold;
    [SerializeField] Sin _cLoding;

    private void Start()
    {
        float fX = Screen.width / 1242f;
        float fY = Screen.height / 2688f;
        float fTemp;

        if (fX < fY) fTemp = fX;
        else fTemp = fY;

        m_rScaleControll.localScale = new Vector3(fTemp, fTemp, 0);
    }
    public void SetSin(GameState GS)
    {
        switch (GS)
        {
            case GameState.Login:
            case GameState.Title:
            case GameState.Lobe:
            case GameState.Game:
                Time.timeScale = 1;
                CloseAll();
                break;

            case GameState.ReStart:
                m_vecSin[(int)GS].Close();
                Time.timeScale = 1;
                return;

            case GameState.Stop:
            case GameState.Win:
            case GameState.Lose:
            case GameState.LaidEnd:
                Time.timeScale = 0;
                break;
        }
        m_vecSin[(int)GS].Open();
    }
    public void SetSin(Stage cStage)
    {
        CloseAll();
        m_vecSin[(int)GameState.Game].Open(cStage);
    }
    public void SetSin(Act cAct)
    {
        CloseAll();
        m_vecSin[(int)GameState.Talk].Open(cAct);
    }
    void CloseAll()
    {
        for (int i = 0; i < m_vecSin.Count; i++)
        {
            m_vecSin[i].Close();
        }
        for (int i = 0; i < _cShowSin.Count; i++)
        {
            _cShowSin[i].Close();
        }
    }
    public void CloseUI()
    {
        _objCamvas.SetActive(false);
    }
    public void OpenUI()
    {
        _objCamvas.SetActive(true);
    }
    public void ShowText(string strText)
    {
        _cShowSin[0].Open(strText);
    }
    public void ShowTextEnd()
    {
        _cShowSin[0].Close();
    }
    public void ShowCard(string strText, Card cCard)
    {
        _cShowSin[1].Open(strText, cCard);
    }
    public void ShowMember(string strText, Card cMember)
    {
        GetMemberCutSin(strText, cMember);
    }
    public void Show10Card(string strText, List<Card> cCard) 
    {
        _cShowSin[3].Open(strText, cCard);
    }
    void GetMemberCutSin(string strText, Card cMember)
    {
        StartCoroutine(MemberCut(strText, cMember));
    }
    IEnumerator MemberCut(string strText, Card cMember)
    {
        float count = 30f;
        float time = 1;
        _cMemberCutSin.Open();
        _txtMemberCutSinScript.text = cMember._strScript;
        _txtMemberCutSinScript.color = new Color(1,1,1,0);
        _imgMemberCutSin.gameObject.SetActive(false);
        for (int i = 0; i < count; i++)
        {
            _txtMemberCutSinScript.color += new Color(0, 0, 0, 1 / count);
            yield return new WaitForSeconds(time / count);
        }

        yield return new WaitForSeconds(1);
        _imgMemberCutSin.sprite = cMember.m_imgImage;
        _imgMemberCutSin.gameObject.SetActive(true);
        _imgMemberCutSin.rectTransform.localScale = new Vector3(3,3,1);
        _imgMemberCutSin.rectTransform.anchoredPosition = new Vector2(-1200, 2663);
        for (int i = 0; i < count; i++)
        {
            Vector2 temp = _imgMemberCutSin.rectTransform.anchoredPosition;
            temp += new Vector2(2400 / count, 0);
            _imgMemberCutSin.rectTransform.anchoredPosition = temp;
            yield return new WaitForSeconds(time / count);
        }
        _imgMemberCutSin.rectTransform.anchoredPosition = new Vector2(1200, -388);
        for (int i = 0; i < count; i++)
        {
            Vector2 temp = _imgMemberCutSin.rectTransform.anchoredPosition;
            temp -= new Vector2(2400 / count, 0);
            _imgMemberCutSin.rectTransform.anchoredPosition = temp;
            yield return new WaitForSeconds(time / count);
        }
        _imgMemberCutSin.rectTransform.localScale = new Vector3(2, 2, 1);
        _imgMemberCutSin.rectTransform.anchoredPosition = new Vector2(0, 1300);
        for (int i = 0; i < count; i++)
        {
            Vector2 temp = _imgMemberCutSin.rectTransform.anchoredPosition;
            temp -= new Vector2(0, 2600 / count);
            _imgMemberCutSin.rectTransform.anchoredPosition = temp;
            yield return new WaitForSeconds(time / count);
        }
        _imgMemberCutSin.rectTransform.localScale = new Vector3(1, 1, 1);
        _imgMemberCutSin.rectTransform.anchoredPosition = new Vector2(0, 0);
        yield return new WaitForSeconds(2);
        _cMemberCutSin.Close();
        _cShowSin[2].Open(strText, cMember);
    }
    public void OnOffLineGold()
    {
        _cOffLineGold.Open();
    }
    public void OnLoding()
    {
        _cLoding.Open();
    }
    public void OffLoding()
    {
        _cLoding.Close();
    }
}