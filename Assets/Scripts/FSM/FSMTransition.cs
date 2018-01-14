using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Transition for the state machine, here is where will check when a state of the State Machine is finish.
/// containt a State that is will be the next state in the FSM. 
/// 
/// This class is abstract to force the implementation of the method "CheckChangeState" in the childs.
/// this method will do the checking for know if the current state finish or not
/// </summary>
public abstract class FSMTransition
{
    /// <summary>
    ///  /// <summary>
    /// state machine owner of this state, is set in the constructor
    /// </summary>
    /// </summary>
    protected FSMachine _stateMachine;
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

    /// <summary>
    /// next state that will start when this condition is true (meaning that will finish the current state)
    /// this value can be left as null, meaning that after this transition, the FSM is over
    /// </summary>
    protected FSMState _nextState;

    /// <summary>
    /// Constructor for the transition, will be call when the state machine is being constructed
    /// </summary>
    /// <param name="stateMachine">state machine owner of this transition</param>
    public FSMTransition(FSMachine stateMachine)
    {
        _nextState = null;
        _stateMachine = stateMachine;
    }

    /// <summary>
    /// Method call each update while the state is activate.
    /// Here is where will be done the checking about if the state shuld finish or not.
    /// </summary>
    public abstract bool CheckChangeState();

    /// <summary>
    /// this method set which one will be the next state.
    /// </summary>
    /// <param name="nextState">next state to be loaded after this transition</param>
    public void SetNextState(FSMState nextState)
    {
        _nextState = nextState;
    }

    /// <summary>
    /// return the next state var
    /// </summary>
    /// <returns>the next state var</returns>
    public FSMState GetNextState()
    {
        return _nextState;
    }
}
