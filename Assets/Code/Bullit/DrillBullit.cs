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
            // ���� hit ��Ű��
            other.gameObject.GetComponent<Unit>().Hit(m_fDamage, m_eCE);
        }
    }
}
