using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSin : Sin
{

    public void Card_1() 
    {
        GameManager.GM.GetCard(GameManager.GM.cCM.GetRendom_1());
    }
    public void Card_10() 
    {
        GameManager.GM.GetCard(GameManager.GM.cCM.GetRendom_10());

    }

}

