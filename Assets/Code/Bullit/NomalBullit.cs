using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalBullit : Bullit
{
    private void Update()
    {
        transform.Translate(Vector3.up * 10f * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && m_nTargetEa >= 1)
        {
            Unit temp = other.gameObject.GetComponent<Unit>();

            if (temp.Hit(m_fDamage, m_eCE))
            {
                Destroy(gameObject);
                m_nTargetEa--;
            }
        }
    }
}
