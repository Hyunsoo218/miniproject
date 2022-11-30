using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    public InputField InputName;
    public string[] userData;
    private int gold = 100;
    
    public void loginBtn(string id, string pwd)
    {
        print("로그인 시작");
        StartCoroutine(ServerLoginUser(id,pwd));
        print("로그인 종료");
    }
    public void registerBtn()
    {
        StartCoroutine(ServerMakeUser());
    }
    public void GetBewCard(CardData data)
    {
        StartCoroutine(GetBewCardCo(data));
    }
    public void StageClear(StageData data)
    {
        StartCoroutine(StageClearCo(data));
    }
    IEnumerator StageClearCo(StageData data)
    {
        WWWForm form = new WWWForm();


        form.AddField("userno", GameManager.GM.m_cPlayer.userno + "");
        form.AddField("d1", "1");
        form.AddField("d2", data.m_strStage);

        UnityWebRequest www = UnityWebRequest.Post("http://10.30.5.141:3030/stageClear", form);
        yield return www.SendWebRequest();
    }
    IEnumerator GetBewCardCo(CardData data) 
    {
        WWWForm form = new WWWForm();


        form.AddField("userno", GameManager.GM.m_cPlayer.userno+"");
        form.AddField("d1", data.m_eCardType);
        form.AddField("d2", data.m_eCardRank);
        form.AddField("d3", data.m_nCost);
        form.AddField("d4", data.m_nLevel);
        form.AddField("d5", data.m_nMaxLevel);
        form.AddField("d6", data.m_nUnlimite);
        form.AddField("d7", data.m_nLevelUpGold + "");
        form.AddField("d8", data.m_fAp+"");
        form.AddField("d9", data.m_fHp + "");
        form.AddField("d10", data._eMT);


        UnityWebRequest www = UnityWebRequest.Post("http://10.30.5.141:3030/insertCard", form);
        yield return www.SendWebRequest();
    }
    IEnumerator ServerLoginUser(string id, string pwd)
    {
        print("로그인 코루틴 시작");
        WWWForm form = new WWWForm();
        form.AddField("ID", id);
        form.AddField("PW", pwd);
        UnityWebRequest www = UnityWebRequest.Post("http://10.30.5.141:3030/login_user", form);

        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            print(www.downloadHandler.text);
            print("asdadad");
            GameManager.GM.ShowText("�α��ο� ����");
        }
        if (www.downloadHandler.text == "101")
        {
            print("아이디");
            Debug.Log("오류");
        }
        else if (www.downloadHandler.text == "102")
        {
            print("비밀번호 오류");
        }
        else
        {
            Debug.Log("Login Success");
            // 제화, 인트로 불러오기 시작

            UserData tempPl = JsonUtility.FromJson<UserData>(www.downloadHandler.text);
            print(www.downloadHandler.text);
      
            GameManager.GM.m_cPlayer.userno = tempPl.userno;
            GameManager.GM.m_cPlayer.m_strId = tempPl.userID;
            GameManager.GM.m_cPlayer.m_strPass = tempPl.userPW;
            GameManager.GM.m_cPlayer.m_strName = tempPl.userName;
            GameManager.GM.m_cPlayer.m_nGold = tempPl.m_nGold;
            GameManager.GM.m_cPlayer.m_nDiamond = tempPl.m_nDiamond;
            GameManager.GM.m_cPlayer.m_nGas = tempPl.m_nGas;
            GameManager.GM.m_cPlayer._bFirst = tempPl._bFirst;

            print(GameManager.GM.m_cPlayer.ToString());

            // 제화, 인트로 불러오기 끝

            GameManager.GM.m_cPlayer.AllSet(); // ok

            form.AddField("userno", tempPl.userno);
            www = UnityWebRequest.Post("http://10.30.5.141:3030/GetCardData", form); //  카드값 주는 주소로 변경
            yield return www.SendWebRequest();
            print(www.downloadHandler.text);

            var CardData = JsonHelper.FromJson<CardData>(www.downloadHandler.text);
            for (int i = 0; i < CardData.Length; i++)
            {
                if (CardData[i].m_nCost == -1)
                {
                    Member temp = GameManager.GM.cMM.GetMember((MemberType)CardData[i]._eMT);
                    GameManager.GM.m_cPlayer.m_cAvata._vecMyMember.Add(temp);
                }
                else
                {
                    Card temp = GameManager.GM.cCM.GetCard((CardType)CardData[i].m_eCardType, (CardRank)CardData[i].m_eCardRank);// 카드

                    temp.m_nLevel = CardData[i].m_nLevel;
                    temp.m_nMaxLevel = CardData[i].m_nMaxLevel;
                    temp.m_nUnlimite = CardData[i].m_nUnlimite;
                    temp.m_nLevelUpGold = CardData[i].m_nLevelUpGold;
                    temp.m_fAp = CardData[i].m_fAp;
                    temp.m_fHp = CardData[i].m_fHp;
                    temp.cardno = CardData[i].cardno;

                    GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard.Add(temp);
                }
            }
            // 카드 데이터 클레스 만들어서 작업 다시 끝
            www = UnityWebRequest.Post("http://10.30.5.141:3030/GetStageData", form); //  스테이지 주는 주소로 변경
            yield return www.SendWebRequest();
            var stageData = JsonHelper.FromJson<StageData>(www.downloadHandler.text);
            for (int i = 0; i < stageData.Length; i++)
            {
                StageData s = stageData[i];
                GameManager.GM.cSM.SetStage(s.m_strStage, s.m_bClear);
                print("스테이지 : " + s.m_strStage + "  클리어: "+ s.m_bClear);

            }
            

            GameManager.GM.GoTitle();
            www.Dispose();
            print("로그인 코루틴 끝");
        }

    }
    IEnumerator ServerMakeUser()
    {
        WWWForm form = new WWWForm();
        //form.AddField("ID", inputID.text);
        //form.AddField("PW", inputPW.text);
        form.AddField("NAME", InputName.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:3030/make_user", form);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

        }
    }
    IEnumerator GoldSend(long gold)
    {
        WWWForm form = new WWWForm();
        form.AddField("GoldSend", "" + gold);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:3030/gold_send", form);

        yield return www.SendWebRequest();
        www.uploadHandler.Dispose();
        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            //Myinfotext[5].text = 

        }
    }
    IEnumerator DiaSend(long dia)
    {
        WWWForm form = new WWWForm();
        form.AddField("DiaSend", "" + dia);
        print(userData[0]);
        form.AddField("userno", userData[0]);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:3030/gold_send", form);

        yield return www.SendWebRequest();
        www.uploadHandler.Dispose();
        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            //Myinfotext[5].text = 

        }
    }


}
[System.Serializable]
public class UserData
{
    public int userno;
    public string userID;
    public string userPW;
    public string userName;
    public long m_nGold;     // 골드
    public int m_nDiamond;  // 다이아
    public int m_nGas;      // 피로도 
    public bool _bFirst = false; //
}
[System.Serializable]
public class PresetData
{
    public int userno;
    public int presetno;
    public int c1;
    public int c2;
    public int c3;
    public int c4;
    public int c5;
    public int c6;
    public int c7;
    public int c8;
    public int c9;

}
[System.Serializable]
public class CardData
{
    public int m_eCardType;    //
    public int m_eCardRank;    //
    public int m_nCost;             //
    public int m_nLevel = 1;        //
    public int m_nMaxLevel = 30;    //
    public int m_nUnlimite = 0;     //
    public long m_nLevelUpGold = 10;//
    public float m_fAp = 3;         //
    public float m_fHp = 10;        //
    public int _eMT;         //
    public int cardno;         //
}
[System.Serializable]
public class StageData
{
    public bool m_bClear = false;   // 1
    public string m_strStage;       // 2
}