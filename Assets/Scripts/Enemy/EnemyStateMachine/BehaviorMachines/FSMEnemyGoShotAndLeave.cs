using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State Machine with some behavior for the enemy plane.
/// This state machine consist in a enemy start in some position, go to another, shot, go to another position and then is destoryed
/// Ussually will start outside screen, will go to some position inside the screen, shot and then go out the screen again
/// The initial position, the shot position and the final position will be set in the constructor
/// </summary>
public class FSMEnemyGoShotAndLeave : FSMEnemyBehavior
{
    /// <summary>
    /// Initial position (spanw position) for the plane
    /// </summary>
    Vector3 _initialPosition;
    /// <summary>
    /// Shot position for the plane ( position from where the plane will shot )
    /// </summary>
    Vector3 _shotPosition;
    /// <summary>
    /// Final position where the plane will be destroyed
    /// </summary>
    Vector3 _finalPosition;

    /// <summary>
    /// Constructor for the FSM setting plane affected, the initial position, shop position and final position
    /// </summary>
    /// <param name="plane">plane affected by this behavior</param>
    /// <param name="initialPosition">initial position</param>
    /// <param name="shotPosition">shot position</param>
    /// <param name="finalPosition">final position</param>
    public FSMEnemyGoShotAndLeave(EnemyEntity plane, Vector3 initialPosition, Vector3 shotPosition, Vector3 finalPosition) : base(plane)
    {
        // setting var initial position
        _initialPosition = initialPosition;
        // setting var shot position
        _shotPosition = shotPosition;
        // setting var final position
        _finalPosition = finalPosition;
    }

    /// <summary>
    /// Method call when the FSM Start. Creating the behavior for the FSM
    /// -Setting the plane to the initial position
    /// -Creating the state to go to the shot position and his transition to know when reach the position ( setting acctually by time)
    /// -Create the state shot with a always true transition ( converting the shot state in a one frame state )
    /// -Create the state to go to the final position and his transition to know when reach the position ( setting acctually by time)
    /// </summary>
    public override void Create()
    {

        // getting the plane and getting the speed for easy time calculation 
        EnemyEntity plane = ( StateMachine as FSMEnemyBehavior ).Plane;
        float planeSpeed = plane.GetComponentInChildren<EntityMovement>().Speed;
        float timeToReachPosition = 0;

        // setting the plane in the initial position
        plane.GetComponent<EntityMovement>().Position = _initialPosition;

        // calculate the time to reach the shop position
        timeToReachPosition = ( _shotPosition - _initialPosition ).magnitude / planeSpeed;
        // creating the state go to position 
        FSMState stateGoToShotPosition = new FSMStateMoveToScreenPosition(this, _shotPosition, timeToReachPosition);
        // creating the transition to inform when reach shopPosition 
        FSMTransition onReachShotPositionByTime = new FSMTransitionTime(this, timeToReachPosition);

        // creating state shot
        FSMState stateShot = new FSMStateShot(this);
        // creating one frame transition
        FSMTransition onShot = new FSMTransitionTrue(this);

        // calculating the time to reach the final positions
        timeToReachPosition = ( _finalPosition - _shotPosition ).magnitude / planeSpeed;
        // creating the state go to final position
        FSMState stateGoToFinalPositions = new FSMStateMoveToScreenPosition(this, _finalPosition, timeToReachPosition);
        // creating the transition when reach final position
        FSMTransition onReachFinalPositionByTime = new FSMTransitionTime(this, timeToReachPosition);

        //creating the states flow
        // fist state go to shot positions
        SetFirstState(stateGoToShotPosition);
        // go to shot position with transition on reach position by time
        stateGoToShotPosition.AddTransition(onReachShotPositionByTime);
        // on reach position go to state shot
        onReachShotPositionByTime.SetNextState(stateShot);

        // set transition on shot for state shot
        stateShot.AddTransition(onShot);
        // set next state as go to final position
        onShot.SetNextState(stateGoToFinalPositions);
        // set transition for state go to finish positoin as reach final position
        stateGoToFinalPositions.AddTransition(onReachFinalPositionByTime);

        base.Create();
    }

    /// <summary>
    /// when the state machine finish the plane must self destroy if not had been destroyed by another object in the game
    /// </summary>
    public override void Finish()
    {
        if (_finished)
            return;

        base.Finish();

        // only self destroy if still alive ( HP greater than 0 )
        if (Plane.HealthPoints > 0)
        {
            // destory plane with no point won
            Plane.DestroyPlane(false);
        }
    }



}
