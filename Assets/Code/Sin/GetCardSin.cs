using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCardSin : Sin
{
    [SerializeField] GameObject _obgMovie;
    [SerializeField] float _fTime;
    public override void Open()
    {
        base.Open();
        _obgMovie.SetActive(false);
        _obgMovie.SetActive(true);
        Invoke("Close", _fTime);
    }
    public override void Close()
    {
        base.Close();

    }
}
