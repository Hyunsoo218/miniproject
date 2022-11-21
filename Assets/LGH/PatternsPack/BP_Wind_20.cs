using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_20 : Pattern_Proto
{
    float timer = 0;
    GameObject temp;
    public override void UsePattern()
    {
        for(int j = 0; j < 4; j++)
        {
            for (int i = -12; i < 13; i++)
            {
                temp = Instantiate(gameObject, transform.position + new Vector3(0, 0, j*2), transform.rotation) as GameObject;
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
        timer += Time.deltaTime;
        if(timer > 0.5f && timer < 1f)
        {
            transform.Rotate(new Vector3(0, 0, 180 * Random.Range(-10f, 10f)));
        }
    }
}

