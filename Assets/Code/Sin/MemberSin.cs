using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberSin : Sin
{
    [SerializeField] List<Image> _vecImage = new List<Image>();
    [SerializeField] List<Text> _vecText = new List<Text>();
    [SerializeField] Sprite _imgNull;
    [SerializeField] GameObject _objSelectMember;
    [SerializeField] Transform _tMember;
    [SerializeField] GameObject _objMemberBut;
    [SerializeField] Image _imgMember;
    [SerializeField] GameObject _objMemberData;
    [SerializeField] Text _txtMemberName;
    [SerializeField] Text _txtMemberData;
    List<Card> _vecSelectMember;
    List<Card> _vecMyMember;
    List<GameObject> _vecMemberBut = new List<GameObject>();
    Card _cMember;
    int _nChageTarget;
    bool _bShowMemberData;

    public override void Open()
    {
        GameManager.GM.ShowText("동료를 뽑기(상점)에서 획득하고\n정보 확인, 관리(장착/해제)할 수 있습니다.");

        _objMemberData.SetActive(false);
        _bShowMemberData = false;
        _vecSelectMember = new List<Card>();
        _objSelectMember.SetActive(false);
        base.Open();
        _vecSelectMember.AddRange(GameManager.GM.m_cPlayer.m_cAvata._vecUseMember);
        for (int i = 0; i < _vecSelectMember.Count; i++)
        {
            if (_vecSelectMember[i] != null)
            {
                _vecImage[i].sprite = _vecSelectMember[i]._imgCard;
                _vecText[i].text = _vecSelectMember[i]._strName;
            }
            else
            {
                _vecImage[i].sprite = _imgNull;
                _vecText[i].text = "";
            }
        }
    }
    public void ChangBut(int nNum)
    {
        for (int i = 0; i < _vecMemberBut.Count; i++)
        {
            Destroy(_vecMemberBut[i].gameObject);
        }
        _vecMemberBut.Clear();
        _nChageTarget = nNum;
        _objSelectMember.SetActive(true);
        _imgMember.sprite = _imgNull;
        _vecMyMember = new List<Card>();
        _vecMyMember.AddRange(GameManager.GM.m_cPlayer.m_cAvata._vecMyMember);
        for (int i = 0; i < _vecSelectMember.Count; i++)
        {
            if (_vecSelectMember[i] != null)
            {
                _vecMyMember.Remove(_vecSelectMember[i]);
            }
        }
        for (int i = 0; i < _vecMyMember.Count; i++)
        {
            GameObject temp = Instantiate(_objMemberBut, _tMember);
            Button but = temp.GetComponent<Button>();
            but.image.sprite = _vecMyMember[i]._imgCard;

            int index = i;
            but.onClick.AddListener(() => 
            {
                SelectBut(_vecMyMember[index]); 
            });

            //switch (i)
            //{
            //    case 0: but.onClick.AddListener(() => SelectBut(_vecMyMember[0])); break;
            //    case 1: but.onClick.AddListener(() => SelectBut(_vecMyMember[1])); break;
            //    case 2: but.onClick.AddListener(() => SelectBut(_vecMyMember[2])); break;
            //    case 3: but.onClick.AddListener(() => SelectBut(_vecMyMember[3])); break;
            //    case 4: but.onClick.AddListener(() => SelectBut(_vecMyMember[4])); break;
            //    case 5: but.onClick.AddListener(() => SelectBut(_vecMyMember[5])); break;
            //}
            _vecMemberBut.Add(temp);
        }
        _tMember.GetComponent<RectTransform>().sizeDelta = new Vector2((300f * _vecMyMember.Count) - 50f, 100f);
        _cMember = null;
    }
    public void SelectBut(Card cMember)
    {
        _cMember = cMember;
        _txtMemberName.text = _cMember._strName;
        _txtMemberData.text = _cMember._strData;
        _imgMember.sprite = _cMember._imgFull;
    }
    public void SubmitBut()
    {

        if (_cMember == null)
        {
            for (int i = 0; i < _vecMemberBut.Count; i++)
            {
                Destroy(_vecMemberBut[i]);
            }
            _cMember = null;
            Open();
            return;
        }
        else
        {
            GameManager.GM.m_cPlayer.m_cAvata._vecUseMember[_nChageTarget] = _cMember;
            for (int i = 0; i < _vecMemberBut.Count; i++)
            {
                Destroy(_vecMemberBut[i]);
            }
            Open();
            _cMember = null;
        }
    }
    public void ShowMemberData()
    {
        if (_cMember == null) return;

        if (_bShowMemberData == false)
        {
            _bShowMemberData = true;
            _objMemberData.SetActive(true);
        }
        else
        {
            _bShowMemberData = false;
            _objMemberData.SetActive(false);
        }
    }
}
