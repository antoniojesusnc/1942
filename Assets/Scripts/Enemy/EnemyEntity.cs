using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for the normal enemy Plane. Have the point that the player should receive when is destroyed
/// </summary>
public class EnemyEntity : EntityPlane
{
    /// <summary>
    /// Points setted by editor about how many point shuld get the player when this enemy is destroyed
    /// </summary>
    [SerializeField]
    private int _points;
    public int Points
    {
        get
        {
            return _points;
        }
        set
        {
            _points = value;
        }
    }

    /// <summary>
    /// override method call when the plane is destroyed, if killed by enemy is calling to change the points
    /// Also calling to the Factory Enemies telling that this plane is destoyed
    /// </summary>
    /// <param name="destroyedByPlayer"></param>
    public override void DestroyPlane(bool destroyedByPlayer = true)
    {
        // if the plane was destroyed by the player, increment the level score in the level manager
        if (destroyedByPlayer)
        {
            GameManager.Instance.LevelManager.LevelScore += _points;
        }

        // calling the enemy factory about the plane destruction
        FactoryEnemies.DestroyEnemy(this);
    }


}
