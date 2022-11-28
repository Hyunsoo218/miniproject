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
        StartCoroutine(ServerLoginUser(id,pwd));
    }
    public void registerBtn()
    {
        StartCoroutine(ServerMakeUser());
    }
    public void goldSend(long gold)
    {
        StartCoroutine(GoldSend(gold));
    }
    public void diaSend(long dia)
    {
        StartCoroutine(DiaSend(dia));
    }
    IEnumerator ServerLoginUser(string id, string pwd)
    {
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
            print(www.downloadHandler.text);
            string data = www.downloadHandler.text.Replace("{", "");
            data = data.Replace("}", "");
            data = data.Replace("\"", "");

            string[] val = data.Split(",");
            string[] val2;

            for (int i = 0; i < val.Length; i++)
            {
                val2 = val[i].Split(":");
                //Myinfotext[i].text = Myinfotext[i].text + val2[1];
                userData[i] = val2[1];
                print(val2[1]);
            }
            //panel[0].gameObject.SetActive(false);
            //panel[1].gameObject.SetActive(true);
            GameManager.GM.GoTitle();
            www.Dispose();
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
