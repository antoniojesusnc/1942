using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FSM Transition that will be true when spend a certain time. Use the level time to know if the time passed
/// </summary>
public class FSMTransitionTime : FSMTransition
{
    /// <summary>
    /// time that have to spend to become the transition true, is setted in the constructor
    /// </summary>
    private float _time;
    /// <summary>
    /// var to control when the transition must finish. Is setted as current time plus the time at start the checkings
    /// </summary>
    private float _completionTime;

    /// <summary>
    /// contrusctor for the transition, the time will be setted here
    /// </summary>
    /// <param name="stateMachine"></param>
    /// <param name="time"></param>
    public FSMTransitionTime(FSMachine stateMachine, float time) : base(stateMachine)
    {
        _time = time;
    }

    /// <summary>
    /// overrite method to check when the state should finish
    /// If start _completionTime == 0, then calculate when should finish ( current level time + time )
    /// In each check compare the level time with the completino time calculated
    /// </summary>
    /// <returns></returns>
    public override bool CheckChangeState()
    {
        // if the completion time is not setted yet, make the calculation and setted
        if (_completionTime == 0)
        {
            _completionTime = GameManager.Instance.LevelManager.LevelTime + _time;
        }

        // checking if the level time is greater that the completino time
        return GameManager.Instance.LevelManager.LevelTime >= _completionTime;
    }
}
