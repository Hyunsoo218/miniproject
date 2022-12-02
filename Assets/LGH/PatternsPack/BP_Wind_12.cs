using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_12: Pattern_Proto
{
    GameObject temp;
    public override void UsePattern()
    {
        temp = Instantiate(gameObject, transform.position + new Vector3(-4, 0, 0), transform.rotation) as GameObject;
        temp.transform.Rotate(new Vector3(0, 0, 30));
        temp = Instantiate(gameObject, transform.position + new Vector3(4, 0, 0), transform.rotation) as GameObject;
        temp.transform.Rotate(new Vector3(0, 0, -30));

    }
    private void Start()
    {
        Destroy(gameObject, 6f);
    }
    private void Update()
    {
        transform.Translate(Vector3.down * p_fSpeed * Time.deltaTime);
    }
}
