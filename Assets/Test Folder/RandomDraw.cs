using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class RandomDraw : MonoBehaviour
{
    // Ȱ��ȭ,��Ȱ��ȭ �� ������Ʈ
    //*public enum CardGrades { S, A, B, C, D} //ī����
    
    public GameObject CardDrawManager; //ī��̱�
    public GameObject PartnerDrawManager; //����̱�
    public GameObject RightBtn; //������ ��ư 
    public GameObject LeftBtn; //���� ��ư
    public GameObject DrawShop; // �̱� ����
    public GameObject DrawWindow; // �̴� â
    public GameObject OneImage; // �̴� â
    public GameObject TenImage; // �̴� â
    public GameObject ConfirmBtn; // Ȯ�� ��ư
    public GameObject RedrawBtn1; // 1ȸ �̱�
    public GameObject RedrawBtn2; // 10ȸ �̱�
    public GameObject PanelChange; // �̱� ���� ����
    public GameObject ObjBuilt;

    public GameObject aa;

    public Image[] arrDrawWindow;

    // �ҷ��� �̹��� ������Ʈ
    public Image DrawImage; //�̹����� ���

    //���� �̹���
    public Sprite Image1;
    public Sprite Image2;
    public Sprite Image3;
    public Sprite Image4;
    public Sprite Image5;
    public Sprite Image6;
    public Sprite Image7;
    public Sprite Image8;
    public Sprite Image9;
    public Sprite Image10;
    public Sprite Image11;
    public Sprite Image12;
    public Sprite Image13;
    public Sprite Image14;
    public Sprite Image15;
    public Sprite Image16;
    public Sprite Image17;
    public Sprite Image18;
    public Sprite Image19;
    public Sprite Image20;


    
    // ����
    public int RandomInt; // 1ȸ �̱� ���� ����
    public float f; // ���� ����

    private void Update()
    {
        RandomInt = Random.Range(1, 20);  // ���� ������ ����
                                          //* RandomInt = Random.Range(1, 120); 
    }

    public void OneDraw() //1ȸ �̱� ��ư
    {
        DrawShop.SetActive(false); //�̱� ���� ȭ���� ��Ȱ��ȭ
        DrawWindow.SetActive(true); // ���� �̹����� ����� ȭ���� Ȱ��ȭ
        TenImage.SetActive(false); // 10ȸ �̱� ȭ�� ���â�� ��Ȱ��ȭ
        ConfirmBtn.SetActive(true); // Ȯ�� ��ư Ȱ��ȭ
        RedrawBtn1.SetActive(true); // 1ȸ �̱� ��ư Ȱ��ȭ
        RedrawBtn2.SetActive(false); // 10ȸ �̱� ��ư�� ��Ȱ��ȭ
        RightBtn.SetActive(false); //������ ��ư ��Ȱ��ȭ

        if (RandomInt == 1) // RandomInt�� 1�̶��
        {
            DrawImage.sprite = Image1; // DrawImage�� Sprite�� Image1(Sprite)�� ����
        }
        else if (RandomInt == 2)
        {
            DrawImage.sprite = Image2;
        }
        else if (RandomInt == 3)
        {
            DrawImage.sprite = Image3;
        }
        else if (RandomInt == 4)
        {
            DrawImage.sprite = Image4;
        }
        else if (RandomInt == 5)
        {
            DrawImage.sprite = Image5;
        }
        else if (RandomInt == 6)
        {
            DrawImage.sprite = Image6;
        }
        else if (RandomInt == 7)
        {
            DrawImage.sprite = Image7;
        }
        else if (RandomInt == 8)
        {
            DrawImage.sprite = Image8;
        }
        else if (RandomInt == 9)
        {
            DrawImage.sprite = Image9;
        }
        else if (RandomInt == 10)
        {
            DrawImage.sprite = Image10;
        }
        else if (RandomInt == 11)
        {
            DrawImage.sprite = Image11;
        }
        else if (RandomInt == 12)
        {
            DrawImage.sprite = Image12;
        }
        else if (RandomInt == 13)
        {
            DrawImage.sprite = Image13;
        }
        else if (RandomInt == 14)
        {
            DrawImage.sprite = Image14;
        }
        else if (RandomInt == 15)
        {
            DrawImage.sprite = Image15;
        }
        else if (RandomInt == 16)
        {
            DrawImage.sprite = Image16;
        }
        else if (RandomInt == 17)
        {
            DrawImage.sprite = Image17;
        }
        else if (RandomInt == 18)
        {
            DrawImage.sprite = Image18;
        }
        else if (RandomInt == 19)
        {
            DrawImage.sprite = Image19;
        }
        else if (RandomInt == 20)
        {
            DrawImage.sprite = Image20;
        }
 
    }

    void Start()
    {
         
    }




    public void TenDraw() //10ȸ �̱� ��ư 
    {
        DrawShop.SetActive(false); //�̱� ���� ȭ���� ��Ȱ��ȭ
        DrawWindow.SetActive(true); // ���� �̹����� ����� ȭ���� Ȱ��ȭ
        OneImage.SetActive(false); // 1ȸ �̱� ȭ���� ��Ȱ��ȭ
        ConfirmBtn.SetActive(true); // Ȯ�� ��ư Ȱ��ȭ
        RedrawBtn1.SetActive(false); // 1ȸ �̱� ��ư Ȱ��ȭ
        RedrawBtn2.SetActive(true); // 10ȸ �̱� ��ư�� ��Ȱ��ȭ
        RightBtn.SetActive(false); //������ ��ư ��Ȱ��ȭ



        for (int i = 0; i < arrDrawWindow.Length; i++)
        {
            f = Random.Range(1, 20); //���� ��
            arrDrawWindow[i].gameObject.SetActive(true);

            RandomInt = (int)f;


            switch (RandomInt)
            {
                case 1: arrDrawWindow[i].sprite = Image1; break; //��ο��̹��� �ȿ� ��������Ʈ�� �̹���1�� �ٲ��
                case 2: arrDrawWindow[i].sprite = Image2; break;
                case 3: arrDrawWindow[i].sprite = Image3; break;
                case 4: arrDrawWindow[i].sprite = Image4; break;
                case 5: arrDrawWindow[i].sprite = Image5; break;
                case 6: arrDrawWindow[i].sprite = Image6; break;
                case 7: arrDrawWindow[i].sprite = Image7; break;
                case 8: arrDrawWindow[i].sprite = Image8; break;
                case 9: arrDrawWindow[i].sprite = Image9; break;
                case 10: arrDrawWindow[i].sprite = Image10; break;
                case 11: arrDrawWindow[i].sprite = Image11; break;
                case 12: arrDrawWindow[i].sprite = Image12; break;
                case 13: arrDrawWindow[i].sprite = Image13; break;
                case 14: arrDrawWindow[i].sprite = Image14; break;
                case 15: arrDrawWindow[i].sprite = Image15; break;
                case 16: arrDrawWindow[i].sprite = Image16; break;
                case 17: arrDrawWindow[i].sprite = Image17; break;
                case 18: arrDrawWindow[i].sprite = Image18; break;
                case 19: arrDrawWindow[i].sprite = Image19; break;
                case 20: arrDrawWindow[i].sprite = Image20; break;

            }
        }
       // Invoke("CloseDraw", 2.0f); // 2�� �ڿ� CloseDraw �Լ��� ���� 
    }

    public void Confirm() // Ȯ�ι�ư
    {
       Invoke("CloseDraw", 0.0f); // 0�� �ڿ� CloseDraw �Լ��� ����   
    }
    public void ReDraw1() // 1 ��̱� ��ư
    {
        Invoke("OneDraw",  0.0f);
    }

    public void ReDraw2() // 10 ��̱� ��ư
    {
        Invoke("TenDraw", 0.0f);
    }
    public void Changepanel() // ����̱� Ȱ��ȭ 
    {
        CardDrawManager.SetActive(true);
        PartnerDrawManager.SetActive(false);
        RightBtn.SetActive(true);
        LeftBtn.SetActive(false);
    }

    public void Changepanel2() //ī��̱� Ȱ��ȭ
    {
        CardDrawManager.SetActive(false);
        PartnerDrawManager.SetActive(true);
        RightBtn.SetActive(false);
        LeftBtn.SetActive(true);
    }

    public void CloseDraw() // �̱� ��ũ��Ʈ�� ����ǰ� �ڵ����� ����
    {
        DrawImage.sprite = null; // �����ߴ� �̹����� �ʱ�ȭ
        DrawShop.SetActive(true); // �̱� ���� ȭ���� Ȱ��ȭ �ϰ�,
        DrawWindow.SetActive(false); // ���� �̹��� ȭ���� ��Ȱ��ȭ
        OneImage.SetActive(true); //1ȸ �̱� ��� Ȱ��ȭ
        TenImage.SetActive(true); //10ȸ �̱� ��� Ȱ��ȭ
        ConfirmBtn.SetActive(false);
        RedrawBtn1.SetActive(false);
        RedrawBtn2.SetActive(false);
        RightBtn.SetActive(true); //������ ��ư ��Ȱ��ȭ
    }
}