using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobe : Sin
{
    // �κ� ȭ�鿡�� ����Ǵ� �͵��� ������. Sin�� �����
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
