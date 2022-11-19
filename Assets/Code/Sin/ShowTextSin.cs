using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextSin : Sin
{
    public Text m_txtShow;
    public override void Open(string strData)
    {
        base.Open();
        m_txtShow.text = strData;
    }
}
