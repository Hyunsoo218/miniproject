using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class TalkSin : Sin
{
    Act _cAct;
    StringReader ItemDataString;
    string _strLine;
    [SerializeField] Image _imgBack;
    [SerializeField] Image _imgTalker;
    [SerializeField] Image _imgFade;
    [SerializeField] Text _txtName;
    [SerializeField] Text _txtChat;
    string value = "";
    bool _bAuto = false;
    public override void Open(Act cAct)
    {
        base.Open(cAct);
        _cAct = cAct;
        Load(cAct);
        ActionCsv();
        if (_bAuto) StartCoroutine(AutoStory());
        else StopAllCoroutines();
        StartCoroutine(FadeOut());
    }
    void Load(Act cAct)
    {
        TextAsset ItemData = Resources.Load(cAct._strFileName) as TextAsset;
        ItemDataString = new StringReader(ItemData.text);
        _strLine = ItemDataString.ReadLine();
    }
    public void ActionTxt()
    {
        _strLine = ItemDataString.ReadLine();

        if (_strLine != null)
        {
            string[] arrVal = _strLine.Split("/");

            _txtName.text = arrVal[0];
            _txtChat.text = arrVal[1];
            _imgBack.sprite = GameManager.GM.cIM.GetBackImage((BackImageType)int.Parse(arrVal[2]));
            _imgTalker.sprite = GameManager.GM.cIM.GetHumenImage((HumenImageType)int.Parse(arrVal[3]));
            switch (int.Parse(arrVal[4]))
            {
                case 1: GameManager.GM.cCSM.CameraShake(_imgBack.rectTransform, int.Parse(arrVal[5]), ShakeMode.Nomal); break;
            }
        }
        else
        {
            StartCoroutine(FadeIn());
        }
    }
    public void ActionCsv()
    {
        _strLine = ItemDataString.ReadLine();

        if (_strLine != null)
        {
            string[] arrVal = _strLine.Split(",");

            _txtName.text = arrVal[0];
            _txtChat.text = arrVal[1];
            _imgBack.sprite = GameManager.GM.cIM.GetBackImage((BackImageType)int.Parse(arrVal[2]));
            _imgTalker.sprite = GameManager.GM.cIM.GetHumenImage((HumenImageType)int.Parse(arrVal[3]));
            switch (int.Parse(arrVal[4]))
            {
                case 1: GameManager.GM.cCSM.CameraShake(_imgBack.rectTransform, int.Parse(arrVal[5]), ShakeMode.Nomal); break;
            }
            GameManager.GM.cSoM.PlaySound((SoundObject)int.Parse(arrVal[6]));
        }
        else
        {
            StartCoroutine(FadeIn());
        }
    }
    IEnumerator FadeIn()
    {
        _imgFade.gameObject.SetActive(true);
        for (float i = 0; i <= 40; i++)
        {
            Color temp = _imgFade.color;
            temp.a = i / 40f;
            _imgFade.color = temp;
            yield return new WaitForSeconds(0.025f);
        }
        GameManager.GM.cSoM.Stop();
        GameManager.GM.RunAct(_cAct._eNextAction);
    }
    IEnumerator FadeOut()
    {
        _imgFade.gameObject.SetActive(true);
        for (float i = 40; i >= 0; i--)
        {
            Color temp = _imgFade.color;
            temp.a = i / 40f;
            _imgFade.color = temp;
            yield return new WaitForSeconds(0.025f);
        }
        _imgFade.gameObject.SetActive(false);
    }
    public void Skip()
    {
        StartCoroutine(FadeIn());
    }
    public void OnAuto()
    {
        if (_bAuto)
        {
            StopAuto();
        }
        else
        {
            StartAuto();
        }
    }
    void StartAuto()
    {
        _bAuto = true;
        StartCoroutine(AutoStory());
    }
    void StopAuto()
    {
        _bAuto = false;
        StopAllCoroutines();
    }
    IEnumerator AutoStory()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(3f);
            ActionCsv();
        }
    }
}
