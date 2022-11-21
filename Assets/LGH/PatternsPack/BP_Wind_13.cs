using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_13: Pattern_Proto
{
    float timer = 0;
    GameObject temp;
    public override void UsePattern()
    {
        for (int i = -12; i < 13; i++)
        {
            temp = Instantiate(gameObject, transform.position + new Vector3(i * 0.1f, 0, i*0.1f), transform.rotation) as GameObject;
            temp.transform.Rotate(new Vector3(0, 0, i*15));
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
