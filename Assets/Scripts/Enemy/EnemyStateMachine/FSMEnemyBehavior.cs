using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State machine that extends from FSMachine adding on the constructor the plane affected (EnemyEntity) by this FSM
/// </summary>
public class FSMEnemyBehavior : FSMachine
{
    /// <summary>
    /// Enemy Plane that apply this State Machine
    /// </summary>
    public EnemyEntity Plane { get; private set; }

    /// <summary>
    /// Constructor assign the enemy plane
    /// </summary>
    /// <param name="plane">plane that apply the FSM behavior</param>
    public FSMEnemyBehavior(EnemyEntity plane)
    {
        Plane = plane;
    }

}
