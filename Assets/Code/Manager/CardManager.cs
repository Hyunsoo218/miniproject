using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<GameObject> m_vecCard_S = new List<GameObject>();
    public List<GameObject> m_vecCard_A = new List<GameObject>();
    public List<GameObject> m_vecCard_B = new List<GameObject>();
    public List<GameObject> m_vecCard_C = new List<GameObject>();
    public List<GameObject> m_vecCard_D = new List<GameObject>();

    public Card GetCard(CardType eCT, CardRank eCR)
    {
        switch (eCR)
        {
            case CardRank.S: return Instantiate(m_vecCard_S[(int)eCT]).GetComponent<Card>();
            case CardRank.A: return Instantiate(m_vecCard_A[(int)eCT]).GetComponent<Card>();
            case CardRank.B: return Instantiate(m_vecCard_B[(int)eCT]).GetComponent<Card>();
            case CardRank.C: return Instantiate(m_vecCard_C[(int)eCT]).GetComponent<Card>();
            case CardRank.D: return Instantiate(m_vecCard_D[(int)eCT]).GetComponent<Card>();
        }
        return null;
    }
    public void GetAllCard()
    {
        for (int i = 0; i < m_vecCard_S.Count; i++)  GameManager.GM.m_cPlayer.m_cAvata.GetCard(Instantiate(m_vecCard_S[i]).GetComponent<Card>()); 
        for (int i = 0; i < m_vecCard_A.Count; i++)  GameManager.GM.m_cPlayer.m_cAvata.GetCard(Instantiate(m_vecCard_A[i]).GetComponent<Card>()); 
        for (int i = 0; i < m_vecCard_B.Count; i++)  GameManager.GM.m_cPlayer.m_cAvata.GetCard(Instantiate(m_vecCard_B[i]).GetComponent<Card>()); 
        for (int i = 0; i < m_vecCard_C.Count; i++)  GameManager.GM.m_cPlayer.m_cAvata.GetCard(Instantiate(m_vecCard_C[i]).GetComponent<Card>()); 
        for (int i = 0; i < m_vecCard_D.Count; i++)  GameManager.GM.m_cPlayer.m_cAvata.GetCard(Instantiate(m_vecCard_D[i]).GetComponent<Card>()); 
    }
    public Card GetRendom_1()
    {
        int nRank = Random.Range(0, 999999) % 100;
        int nType = Random.Range(0, 24);
        if (nRank <= 2)         return GetCard((CardType)nType, CardRank.S); 
        else if (nRank <= 8)    return GetCard((CardType)nType, CardRank.A); 
        else if (nRank <= 20)   return GetCard((CardType)nType, CardRank.B); 
        else if (nRank <= 50)   return GetCard((CardType)nType, CardRank.C); 
        else                    return GetCard((CardType)nType, CardRank.D); 
    }
    public List<Card> GetRendom_10()
    {
        List<Card> temp = new List<Card>();
        for (int i = 0; i < 10; i++) 
            temp.Add(GetRendom_1()); 
        return temp;
    }
}
public enum CardRank
{
    S, A, B, C, D
}
public enum CardType
{
    FireBall, WaterBall, WindShot, StoneBullet, LightBall, DarkBall, // 노말
    FireArrow, WaterArrow, WindArrow, StoneArrow, LightArrow, DarkArrow, // 관통
    FireShower, WaterShower, WindShower, StoneShower, LightShower, DarkShower, // 다단히트
    FireBoom, WaterBoom, WindBoom, StoneBoom, LightBoom, DarkBoom // 범위
}
