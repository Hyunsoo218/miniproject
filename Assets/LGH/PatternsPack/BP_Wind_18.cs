using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Wind_18 : Pattern_Proto
{
    GameObject temp;
    public override void UsePattern()
    {
        for(int k = 0; k < 3; k++)
        {
            for (int j = 0; j < 2; j++)
            {
                switch (j)
                {
                    case 0:
                        for (int i = 0; i < 3; i++)
                        {
                            temp = Instantiate(gameObject, transform.position + new Vector3(-3, 0, k * 3), transform.rotation) as GameObject;
                            temp.transform.Rotate(0, 0, 10 * i);
                        }
                        break;
                    case 1:
                        for (int i = 0; i < 3; i++)
                        {
                            temp = Instantiate(gameObject, transform.position + new Vector3(3, 0, k * 3), transform.rotation) as GameObject;
                            temp.transform.Rotate(0, 0, -10 * i);
                        }
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

