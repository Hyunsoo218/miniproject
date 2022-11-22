using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] List<Sin> m_vecSin = new List<Sin>();
    [SerializeField] RectTransform m_rScaleControll;
    [SerializeField] GameObject _objCamvas;
    [SerializeField] List<Sin> _cShowSin = new List<Sin>();

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
    void CloseAll()
    {
        for (int i = 0; i < m_vecSin.Count; i++)
        {
            m_vecSin[i].Close();
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
    public void ShowCard(string strText, Card cCard)
    {
        _cShowSin[1].Open(strText, cCard);
    }
    public void ShowMember(string strText, Member cMember)
    {
        _cShowSin[2].Open(strText, cMember);
    }
    public void Show10Card(string strText, List<Card> cCard) 
    {
        _cShowSin[3].Open(strText, cCard);
    }
}