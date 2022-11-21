using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_10: Pattern_Proto
{
    float timer = 0;
    GameObject temp;
    public override void UsePattern()
    {
        for(int i = -5; i < 6; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                temp = Instantiate(gameObject, transform.position + new Vector3(-j, 0, j), transform.rotation) as GameObject;
                temp.transform.Rotate(new Vector3(0, 0, (i * 15)));
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
        if(timer <= 2f)
        {
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * 1440));
        }

    }
}
