using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_17 : Pattern_Proto
{
    float timer = 0;
    GameObject temp;
    public override void UsePattern()
    {
        for(int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 31; j++)
            {
                temp = Instantiate(gameObject, transform.position + new Vector3(i * j, 0, j * 0.1f + i*4), transform.rotation) as GameObject;
                temp.transform.localScale = new Vector3(1 * (i + 1), 1 * (i + 1), 1 * (i + 1));
                temp.transform.Rotate(0, 0, 6 * j);
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
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * 1440));
        }

    }
}

