using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class that has the reference of the enemy state machine
/// 
/// The idea is to have the posibility of change the state machine under certain cirscuntances
/// </summary>
public class EnemyBehavior : MonoBehaviour
{
    /// <summary>
    /// current FSM working in the enemy
    /// </summary>
    public FSMachine Behavior { get; set; }

    /// <summary>
    /// method call when destory the enemy, if the enemy object is destory, the state machine must be finished. 
    /// This method will be call as well when the FSM finish, for this reason there are a checking and only 
    /// will be finished the state machine is was not finish already
    /// </summary>
    private void OnDestroy()
    {
        // chekching if FSM is set and not finished, if true call finish to the FSM
        if (Behavior != null && !Behavior.IsFinish())
            Behavior.Finish();
    }
}
