using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// method that control the bullet movement modifiy the entity movement component.
/// The stantard bullet movement is continuous movement to one direction
/// </summary>
public class BulletMovement : GameComponent
{
    /// <summary>
    /// bullet movement direction
    /// </summary>
    [SerializeField]
    private Vector3 _direction;
    public Vector3 BulletDirection
    {
        get
        {
            return _direction;
        }
        set
        {
            _direction = value;
        }
    }

    /// <summary>
    /// entity movement component to control the movement
    /// </summary>
    private EntityMovement _entityMovement;

    /// <summary>
    /// method call at start the class, gettting the entity movement component
    /// </summary>
    protected override void CustomAwake()
    {
        base.CustomAwake();

        _entityMovement = GetComponent<EntityMovement>();
    }

    /// <summary>
    /// method call every frame ( if the game is not paused )
    /// setting the movement momentum with the direccion and the speed
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);

        // setting the momentum with the direction and the speed
        _entityMovement.Momentum += _direction * _entityMovement.Speed * deltaTime;
    }

}
