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
            GameManager.GM.ShowText("재화를 주기적으로 얻을 수 있는 것을 확인 할 수 있습니다.");
            Click = false;
        }
        else if (Click == false)
        {

        }
    }
    
}
