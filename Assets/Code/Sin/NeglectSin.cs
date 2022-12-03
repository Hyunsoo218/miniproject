using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeglectSin : Sin
{
    [SerializeField] Renderer _cBackground;
    [SerializeField] Transform _tGoldBox;
    [SerializeField] float _fSpeed;
    //static bool _bMove = true;
    public int Neg;
    public override void Open()
    {
        base.Open();
        if (Neg == 0)
        {
            Neg = PlayerPrefs.GetInt("Neg");
            if (Neg == 0)
            {
                GameManager.GM.ShowText("��ȭ�� �ֱ������� ���� �� �ִ� ���� Ȯ�� �� �� �ֽ��ϴ�.");
            }
            Neg++;
            PlayerPrefs.SetInt("Neg", Neg);
        }
        //else
        //{
        //    Neg = 0;
        //    PlayerPrefs.SetInt("Neg", Neg);
        //    print("�ʱ�ȭ �߽��ϴ�.");
        //}
    }
}
