using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : Unit
{
    // 몬스터의 정보를 가지는 클래스. Unit을 상속함
    public CardElement m_eElement;
    Unit m_cTarget;
    float m_fAttackTime = 1;
    protected GameObject tempObj;
    public string m_strName;
    public Sprite m_imgImage;

    private void Update()
    {
        if (GameManager.GM.m_eGS != GameState.Game) return;
        if (GameManager.GM.m_eGS == GameState.Tutorial) return;
        Move();

        m_fAttackTime += Time.deltaTime;

        if (m_cTarget != null)
        {
            if (m_fAttackTime >= 0.3f)
            {
                print(gameObject.name + "이 공격함");
                m_fAttackTime = 0;
                m_cTarget.Hit(m_fAp, m_eElement);
            }
        }
    }
    public override void Move()
    {
        switch (GameManager.GM.m_eGT)
        {
            case GameType.TutorialStage:
            case GameType.Defence:
                Pattern_Defens();
                break;
            case GameType.Laid:
            case GameType.Boss:
                break;
        }
    }
    void Pattern_Defens()
    {
        if (transform.position.z < -2f)
        {

        }
        else
        {
            transform.Translate(Vector3.down * m_fSpeed * Time.deltaTime);
        }
    }
    public override void Respon()
    {
        m_fHp = m_fMaxHp;
        gameObject.SetActive(true);
        switch (GameManager.GM.m_eGT)
        {
            case GameType.TutorialStage:
            case GameType.Defence:
                transform.position = new Vector3(Random.Range(-4f, 4f), -0.1f, 12f);
                break;
            case GameType.Laid:
            case GameType.Boss:
                break;
        }
    }
    public override bool Hit(float fDamage, CardElement CE)
    {
        if (m_fHp <= 0)
        {
            return false;
        }

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
        m_fHp -= fDamage;

        if (temp < fDamage)
        {
            switch (m_eElement)
            {
                case CardElement.Fire:
                    tempColor = Color.blue;
                    break;
                case CardElement.Water:
                    tempColor = new Color(0.65f, 0, 0, 0);// 갈색
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

        }
        if (temp > fDamage)
        {
            tempColor = Color.gray;
        }

        tempObj = Instantiate(m_objHitText, transform.position + new Vector3(0, -0.1f, 0), Quaternion.Euler(90f, 0, 0));
        TextMesh tempTM = tempObj.GetComponent<TextMesh>();
        tempTM.color = tempColor;
        tempTM.text = "" + (int)fDamage;
        Destroy(tempObj, 0.3f);

        if (m_fHp <= 0)
        {
            Die();
        }

        return true;
    }
    public override void Die()
    {
        m_cTarget = null;
        gameObject.SetActive(false);
        MonsterSponManager.AddSponWaitMonster(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_cTarget = other.gameObject.GetComponent<Unit>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_cTarget = null;
        }
    }
}
