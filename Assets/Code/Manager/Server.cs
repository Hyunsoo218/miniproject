using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    public InputField InputName;
    public string[] userData;
    
    public void loginBtn(string id, string pwd)
    {
        StartCoroutine(ServerLoginUser(id,pwd));
    }
    public void MakeUser(UserData data)
    {
        StartCoroutine(MakeUserCo(data));
    }
    public void GetBewCard(CardData data)
    {
        StartCoroutine(GetBewCardCo(data));
    }
    public void StageClear(StageData data)
    {
        StartCoroutine(StageClearCo(data));
    }
    public void UpdateCard(Card cCard)
    {
        CardData data = DataConverter.CardToCardData(cCard);
        StartCoroutine(UpdateCardCo(data));
    }
    public void DeleteCard(int nCardno)
    {
        StartCoroutine(DeleteCardCo(nCardno));
    }
    public void SetUserCard()
    {
        StartCoroutine(SetUserCardCo());
    }
    public void UpdateUserData()
    {
        StartCoroutine(UpdateUserDataCo());
    }
    public void ComposeCard(ComposeSin sin)
    {
        StartCoroutine(ComposeCardCo(sin));
    }
    public void EvolutionCard(EvolutionSin sin)
    {
        StartCoroutine(EvolutionCardCo(sin));
    }
    public void UnlimitCard(UnlimitSin sin)
    {
        StartCoroutine(UnlimitCardCo(sin));
    }
    public void ReComposeRedon(ReComposeSin sin)
    {
        StartCoroutine(ReComposeRedonCo(sin));
    }
    public void ReComposeConf(ReComposeSin sin)
    {
        StartCoroutine(ReComposeConfCo(sin));
    }
    public void UpdateRaidScore(int score)
    {
        StartCoroutine(UpdateRaidScoreCo(score));
    }
    public void GetMyRaidScore(RaidSin sin)
    {
        StartCoroutine(GetMyRaidScoreCo(sin));
    }
    public void GetAllRaidScore(RaidSin sin)
    {
        StartCoroutine(GetAllRaidScoreCo(sin));
    }
    IEnumerator GetAllRaidScoreCo(RaidSin sin)
    {
        WWWForm form = new WWWForm();
        UnityWebRequest www = UnityWebRequest.Post("http://34.64.117.51:3030/GetAllRaidScore", form);
        yield return www.SendWebRequest();
        var allScoer = JsonHelper.FromJson<RaidScore>(www.downloadHandler.text);

        for (int i = 0; i < sin._vecRankData.Count; i++)
        {
            Destroy(sin._vecRankData[i]);
        }
        sin._vecRankData.Clear();
        for (int i = 0; i < allScoer.Length; i++)
        {
            GameObject temp = Instantiate(sin._objRankData, sin._tRanks);
            temp.SendMessage("Set", allScoer[i]);
            sin._vecRankData.Add(temp);
        }

        sin.OpenAllScoreSin();
        www.Dispose();
    }
    IEnumerator GetMyRaidScoreCo(RaidSin sin)
    {
        WWWForm form = new WWWForm();
        form.AddField("userno", GameManager.GM.m_cPlayer.userno);
        UnityWebRequest www = UnityWebRequest.Post("http://34.64.117.51:3030/GetMyRaidScore", form);
        yield return www.SendWebRequest();
        if (www.downloadHandler.text == "")
        {
            sin._txtMyRank.text = "";
            sin._txtMyScore.text = "";
        }
        else
        {
            RaidScore tempSco = JsonUtility.FromJson<RaidScore>(www.downloadHandler.text);
            sin._txtMyRank.text = tempSco.Ranking + "";
            sin._txtMyScore.text = tempSco.Score + "";
        }
        sin.gameObject.SetActive(true);
        www.Dispose();
    }
    IEnumerator UpdateRaidScoreCo(int score)
    {
        WWWForm form = new WWWForm(); ;
        form.AddField("userno", GameManager.GM.m_cPlayer.userno + "");
        form.AddField("score", score + "");
        UnityWebRequest www = UnityWebRequest.Post("http://34.64.117.51:3030/updateRaidScore", form); ;
        yield return www.SendWebRequest(); // 아무값이나 리턴 해야 다음으로 진행됨
        www.Dispose();
        GameManager.GM.GoLaidEnd();
    }
    IEnumerator ReComposeRedonCo(ReComposeSin sin)
    {
        WWWForm form; UnityWebRequest www;
        for (int i = 0; i < sin.m_vecRandom.Count; i++)
        {
            form = new WWWForm();
            form.AddField("userno", GameManager.GM.m_cPlayer.userno + "");
            form.AddField("d1", sin.m_vecRandom[i].m_cCard.cardno + "");

            www = UnityWebRequest.Post("http://34.64.117.51:3030/deleteCard", form);
            print("재구성 랜덤 삭제실행 : " + i);
            yield return www.SendWebRequest();
            www.Dispose();
        }
        int nTemp = Random.Range(0, 24);
        GameManager.GM.GetCard((CardType)nTemp, CardRank.S);
        yield return new WaitForSeconds(0.2f);
        sin.Close();
        sin.Open();
    }
    IEnumerator ReComposeConfCo(ReComposeSin sin)
    {
        WWWForm form; UnityWebRequest www;
        for (int i = 0; i < sin.m_vecConf.Count; i++)
        {
            form = new WWWForm();
            form.AddField("userno", GameManager.GM.m_cPlayer.userno + "");
            form.AddField("d1", sin.m_vecConf[i].m_cCard.cardno + "");

            www = UnityWebRequest.Post("http://34.64.117.51:3030/deleteCard", form);
            print("재구성 확정 삭제실행 : " + i);
            yield return www.SendWebRequest();
            www.Dispose();
        }
        GameManager.GM.GetCard(sin.m_cShowCard.m_cCard.m_eCardType, CardRank.S);
        yield return new WaitForSeconds(0.2f);
        sin.Close();
        sin.Open();
    }
    IEnumerator UnlimitCardCo(UnlimitSin sin)
    {
        int nUpLv = 0;
        for (int i = 0; i < sin.m_vecSelectCard.Count; i++)
        {
            nUpLv += sin.m_vecSelectCard[i].m_nUnlimite + 1;
        }
        if (nUpLv > 10 - (sin.m_cSelectSlot.m_cCard.m_nMaxLevel - 70)) nUpLv = 10 - (sin.m_cSelectSlot.m_cCard.m_nMaxLevel - 70);

        sin.m_cSelectSlot.m_cCard.m_nMaxLevel = sin.m_cSelectSlot.m_cCard.m_nMaxLevel + nUpLv;
        sin.m_cSelectSlot.m_cCard.m_nUnlimite += nUpLv;
        for (int i = 0; i < sin.m_vecSelectCard.Count; i++)
        {
            if (sin.m_vecSelectCard[i].m_nLevel > sin.m_cSelectSlot.m_cCard.m_nLevel)
            {
                sin.m_cSelectSlot.m_cCard.Copy(sin.m_vecSelectCard[i]);
            }
        }

        WWWForm form; UnityWebRequest www;
        for (int i = 0; i < sin.m_vecSelectCard.Count; i++)
        {
            form = new WWWForm();
            form.AddField("userno", GameManager.GM.m_cPlayer.userno + "");
            form.AddField("d1", sin.m_vecSelectCard[i].cardno + "");

            www = UnityWebRequest.Post("http://34.64.117.51:3030/deleteCard", form);
            print("초월 삭제실행 : " + i);
            yield return www.SendWebRequest();
            www.Dispose();
        }
        yield return new WaitForSeconds(0.1f);
        GameManager.GM.ShowCard("초월 성공", sin.m_cSelectSlot.m_cCard);
        sin.Close();
        sin.Open();
    }
    IEnumerator EvolutionCardCo(EvolutionSin sin)
    {
        WWWForm form; UnityWebRequest www;
        for (int i = 0; i < sin.m_vecSelectCard.Count; i++)
        {
            form = new WWWForm();
            form.AddField("userno", GameManager.GM.m_cPlayer.userno + "");
            form.AddField("d1", sin.m_vecSelectCard[i].m_cCard.cardno + "");

            www = UnityWebRequest.Post("http://34.64.117.51:3030/deleteCard", form);
            print("진화 삭제실행 : " + i);
            yield return www.SendWebRequest();
            www.Dispose();
        }

        GameManager.GM.m_cPlayer.m_cAvata.GetCard(sin.m_cResult);
        GameManager.GM.ShowCard("진화 성공", sin.m_cResult);
        yield return new WaitForSeconds(0.2f);
        sin.Close();
        sin.Open();
    }
    IEnumerator ComposeCardCo(ComposeSin sin)
    {
        WWWForm form; UnityWebRequest www;
        for (int i = 0; i < sin.m_vecMySelectCard.Count; i++)
        {
            //StartCoroutine(DeleteCardCo(sin.m_vecMySelectCard[i].m_cCard.cardno));
            form = new WWWForm();
            form.AddField("userno", GameManager.GM.m_cPlayer.userno + "");
            form.AddField("d1", sin.m_vecMySelectCard[i].m_cCard.cardno + "");

            www = UnityWebRequest.Post("http://34.64.117.51:3030/deleteCard", form);
            print("합성 삭제실행 : " + i);
            yield return www.SendWebRequest();
            www.Dispose();
        }
        CardRank eCR = sin.m_vecMySelectCard[3].m_cCard.m_eCardRank;
        int nNextRank = (int)eCR;
        nNextRank--;
        Card temp = GameManager.GM.cCM.GetCard((CardType)Random.Range(0, 24), (CardRank)nNextRank);

        GameManager.GM.GetCard(temp);

        yield return new WaitForSeconds(0.2f);
        sin.Close();
        sin.Open();
    }
    IEnumerator UpdateUserDataCo()
    {
        WWWForm form = new WWWForm();
        UserData temp = DataConverter.GetUserData();
        print(temp.m_nGold + " 저장");
        form.AddField("userno", temp.userno);
        form.AddField("gold", temp.m_nGold+"");
        form.AddField("dia", temp.m_nDiamond+"");
        form.AddField("gas", temp.m_nGas+"");
        form.AddField("first", temp._bFirst+"");

        UnityWebRequest www = UnityWebRequest.Post("http://34.64.117.51:3030/UpdateUesrData", form);
        yield return null;
        www.Dispose();
    }
    IEnumerator SetUserCardCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("userno", GameManager.GM.m_cPlayer.userno);

        UnityWebRequest www = UnityWebRequest.Post("http://34.64.117.51:3030/GetCardData", form); //  카드값 주는 주소로 변경
        //UnityWebRequest www = UnityWebRequest.Post("http://34.64.117.51:3030/GetCardData", form); //  카드값 주는 주소로 변경
        yield return www.SendWebRequest();
        print("카드목록 " +www.downloadHandler.text);

        for (int i = 0; i < GameManager.GM.m_cPlayer.m_cAvata._vecMyMember.Count; i++)
        {
            Destroy(GameManager.GM.m_cPlayer.m_cAvata._vecMyMember[i].gameObject);
        }
        for (int i = 0; i < GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard.Count; i++)
        {
            Destroy(GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard[i].gameObject);
        }

        GameManager.GM.m_cPlayer.m_cAvata._vecMyMember.Clear();
        GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard.Clear();

        var CardData = JsonHelper.FromJson<CardData>(www.downloadHandler.text);
        for (int i = 0; i < CardData.Length; i++)
        {
            if (CardData[i].m_nCost == -1)
            {
                Member temp = GameManager.GM.cMM.GetMember((MemberType)CardData[i]._eMT);
                temp.cardno = CardData[i].cardno;
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
        www.Dispose();
    }
    IEnumerator UpdateCardCo(CardData data)
    {
        WWWForm form = new WWWForm();

        form.AddField("userno", GameManager.GM.m_cPlayer.userno + "");
        form.AddField("d1", data.m_eCardType);
        form.AddField("d2", data.m_eCardRank);
        form.AddField("d3", data.m_nCost);
        form.AddField("d4", data.m_nLevel);
        form.AddField("d5", data.m_nMaxLevel);
        form.AddField("d6", data.m_nUnlimite);
        form.AddField("d7", data.m_nLevelUpGold + "");
        form.AddField("d8", data.m_fAp + "");
        form.AddField("d9", data.m_fHp + "");
        form.AddField("d10", data._eMT);
        form.AddField("cardno", data.cardno);

        UnityWebRequest www = UnityWebRequest.Post("http://34.64.117.51:3030/updateCard", form);
        yield return www.SendWebRequest();
        www.Dispose();
    }
    IEnumerator DeleteCardCo(int nCardno)
    {
        WWWForm form = new WWWForm();
        form.AddField("userno", GameManager.GM.m_cPlayer.userno + "");
        form.AddField("d1", nCardno+"");
        
        UnityWebRequest www = UnityWebRequest.Post("http://34.64.117.51:3030/deleteCard", form);
        yield return www.SendWebRequest();
        www.Dispose();
    }
    IEnumerator StageClearCo(StageData data)
    {
        WWWForm form = new WWWForm();
        form.AddField("userno", GameManager.GM.m_cPlayer.userno + "");
        form.AddField("d1", "1");
        form.AddField("d2", data.m_strStage);

        UnityWebRequest www = UnityWebRequest.Post("http://34.64.117.51:3030/stageClear", form);
        yield return null;
        www.Dispose();
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

        UnityWebRequest www = UnityWebRequest.Post("http://34.64.117.51:3030/insertCard", form);
        yield return www.SendWebRequest();
        www.Dispose();
    }
    IEnumerator ServerLoginUser(string id, string pwd)
    {
        print("로그인 코루틴 시작");
        WWWForm form = new WWWForm();
        form.AddField("ID", id);
        form.AddField("PW", pwd);
        // [한종환] [오후 10:04] http://34.64.117.51:3030
        UnityWebRequest www = UnityWebRequest.Post("http://34.64.117.51:3030/login_user", form);

        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            print(www.error);
            GameManager.GM.ShowText("네트워크 애러");
        }
        else if (www.downloadHandler.text == "101")
        {
            GameManager.GM.ShowText("존재하지 않는 ID입니다");
        }
        else if (www.downloadHandler.text == "102")
        {
            GameManager.GM.ShowText("비밀번호를 다시 확인 하세요");
        }
        else
        {
            // 플레이어 정보 불러오기 시작
            UserData tempPl = JsonUtility.FromJson<UserData>(www.downloadHandler.text);
            //yield return www.SendWebRequest();
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

            // 플레이어 정보 불러오기 종료
            GameManager.GM.m_cPlayer.AllSet(); // ok
            // 카드 작업 시작
            form.AddField("userno", GameManager.GM.m_cPlayer.userno);
            www.Dispose();
            www = UnityWebRequest.Post("http://34.64.117.51:3030/GetCardData", form); //  카드값 주는 주소로 변경
            yield return www.SendWebRequest();
            print(www.downloadHandler.text);

            GameManager.GM.m_cPlayer.m_cAvata._vecMyMember.Clear();
            GameManager.GM.m_cPlayer.m_cAvata.m_vecMyCard.Clear();

            var CardData = JsonHelper.FromJson<CardData>(www.downloadHandler.text);
            for (int i = 0; i < CardData.Length; i++)
            {
                if (CardData[i].m_nCost == -1)
                {
                    Member temp = GameManager.GM.cMM.GetMember((MemberType)CardData[i]._eMT);
                    temp.cardno = CardData[i].cardno;
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
            // 카드 작업 끝
            // 스테이지 불러오기 시작
            www.Dispose();
            www = UnityWebRequest.Post("http://34.64.117.51:3030/GetStageData", form); //  스테이지 주는 주소로 변경
            yield return www.SendWebRequest();
            var stageData = JsonHelper.FromJson<StageData>(www.downloadHandler.text);
            for (int i = 0; i < stageData.Length; i++)
            {
                StageData s = stageData[i];
                GameManager.GM.cSM.SetStage(s.m_strStage, s.m_bClear);
                print("스테이지 : " + s.m_strStage + "  클리어: "+ s.m_bClear);
            }
            // 스테이지 불러오기 종료
            StartCoroutine(AutoUpdateUserDataCo());
            GameManager.GM.GoTitle();
        }
        www.Dispose();
    }
    IEnumerator MakeUserCo(UserData data)
    {
        WWWForm form = new WWWForm();

        form.AddField("id", data.userID);
        form.AddField("pw", data.userPW);
        form.AddField("name", data.userName);
        form.AddField("gold", data.m_nGold + "");
        form.AddField("dia", data.m_nDiamond + "");
        form.AddField("gas", data.m_nGas + "");
        form.AddField("first", data._bFirst + "");

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:3030/make_user", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            GameManager.GM.ShowText("회원가입 에러");
        }
        else
        {
            GameManager.GM.ShowText("회원가입 성공");
            GameManager.GM.GoLogin();
        }
        www.Dispose();
    }
    IEnumerator AutoUpdateUserDataCo()
    {
        while (true)
        {
            print("자동저장");
            StartCoroutine(UpdateUserDataCo());
            yield return new WaitForSeconds(3f);
        }
    }
}
public class DataConverter
{
    public static CardData CardToCardData(Card cCard)
    {
        CardData temp = new CardData();
        temp.m_eCardType = (int)cCard.m_eCardType;
        temp.m_eCardRank = (int)cCard.m_eCardRank;
        temp._eMT = (int)cCard._eMT;
        temp.m_nCost = cCard.m_nCost;
        temp.m_nLevel = cCard.m_nLevel;
        temp.m_nUnlimite = cCard.m_nUnlimite;
        temp.m_nLevelUpGold = cCard.m_nLevelUpGold;
        temp.m_fAp = cCard.m_fAp;
        temp.m_nMaxLevel = cCard.m_nMaxLevel;
        temp.m_fHp = cCard.m_fHp;
        temp.cardno = cCard.cardno;
        return temp;
    }
    public static UserData GetUserData()
    {
        UserData temp = new UserData
        {
            userno = GameManager.GM.m_cPlayer.userno,
            m_nGold = GameManager.GM.m_cPlayer.m_nGold,
            m_nDiamond = GameManager.GM.m_cPlayer.m_nDiamond,
            m_nGas = GameManager.GM.m_cPlayer.m_nGas,
            _bFirst = GameManager.GM.m_cPlayer._bFirst //
        };

        return temp;
    }
}
[System.Serializable]
public class RaidScore
{
    public int Ranking;
    public string Name;
    public int Score;
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