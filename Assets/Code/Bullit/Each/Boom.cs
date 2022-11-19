using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public CardElement m_eCE;
    float m_fDamage;

    public void Set(float fDamage, CardElement mCE)
    {
        m_fDamage = fDamage;
        m_eCE = mCE;
        Destroy(gameObject, 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // À¯´Ö hit ½ÃÅ°±â
            other.gameObject.GetComponent<Unit>().Hit(m_fDamage , m_eCE);
            print("Æø¹ß ¸íÁß");
        }
    }
}
