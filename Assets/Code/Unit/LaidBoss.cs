using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaidBoss : Boss
{
    public Text m_txtScore;
    public float m_fScore = 0;
    public override void Respon()
    {
        m_fScore = 0;
        transform.position = new Vector3(0, -0.1f, 6.5f);
    }
    public override bool Hit(float fDamage, CardElement CE)
    {
        Color tempColor = Color.white;
        float temp = fDamage;

        switch (m_eElement)
        {
            case CardElement.Fire:
                if (CE == CardElement.Water) fDamage = fDamage * 1.25f;
                if (CE == CardElement.Wind) fDamage = fDamage * 0.75f;
                break;
            case CardElement.Water:
                if (CE == CardElement.Stone) fDamage = fDamage * 1.25f;
                if (CE == CardElement.Fire) fDamage = fDamage * 0.75f;
                break;
            case CardElement.Wind:
                if (CE == CardElement.Fire) fDamage = fDamage * 1.25f;
                if (CE == CardElement.Stone) fDamage = fDamage * 0.75f;
                break;
            case CardElement.Stone:
                if (CE == CardElement.Wind) fDamage = fDamage * 1.25f;
                if (CE == CardElement.Water) fDamage = fDamage * 0.75f;
                break;
            case CardElement.Light:
                if (CE == CardElement.Dark) fDamage = fDamage * 1.25f;
                break;
            case CardElement.Dark:
                if (CE == CardElement.Light) fDamage = fDamage * 1.25f;
                break;
        }

        if (temp < fDamage)
        {
            switch (m_eElement)
            {
                case CardElement.Fire:
                    tempColor = Color.blue;
                    break;
                case CardElement.Water:
                    tempColor = new Color(0.65f, 0, 0, 0);// 哎祸
                    break;
                case CardElement.Wind:
                    tempColor = Color.red;
                    break;
                case CardElement.Stone:
                    tempColor = Color.green;
                    break;
                case CardElement.Light:
                    tempColor = Color.black;
                    break;
                case CardElement.Dark:
                    tempColor = Color.yellow;
                    break;
            }
            print("加己 农府!");

        }
        if (temp > fDamage)
        {
            tempColor = Color.gray;
            print("加己 叼农府...");
        }

        tempObj = Instantiate(m_objHitText, transform.position, Quaternion.Euler(90f, 0, 0));
        TextMesh tempTM = tempObj.GetComponent<TextMesh>();
        tempTM.color = tempColor;
        tempTM.text = "" + (int)fDamage;
        Destroy(tempObj, 0.3f);

        tempObj.transform.position += new Vector3(0, -0.1f, -3f);

        m_fScore += fDamage;

        m_txtScore.text = ((int)m_fScore).ToString();
        return true;
    }
    public override void Die()
    {
        //GameManager.GM.GoWin();
    }
}
