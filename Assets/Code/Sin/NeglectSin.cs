using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeglectSin : Sin
{
    [SerializeField] Renderer _cBackground;
    [SerializeField] Transform _tGoldBox;
    [SerializeField] float _fSpeed;
    static bool _bMove = true;
    private void Update()
    {
        if (_bMove)
        {
            _cBackground.material.mainTextureOffset += new Vector2(Time.deltaTime * 0.5f, 0);
            _tGoldBox.Translate(Vector3.left * Time.deltaTime * _fSpeed);
        }
    }
    public static void MoveBackGround()
    {
        _bMove = true;
    }
    public static void StopBackGround()
    {
        _bMove = false;
    }
}
