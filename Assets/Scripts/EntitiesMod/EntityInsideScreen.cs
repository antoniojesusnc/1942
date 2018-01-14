using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Auxiliar component that not allow the player entity go out the screen.
/// If because the new position for the player will be outside, this class set the position to the border.
/// </summary>
public class EntityInsideScreen : GameComponent
{
    /// <summary>
    /// graphic for the plane, this will use for know the plane size
    /// </summary>
    private EntityGraphic _planeGraphic;
    /// <summary>
    /// auxiliar var for know the world position for the bot left screen position
    /// </summary>
    Vector3 _botLeftPos;
    /// <summary>
    /// auxiliar var for know the world position for the top righ screen position
    /// </summary>
    Vector3 _topRightPos;

    /// <summary>
    /// method call when the class is created, 
    /// -setting the entity graphic var
    /// -setting the bot left screen world position
    /// -setting the top right screen world position
    /// </summary>
    protected override void CustomAwake()
    {
        base.CustomAwake();
        _planeGraphic = GetComponentInChildren<EntityGraphic>();

        _botLeftPos = Camera.main.ViewportToWorldPoint(Vector3.zero);
        _topRightPos = Camera.main.ViewportToWorldPoint(Vector3.one);
    }

    /// <summary>
    /// method call every frame after the updates ( if game not paused )
    /// here the method adjust to screen is called
    /// </summary>
    /// <param name="deltaTime">time since last update</param>
    public override void CustomLateUpdate(float deltaTime)
    {
        base.CustomLateUpdate(deltaTime);

        adjustToScreen();
    }

    /// <summary>
    /// class that checks the plane position and set inside the screen if outside.
    /// Checking side separatly and setting to the border if outside screen
    /// </summary>
    private void adjustToScreen()
    {

        Vector3 planePosition = transform.position;

        // checking if out for left side
        if (planePosition.x - _planeGraphic.Size.width * 0.5f < _botLeftPos.x)
        {
            planePosition.Set(_botLeftPos.x + _planeGraphic.Size.width * 0.5f, planePosition.y, planePosition.z);
        }

        // checking if out for right side
        if (planePosition.x + _planeGraphic.Size.width * 0.5f > _topRightPos.x)
        {
            planePosition.Set(_topRightPos.x - _planeGraphic.Size.width * 0.5f, planePosition.y, planePosition.z);
        }

        // checking if out by top side
        if (planePosition.y + _planeGraphic.Size.height * 0.5f > _topRightPos.y)
        {
            planePosition.Set(planePosition.x, _topRightPos.y - _planeGraphic.Size.height * 0.5f, planePosition.z);
        }

        // checking if out by top side
        if (planePosition.y - _planeGraphic.Size.height * 0.5f < _botLeftPos.y)
        {
            planePosition.Set(planePosition.x, _botLeftPos.y + _planeGraphic.Size.height * 0.5f, planePosition.z);
        }

        // setting the position clamped to the screen positions
        transform.position = planePosition;
    }

}
