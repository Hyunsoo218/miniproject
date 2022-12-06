using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCardSin : Sin
{
    [SerializeField] GameObject _obgUI;
    [SerializeField] GameObject _obgBangChi;
    [SerializeField] GameObject _obgWall;
    [SerializeField] float _fTime;
    public override void Open()
    {
        base.Open();
        _obgUI.SetActive(false);
        _obgBangChi.SetActive(false);
        _obgWall.SetActive(false);
        Invoke("Close", _fTime);
    }
    public override void Close()
    {
        base.Close();
        _obgUI.SetActive(true);
        _obgBangChi.SetActive(true);
        _obgWall.SetActive(true);
    }
}
