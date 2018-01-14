using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State that move the plane from current position to the final position in a certain time.
/// This is using a Lerp, so the plane cannot be affeted by external forces
/// </summary>
public class FSMStateMoveToScreenPosition : FSMState
{
    /// <summary>
    /// initial plane position
    /// </summary>
    private Vector3 _initialPosition;
    /// <summary>
    /// final position for the plane
    /// </summary>
    private Vector3 _finalPosition;
    /// <summary>
    /// movement duration
    /// </summary>
    private float _duration;
    /// <summary>
    /// auxiliar accumulative time var
    /// </summary>
    private float _timeStamp;

    /// <summary>
    /// auxiliar var for know the last plane position
    /// </summary>
    private Vector3 _lastPosition;
    /// <summary>
    /// auxiliar var with the new plane position each frame
    /// </summary>
    private Vector3 _currentPosition;

    /// <summary>
    /// entity movement of the plane
    /// </summary>
    private EntityMovement _entityMovement;

    /// <summary>
    /// constuctor for the state, receiving by parameter the final position and the movement duration.
    /// setting this var to the class vars
    /// </summary>
    /// <param name="stateMachine">owner of this state</param>
    /// <param name="finalPosition">final plane position</param>
    /// <param name="duration">movement duration</param>
    public FSMStateMoveToScreenPosition(FSMachine stateMachine, Vector3 finalPosition, float duration) : base(stateMachine)
    {
        _finalPosition = finalPosition;
        _duration = duration;
    }

    /// <summary>
    /// method call when the state start, setting some initial value for the movement calculation
    /// </summary>
    public override void StateInit()
    {
        // getting the entity movement
        _entityMovement = ( StateMachine as FSMEnemyBehavior ).Plane.GetComponentInChildren<EntityMovement>();
        // initial position for the plane
        _initialPosition = _entityMovement.transform.position;
        _lastPosition = _initialPosition;
    }

    /// <summary>
    /// method call every frame while the state is active.
    /// make the calculation with one lerp about where is the plane and set this position to the plane
    /// </summary>
    /// <param name="deltaTime">time since last frame</param>
    public override void StateUpdate(float deltaTime)
    {
        // incrementing the acum time var
        _timeStamp += deltaTime;
        // calculate where the plane should be located
        _currentPosition = Vector3.Lerp(_initialPosition, _finalPosition, _timeStamp / _duration);
        // thans to the last position, the momentum is know
        _entityMovement.Momentum += _currentPosition - _lastPosition;
        // updating the last position
        _lastPosition = _currentPosition;
    }

    public override void StateFinish()
    {

    }

}
