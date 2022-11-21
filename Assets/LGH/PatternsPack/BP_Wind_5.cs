using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_5 : Pattern_Proto
{
    float timer = 0f;
    public override void UsePattern()
    {
        for(int i = -3; i < 4; i++)
        {
            GameObject temp = Instantiate(gameObject, transform.position+new Vector3((i*1.2f), 0, (i*2f)), transform.rotation) as GameObject;
            temp.transform.Rotate(new Vector3(0, 0, 0));
        }
    }
    private void Start()
    {
        Destroy(gameObject, 20f);
    }
    private void Update()
    {
        transform.Translate(Vector3.down * p_fSpeed * Time.deltaTime);

        timer += Time.deltaTime;
    }
}
