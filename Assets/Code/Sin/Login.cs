using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : Sin
{
    public InputField inputID;
    public InputField inputPW;
    public void LoginTry()
    {
        GameManager.GM.cServer.loginBtn(inputID.text, inputPW.text);
    }
}
