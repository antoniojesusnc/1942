using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main class for the Finite State Machine, creating the machine (states and transitions), controlling the states and checking the transitions
/// for allow the flow.
/// The states have a list of transition, the transition knows which one is the next state. If next state is empty, meaning finish FSM
/// </summary>
public class FSMachine
{

    /// <summary>
    /// active state in the state machine
    /// </summary>
    private FSMState _currentState;

    /// <summary>
    /// Instance of the object to avoid the call this
    /// </summary>
    protected FSMachine StateMachine
    {
        get
        {
            return this;
        }
    }

    /// <summary>
    /// var that saves if the state machine is finish or not
    /// </summary>
    protected bool _finished;

    /// <summary>
    /// aux var that controls with transition for the current state is being processed
    /// </summary>
    private int _transitionIndex;

    /// <summary>
    /// Method call when the State Machine Start, NOT when is created.
    /// This allow to create a multiple states machines without use it until required
    /// </summary>
    public virtual void Create()
    {
        _finished = false;

        if (_currentState != null)
            _currentState.StateInit();
    }

    /// <summary>
    /// method call every Update by the state machine manager
    /// -check if state machine is finish, if true, break the update
    /// -if not current state enable, meaning state machine finished, so calling finish State Machine
    /// -call update for the current State
    /// -check all the transition for the current state looking for a finish signal. If some transition finish, call next State
    /// </summary>
    /// <param name="time">time since last update</param>
    public virtual void Update(float time)
    {
        // checking if state machine is finish
        if (IsFinish())
            return;

        // if not current state, meaning FSM finish
        if (_currentState == null)
        {
            Finish();
            return;
        }

        // calling update for the current state
        _currentState.StateUpdate(time);

        // checking all transition ( in order ) for the current state, looking for a change signal ( a true return from any transition )
        // if true, go to next state
        for (_transitionIndex = 0; _transitionIndex < _currentState.GetTransitions().Count; ++_transitionIndex)
        {
            if (_currentState.GetTransitions()[_transitionIndex].CheckChangeState())
            {
                NextState();
                return;
            }
        }
    }

    /// <summary>
    /// method call when the FSM need to change to the next state, finishing the current state and starting the next one ( if exist, if not, finish the FSM)
    /// Because the transition index is a class var, I know from which transition for the current state process and get the next state
    /// </summary>
    public virtual void NextState()
    {
        // call finish to the current state
        _currentState.StateFinish();
        if (IsFinish())
            return;

        // getting the state from the transition and setting as current state
        _currentState = _currentState.GetTransitions()[_transitionIndex].GetNextState();
        // after set, call init for the new current state
        if (_currentState != null)
        {
            _currentState.StateInit();
        }
        // if after set, the state is null is because there are no next state so the FSM is finish
        else
        {
            Finish();

        }
    }

    /// <summary>
    /// method call when the FSM must to finish
    /// </summary>
    public virtual void Finish()
    {
        if (_finished)
            return;

        // setting the var as finish
        _finished = true;

        // if still current state, is because or the state machine is endless or because finish before finish the FSM flow.
        // if still current state, the current state must to be finished
        if (_currentState != null)
        {
            _currentState.StateFinish();
        }
        _currentState = null;
    }

    /// <summary>
    /// method that sets the first state machine, set the state as current one at start the state machine
    /// </summary>
    /// <param name="currentState">state to be setted as current</param>
    public void SetFirstState(FSMState currentState)
    {
        _currentState = currentState;

    }

    /// <summary>
    /// return is the state machine is finished, knowing because the var finished
    /// </summary>
    /// <returns></returns>
    public bool IsFinish()
    {
        return _finished;
    }
}
