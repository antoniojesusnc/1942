using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State for the state machine, here is where will happen the main actions in the State Machine.
/// containt a list of all the possibles transitions that can finish this state. The list is check by order, 
/// so if is happening that finish two conditions at same time, will have preference the fist one in the list ( First In, First Checked )
/// 
/// This class is abstract to force the implementation of the method "StateInit" in the childs.
/// The flow is: Init, Update, Finish
/// </summary>
public abstract class FSMState
{
    /// <summary>
    /// state machine owner of this state, is set in the constructor
    /// </summary>
    public FSMachine _stateMachine;
    public FSMachine StateMachine
    {
        get
        {
            return _stateMachine;
        }
        private set
        {
            _stateMachine = value;
        }
    }

    List<FSMTransition> _transitions;

    /// <summary>
    /// Constructor for the state, will be call when the state machine is being constructed
    /// </summary>
    /// <param name="stateMachine">state machine owner of this state</param>
    public FSMState(FSMachine stateMachine)
    {
        // init the transition list
        _transitions = new List<FSMTransition>();

        // state machine owner of this state
        StateMachine = stateMachine;
    }

    /// <summary>
    /// Method call when the state start and is the current state actived.
    /// Here is where will happen the majority of the actions
    /// </summary>
    public abstract void StateInit();

    /// <summary>
    /// Method call every frame while the state is active, allowing to have control over time for some actions
    /// </summary>
    /// <param name="time">time since last update</param>
    public virtual void StateUpdate(float time) { }

    /// <summary>
    /// Method call when the State Finish, ussually used to reset stuff, undo it or set last properties
    /// </summary>
    public virtual void StateFinish() { }

    /// <summary>
    /// Method to add one transition to the Current State
    /// </summary>
    /// <param name="transitions">transition to be added</param>
    public void AddTransition(FSMTransition transitions)
    {
        if (_transitions == null) _transitions = new List<FSMTransition>();
        _transitions.Add(transitions);
    }

    /// <summary>
    /// Method to get all the transiton for this state
    /// </summary>
    /// <returns>return all transitions</returns>
    public List<FSMTransition> GetTransitions()
    {
        return _transitions;
    }
}
