using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActManager : MonoBehaviour
{
    [SerializeField] List<Act> _vecAtion;

    public Act Act
    {
        get => default;
        set
        {
        }
    }

    public Act GetAct(ActionType eAT)
    {
        return _vecAtion[(int)eAT];
    }
}
