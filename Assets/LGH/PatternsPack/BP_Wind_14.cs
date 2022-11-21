using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_14: Pattern_Proto
{
    float timer = 0;
    GameObject temp;
    public override void UsePattern()
    {
        for(int j = 0; j < 2; j++)
        {
            for (int i = -1; i < 2; i++)
            {
                switch (i)
                {
                    case -1:
                        temp = Instantiate(gameObject, transform.position + new Vector3(3 + (j * 2), 0, j*2), transform.rotation) as GameObject;
                        temp.transform.Rotate(new Vector3(0, 0, -30));
                        break;
                    case 0:
                        temp = Instantiate(gameObject, transform.position + new Vector3(j * 2, 0, 6+(j*2)), transform.rotation) as GameObject;
                        temp.transform.Rotate(new Vector3(0, 0, 0));
                        break;
                    case 1:
                        temp = Instantiate(gameObject, transform.position + new Vector3(-3 + (j * 2), 0, 3+(j*2)), transform.rotation) as GameObject;
                        temp.transform.Rotate(new Vector3(0, 0, 30));
                        break;
                }

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
    }
}
