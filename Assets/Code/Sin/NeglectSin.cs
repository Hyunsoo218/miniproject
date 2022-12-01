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
    public override void Open()
    {
        base.Open();
        GameManager.GM.ShowText("재화를 주기적으로 얻을 수 있는 것을 확인 할 수 있습니다.");
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
