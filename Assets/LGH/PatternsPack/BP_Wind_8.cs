using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_8 : Pattern_Proto
{
    public override void UsePattern()
    {
            GameObject temp = Instantiate(gameObject, transform.position+new Vector3(4, 0, 0), transform.rotation) as GameObject;
            temp.transform.Rotate(new Vector3(0, 0, -30));
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
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * -360));
            if(timer >= 2.5)
            {
                timer = 0;
            }
        }
    }
}
