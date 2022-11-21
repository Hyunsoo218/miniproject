using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_4 : Pattern_Proto
{
    float timer = 0f;
    public override void UsePattern()
    {
        for(int i = -4; i < 5; i++)
        {
            GameObject temp = Instantiate(gameObject, transform.position + new Vector3(Random.Range(-2f, 2f) ,0, 0), transform.rotation) as GameObject;
            temp.transform.Rotate(new Vector3(0, 0, i*12));
        }
    }
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        transform.Translate(Vector3.down * p_fSpeed * Time.deltaTime);

        timer += Time.deltaTime;
    }
}
