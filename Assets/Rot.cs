using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot : MonoBehaviour
{
    [SerializeField] float _Rot = 36000f;
    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles += new Vector3(0,0,Time.deltaTime * _Rot);
    }
}
