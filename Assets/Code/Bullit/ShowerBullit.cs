using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerBullit : Bullit
{
    public GameObject m_objEach;
    public int m_nIntEa;
    int nEa = 0;
    private void Start()
    {
        //Set(1f);
    }
    private void Update()
    {
        if (nEa < 10)
        {
            GameObject temp = Instantiate(m_objEach, transform.position, transform.rotation);
            temp.transform.eulerAngles = new Vector3(90f, Random.Range(-20f, 20f), 0);
            temp.GetComponent<Bullit>().Set(m_fDamage);

            nEa++;
        }
    }
    public override void Set(float fDamage)
    {
        base.Set(fDamage);
        transform.parent = GameManager.GM.m_cPlayer.m_cAvata.transform;
        //for (int i = 0; i < m_nIntEa; i++)
        //{
        //    GameObject temp = Instantiate(m_objEach, transform.position, transform.rotation);
        //    temp.transform.eulerAngles = new Vector3(90f, Random.Range(-20f, 20f), 0);
        //    temp.GetComponent<Bullit>().Set(fDamage);
        //}
    }
}
