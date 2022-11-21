using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_16 : Pattern_Proto
{
    float timer = 0;
    GameObject temp;
    public override void UsePattern()
    {
        for(int k = 0; k < 5; k++)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    temp = Instantiate(gameObject, transform.position + new Vector3(i, 0, k*2), transform.rotation) as GameObject;
                    temp.transform.Rotate(0, 0, j * 40 + i * 5 + k * 2);
                }
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
        if (timer <= 1f)
        {
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * 720));
        }

    }
}

