using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : Sin
{
    [SerializeField] GameObject _objLogin;
    [SerializeField] GameObject _objMakeUser;
    public InputField inputID;
    public InputField inputPW;
    [SerializeField] InputField _inId;
    [SerializeField] InputField _inPwd;
    [SerializeField] InputField _inName;
    public override void Open()
    {
        base.Open();
        Cancel();
        inputID.text = "";
        inputPW.text = "";
        _inId.text = "";
        _inPwd.text = "";
        _inName.text = "";
        string lastId = PlayerPrefs.GetString("lastLoginId");
        string lastPw = PlayerPrefs.GetString("lastLoginPw");
        if (lastId != "" && lastPw != "")
        {
            GameManager.GM.cServer.loginBtn(lastId, lastPw);
        }
    }
    public void LoginTry()
    {
        GameManager.GM.cServer.loginBtn(inputID.text, inputPW.text);
    }
    public void ChageFrom()
    {
        _objLogin.SetActive(false);
        _objMakeUser.SetActive(true);
    }
    public void Cancel()
    {
        _objLogin.SetActive(true);
        _objMakeUser.SetActive(false);
    }
    public void MakeUser()
    {
        if (_inId.text == "")
        {
            GameManager.GM.ShowText("���̵� �Է��ϼ���");
            return;
        }
        if (_inPwd.text == "")
        {
            GameManager.GM.ShowText("��й�ȣ�� �Է��ϼ���");
            return;
        }
        if (_inName.text == "")
        {
            GameManager.GM.ShowText("�г����� �Է��ϼ���");
            return;
        }
        UserData data = new UserData()
        {
            userID = _inId.text,
            userPW = _inPwd.text,
            userName = _inName.text,
            m_nGold = 0,
            m_nDiamond = 0,
            m_nGas = 100,
            _bFirst = true
        };
        GameManager.GM.cServer.MakeUser(data);
    }
}