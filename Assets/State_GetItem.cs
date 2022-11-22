using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_GetItem : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NeglectSin.StopBackGround();
    }
}
