using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_9: Pattern_Proto
{
    public override void UsePattern()
    {
        for (int i = -2; i < 3; i++)
        {
            GameObject temp = Instantiate(gameObject, transform.position + new Vector3(i * 0.9f, 0, i + 8), transform.rotation) as GameObject;
            temp.transform.Rotate(new Vector3(0, 0, 0));
        }
    }
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    float timer = 0f;
    private void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(Vector3.down * p_fSpeed * Time.deltaTime);
        if(timer >= 1.5)
        {
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * 720));
            if(timer >= 2.5)
            {
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * 720));
                timer = 0;
            }
        }
    }
}
