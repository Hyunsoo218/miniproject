using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeglectSin : Sin
{
    bool Click = true;
    public override void Open()
    {
        base.Open();
        if (Click == true)
        {
            GameManager.GM.ShowText("��ȭ�� �ֱ������� ���� �� �ִ� ���� Ȯ�� �� �� �ֽ��ϴ�.");
            Click = false;
        }
        else if (Click == false)
        {

        }
    }
    
}
