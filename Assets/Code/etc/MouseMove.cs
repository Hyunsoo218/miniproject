using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseMove : MonoBehaviour
{
    [SerializeField] RectTransform _tMouse;
    bool _bRight = true;
    Vector3 _tStratPos;
    float _fX;
    float _fY;
    private void Start()
    {
        _tStratPos = _tMouse.localPosition;
        _fX = _tStratPos.x;
        _fY = _tStratPos.y;
    }
    void Update()
    {
        if (_bRight)
        {
            _fX += 400f * Time.deltaTime;
            _tMouse.localPosition = new Vector3(_fX, _fY, 0);
            if (_fX > 300f)
            {
                _bRight = false;
            }
        }
        else
        {
            _fX -= 400f * Time.deltaTime;
            _tMouse.localPosition = new Vector3(_fX, _fY, 0);
            if (_fX < -300f)
            {
                _bRight = true;
            }
        }
    }
}
