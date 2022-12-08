using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullit : Bullit
{
    public GameObject m_objBoom;

    public Boom Boom
    {
        get => default;
        set
        {
        }
    }

    public Boom Boom1
    {
        get => default;
        set
        {
        }
    }

    public Boom Boom2
    {
        get => default;
        set
        {
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.up * 10f * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject temp = Instantiate(m_objBoom, transform.position, Quaternion.Euler(90f, 0, 0));
            temp.GetComponent<Boom>().Set(m_fDamage, m_eCE);
            print("¹üÀ§Åº ¸íÁß");
            Destroy(gameObject);
        }
    }
}
