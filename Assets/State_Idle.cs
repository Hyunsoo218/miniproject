using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NeglectSin.MoveBackGround();
    }
}