using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class RandomDraw : MonoBehaviour
{
    // 활성화,비활성화 할 오브젝트
    //*public enum CardGrades { S, A, B, C, D} //카드등급
    
    public GameObject CardDrawManager; //카드뽑기
    public GameObject PartnerDrawManager; //동료뽑기
    public GameObject RightBtn; //오른쪽 버튼 
    public GameObject LeftBtn; //왼쪽 버튼
    public GameObject DrawShop; // 뽑기 상점
    public GameObject DrawWindow; // 뽑는 창
    public GameObject OneImage; // 뽑는 창
    public GameObject TenImage; // 뽑는 창
    public GameObject ConfirmBtn; // 확인 버튼
    public GameObject RedrawBtn1; // 1회 뽑기
    public GameObject RedrawBtn2; // 10회 뽑기
    public GameObject PanelChange; // 뽑기 종류 변경
    public GameObject ObjBuilt;

    public GameObject aa;

    public Image[] arrDrawWindow;

    // 불러올 이미지 오브젝트
    public Image DrawImage; //이미지를 출력

    //숫자 이미지
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


    
    // 변수
    public int RandomInt; // 1회 뽑기 랜덤 변수
    public float f; // 랜덤 변수

    private void Update()
    {
        RandomInt = Random.Range(1, 20);  // 랜덤 범위를 설정
                                          //* RandomInt = Random.Range(1, 120); 
    }

    public void OneDraw() //1회 뽑기 버튼
    {
        DrawShop.SetActive(false); //뽑기 선택 화면을 비활성화
        DrawWindow.SetActive(true); // 랜덤 이미지를 출력한 화면을 활성화
        TenImage.SetActive(false); // 10회 뽑기 화면 결과창을 비활성화
        ConfirmBtn.SetActive(true); // 확인 버튼 활성화
        RedrawBtn1.SetActive(true); // 1회 뽑기 버튼 활성화
        RedrawBtn2.SetActive(false); // 10회 뽑기 버튼을 비활성화
        RightBtn.SetActive(false); //오른쪽 버튼 비활성화

        if (RandomInt == 1) // RandomInt가 1이라면
        {
            DrawImage.sprite = Image1; // DrawImage의 Sprite에 Image1(Sprite)를 적용
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




    public void TenDraw() //10회 뽑기 버튼 
    {
        DrawShop.SetActive(false); //뽑기 선택 화면을 비활성화
        DrawWindow.SetActive(true); // 랜덤 이미지를 출력한 화면을 활성화
        OneImage.SetActive(false); // 1회 뽑기 화면을 비활성화
        ConfirmBtn.SetActive(true); // 확인 버튼 활성화
        RedrawBtn1.SetActive(false); // 1회 뽑기 버튼 활성화
        RedrawBtn2.SetActive(true); // 10회 뽑기 버튼을 비활성화
        RightBtn.SetActive(false); //오른쪽 버튼 비활성화



        for (int i = 0; i < arrDrawWindow.Length; i++)
        {
            f = Random.Range(1, 20); //변수 값
            arrDrawWindow[i].gameObject.SetActive(true);

            RandomInt = (int)f;


            switch (RandomInt)
            {
                case 1: arrDrawWindow[i].sprite = Image1; break; //드로우이미지 안에 스프라이트를 이미지1로 바꿔라
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
       // Invoke("CloseDraw", 2.0f); // 2초 뒤에 CloseDraw 함수를 실행 
    }

    public void Confirm() // 확인버튼
    {
       Invoke("CloseDraw", 0.0f); // 0초 뒤에 CloseDraw 함수를 실행   
    }
    public void ReDraw1() // 1 재뽑기 버튼
    {
        Invoke("OneDraw",  0.0f);
    }

    public void ReDraw2() // 10 재뽑기 버튼
    {
        Invoke("TenDraw", 0.0f);
    }
    public void Changepanel() // 동료뽑기 활성화 
    {
        CardDrawManager.SetActive(true);
        PartnerDrawManager.SetActive(false);
        RightBtn.SetActive(true);
        LeftBtn.SetActive(false);
    }

    public void Changepanel2() //카드뽑기 활성화
    {
        CardDrawManager.SetActive(false);
        PartnerDrawManager.SetActive(true);
        RightBtn.SetActive(false);
        LeftBtn.SetActive(true);
    }

    public void CloseDraw() // 뽑기 스크립트가 실행되고 자동으로 실행
    {
        DrawImage.sprite = null; // 적용했던 이미지를 초기화
        DrawShop.SetActive(true); // 뽑기 선택 화면을 활성화 하고,
        DrawWindow.SetActive(false); // 랜덤 이미지 화면을 비활성화
        OneImage.SetActive(true); //1회 뽑기 결과 활성화
        TenImage.SetActive(true); //10회 뽑기 결과 활성화
        ConfirmBtn.SetActive(false);
        RedrawBtn1.SetActive(false);
        RedrawBtn2.SetActive(false);
        RightBtn.SetActive(true); //오른쪽 버튼 비활성화
    }
}