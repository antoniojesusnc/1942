using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class to allow the plane movement with the mouse movement
/// change the momentum for some entity movement class comparing the mouse movement between frames
/// extend from game component because must not detect the mouse if the game is paused
/// </summary>
public class PlayerMouseMovement : GameComponent
{
    /// <summary>
    /// auxiliar var for the current mouse world position
    /// </summary>
    Vector3 _currentMousePosition;
    /// <summary>
    /// auxiliar var with the mouse world position in the last frame
    /// </summary>
    Vector3 _lastMousePosition;
    /// <summary>
    /// auxiliar var with the delta mouse world position ( change between frames)
    /// </summary>
    Vector3 _deltaMousePosition;

    EntityMovement _entityMovement;

    /// <summary>
    /// override method call when the class is created.
    /// setting the entity movement var
    /// setting the initial mouse position in the _lastMousePosition var
    /// </summary>
    protected override void CustomAwake()
    {
        base.CustomAwake();

        _entityMovement = GetComponentInParent<EntityMovement>();

        _lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    /// <summary>
    /// method call every frame ( is game is not paused )
    /// getting the mouse world position based on mouse screen position, then calculate the delta posiotion and
    /// modify the momentum is delta different to 0. Then setting the last position as the current
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);

        // getting mouse position 
        _currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // getting delta mouse position
        _deltaMousePosition = _currentMousePosition - _lastMousePosition;

        // I prefer to compare x and Y separatly than compare with Vector3.zero, because I dont want to create a vector each Update
        // Also i can set even if is 0, but i prefer to avoid one call per update
        if (_deltaMousePosition.x != 0 || _deltaMousePosition.y != 0)
        {
            // adding the delta position to the momentum
            _entityMovement.Momentum += _deltaMousePosition;
        }

        // updating the last mouse position with the current one
        _lastMousePosition = _currentMousePosition;
    }
}
