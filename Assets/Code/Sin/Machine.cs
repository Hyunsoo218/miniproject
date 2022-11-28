using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : Sin
{
    [SerializeField] List<MemberType> _vecPicMember;
    
    public List<MemberType> PicupList{ get { return _vecPicMember; } }
    public override void Open()
    {
        base.Open();
    }
    public override void Close()
    {
        base.Close();
    }
}
