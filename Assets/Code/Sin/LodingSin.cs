using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LodingSin : Sin
{
    [SerializeField] List<string> _vecText;
    [SerializeField] Text _txtText;
    public override void Open()
    {
        base.Open();
        _txtText.text = _vecText[Random.Range(0, _vecText.Count)];
    }
}
