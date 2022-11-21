using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_15 : Pattern_Proto
{
    float timer = 0;
    GameObject temp;
    public override void UsePattern()
    {
        for(int i = 0; i < 5; i++)
        {
            temp = Instantiate(gameObject, transform.position + new Vector3(i, 0, i), transform.rotation) as GameObject;
            temp.transform.localScale = new Vector3(1 * (i + 1), 1 * (i + 1), 1 * (i + 1));
            temp.transform.Rotate(0, 0, -6 * i);
        }
    }
    private void Start()
    {
        Destroy(gameObject, 6f);
    }
    private void Update()
    {
        transform.Translate(Vector3.down * p_fSpeed * (Time.deltaTime*2));
        
        
    }
}

