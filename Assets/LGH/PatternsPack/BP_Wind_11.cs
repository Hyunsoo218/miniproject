using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_11: Pattern_Proto
{
    float timer = 0;
    GameObject temp;
    public override void UsePattern()
    {
        for(int i = -2; i < 3; i++)
        {
            temp = Instantiate(gameObject, transform.position + new Vector3(0, 0, i*2.5f), transform.rotation) as GameObject;
            temp.transform.Rotate(new Vector3(0, 0, 90));
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
        if (timer <= 2f)
        {
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * 1440));
        }
    }
}
