using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State that shot once at start and do nothing else
/// </summary>
public class FSMStateShot : FSMState
{
    /// <summary>
    /// empty constructor
    /// </summary>
    /// <param name="stateMachine">owner of this state</param>
    public FSMStateShot(FSMachine stateMachine) : base(stateMachine)
    {
    }

    /// <summary>
    /// override method called whe init the sate
    /// </summary>
    public override void StateInit()
    {
        // get the component entity fire and call method fire
        ( StateMachine as FSMEnemyBehavior ).Plane.GetComponent<EntityFire>().Fire();
    }
}
