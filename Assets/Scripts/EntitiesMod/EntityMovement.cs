using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class to control the movement of the entity, setting the speed and the positions,
/// extend from gamecomponent because must only do update when required
/// </summary>
public class EntityMovement : GameComponent
{
    /// <summary>
    /// movement speed of the entity
    /// </summary>
    [SerializeField]
    public float _speed;
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }

    /// <summary>
    /// var used for modify the movement, instead set position, the movements are accumulate here and then in the lateUpdate are apply
    /// </summary>
    [SerializeField]
    private Vector3 _movement;
    public Vector3 Momentum
    {
        get
        {
            return _movement;
        }
        set
        {
            _movement = value;
        }
    }

    /// <summary>
    /// var to access to the entity position
    /// </summary>
    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }

    /// <summary>
    /// override method to do the movement, after change the momentum, here is where all the movement are apply
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void CustomLateUpdate(float deltaTime)
    {
        base.CustomLateUpdate(deltaTime);

        // change the current positions
        Position += Momentum;
        // reset the momentum. 
        Momentum = Vector3.zero;
    }
}
