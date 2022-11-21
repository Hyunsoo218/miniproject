using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_7 : Pattern_Proto
{
    public override void UsePattern()
    {
        int j = 1;
        for (int i = -3; i < 4; i++)
        {
            GameObject temp = Instantiate(gameObject, transform.position+new Vector3(-i*0.85f, 0, j*0.85f), transform.rotation) as GameObject;
            temp.transform.Rotate(new Vector3(0, 0, 0));
            j++;
        }
    }
    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.Translate(Vector3.down * p_fSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, -Time.deltaTime*25));
    }
}
