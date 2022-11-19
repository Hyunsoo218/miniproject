using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBullit : Bullit
{
    private void Update()
    {
        transform.Translate(Vector3.up * 10f * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // 유닛 hit 시키기
            other.gameObject.GetComponent<Unit>().Hit(m_fDamage, m_eCE);
            print("관통탄 명중");
        }
    }
}
