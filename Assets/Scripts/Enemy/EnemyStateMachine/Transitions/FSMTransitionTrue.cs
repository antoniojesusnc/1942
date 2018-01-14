using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FSM transition that always is true, so the state with this transition will call the start and finish directly
/// </summary>
public class FSMTransitionTrue : FSMTransition
{
    public FSMTransitionTrue(FSMachine stateMachine) : base(stateMachine)
    {
    }

    /// <summary>
    /// override method that checks if need to finish the state,
    /// This is always true because need to finish directly
    /// </summary>
    /// <returns></returns>
    public override bool CheckChangeState()
    {
        return true;
    }
}
