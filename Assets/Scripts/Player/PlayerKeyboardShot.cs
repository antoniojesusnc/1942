using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// component to allow the game shot by keyboard. Extend from Game component for stop checking when the game is paused
/// </summary>
public class PlayerKeyboardShot : GameComponent
{
    /// <summary>
    /// entity that will fire
    /// </summary>
    EntityFire _entityFire;

    /// <summary>
    /// awake call at start, just taking the entity that will fire
    /// </summary>
    protected override void CustomAwake()
    {
        base.CustomAwake();

        _entityFire = GetComponentInParent<EntityFire>();
    }

    /// <summary>
    /// Update method called each frame ( when the game is not in pause )
    /// Checking if input button click and calling fire is pressed
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);

        // get if input down
        bool firePressed = Input.GetButtonDown("Fire1");

        if (firePressed)
        {
            // calling fire
            _entityFire.Fire();
        }
    }
}
