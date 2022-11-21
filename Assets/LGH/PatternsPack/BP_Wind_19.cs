using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_19 : Pattern_Proto
{
    float timer = 0;
    GameObject temp;
    public override void UsePattern()
    {
        for(int j = -2; j < 3; j++)
        {
            for (int i = -9; i < 10; i++)
            {
                temp = Instantiate(gameObject, transform.position + new Vector3(j*5, 0, (i * 2) + 12), transform.rotation) as GameObject;
                temp.transform.Rotate(0, 0, i * 10);
            }
        }
    }
    private void Start()
    {
        Destroy(gameObject, 6f);
    }
    private void Update()
    {
        transform.Translate(Vector3.down * p_fSpeed * Time.deltaTime);
    }
}

