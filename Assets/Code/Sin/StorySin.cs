using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySin : Sin
{
    public List<Sin> list = new List<Sin>();

    public override void Open()
    {
        base.Open( );

        for (int i = 0; i < list.Count; i++)
        {
            list[i].Close();
        }
    }
}
