using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class that controls when the player press with the mouse and then call to shot,
/// extend from game component because if game paused, must not shot
/// </summary>
public class PlayerMouseShot : GameComponent
{
    /// <summary>
    /// entity that will shot
    /// </summary>
    EntityFire _entityFire;

    /// <summary>
    /// override method call at start of the game
    /// setting the entity fire var
    /// </summary>
    protected override void CustomAwake()
    {
        base.CustomAwake();

        _entityFire = GetComponentInParent<EntityFire>();
    }

    /// <summary>
    /// method call every frame ( is game is not paused )
    /// getting the mouse click, if clicked, callig to the entity to fire
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);

        // getting the button 0 ( left button ) from the mouse
        bool firePressed = Input.GetMouseButtonDown(0);
        if (firePressed)
        {
            // calling to fire
            _entityFire.Fire();
        }
    }
}
