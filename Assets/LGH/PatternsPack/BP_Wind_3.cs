using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_3 : Pattern_Proto
{
    float timer = 0f;
    public override void UsePattern()
    {
        for(int i = 0; i < 15; i++)
        {
            GameObject temp = Instantiate(gameObject, transform.position, transform.rotation) as GameObject;
            temp.transform.Rotate(new Vector3(0, 0, Random.Range(-90f, 90f)));
        }
    }
    private void Update()
    {
        transform.Translate(Vector3.down * p_fSpeed * Time.deltaTime);

        timer += Time.deltaTime;

        if(timer > 5)
        {
            Destroy(gameObject);
        }
    }
}
