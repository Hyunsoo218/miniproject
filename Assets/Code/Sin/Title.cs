using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : Sin
{   
    // Ÿ��Ʋ ȭ�鿡�� ����Ǵ� �͵��� ������. Sin�� �����
    public Text m_tTapToStart;
    float fA = 255f;
    float fAddA = 100f;

    private void Update()
    {
        fA += fAddA * Time.deltaTime;

        if (fA > 255)
        {
            fA = 255;
            fAddA = -fAddA;
        }
        if (fA < 50)
        {
            fA = 50;
            fAddA = -fAddA;
        }

        m_tTapToStart.color = new Color(1, 1, 0, fA / 255f);
    }
}
