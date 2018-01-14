using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class to detect the keyboard input and set the player plane movement changing the momentum from entity movement
/// </summary>
public class PlayerKeyboardMovement : GameComponent
{
    /// <summary>
    /// entity that will be moved
    /// </summary>
    EntityMovement _entityMovement;

    /// <summary>
    /// movement that should be done with the inputs
    /// </summary>
    private Vector3 _keyboardMovement;

    /// <summary>
    /// method call when create the class. Getting the entity movement and initializing the keyboardMovement var
    /// </summary>
    protected override void CustomAwake()
    {
        base.CustomAwake();

        _entityMovement = GetComponentInParent<EntityMovement>();
        _keyboardMovement = new Vector3();
    }

    /// <summary>
    /// method call every frame ( is game not paused )
    /// </summary>
    /// <param name="deltaTime">time since last update</param>
    public override void CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);

        // getting the raw value from the axis X
        float horizontal = Input.GetAxisRaw("Horizontal");
        // getting the raw value from the axis Y
        float vertical = Input.GetAxisRaw("Vertical");
        // setting the values for the movement
        _keyboardMovement.Set(horizontal, vertical, 0);
        // adding the plane speed
        _keyboardMovement *= _entityMovement.Speed * deltaTime;
        // adding the calculate movement to ship the momentum
        _entityMovement.Momentum += _keyboardMovement;
    }

}
