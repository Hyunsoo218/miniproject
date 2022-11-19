using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobe : Sin
{
    // 로비 화면에서 싱행되는 것들을 관리함. Sin을 상속함
    public List<Sin> m_vecPanel = new List<Sin>();

    public void OnPanel(int nNum)
    {
        for (int i = 0; i < m_vecPanel.Count; i++)
        {
            m_vecPanel[i].Close();
        }

        m_vecPanel[nNum].Open();
    }
    public override void Open()
    {
        base.Open();
        OnPanel(0);
        OnPanel(2);
    }
}
