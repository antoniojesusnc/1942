using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that has the general behavior and properties for the planes, doesnt matter if enemy or player.
/// Control the Health Points: the lifes of the plane.
/// Implement the interface IBUlletDetector with the method BulletDetected which is called for the class BulletDetector when a detect a bullet
/// </summary>
public abstract class EntityPlane : MonoBehaviour, IBulletDetector
{
    /// <summary>
    /// Healt point for the plane, when is 0 the plane is destroyed
    /// </summary>
    [SerializeField]
    private int _healthPoints;

    public int HealthPoints
    {
        get
        {
            return _healthPoints;
        }
        set
        {
            _healthPoints = value;
        }
    }

    /// <summary>
    /// method for the interface IBulledDetector, is called when the component  BulletDetector ( place at same level or child) detect a bullet trigger collision
    /// when bullet is detected, calling the HitPlane method with the damage
    /// </summary>
    /// <param name="bullet"></param>
    public void BulletCollisionDetected(Bullet bullet)
    {
        // calling the hit plane method with the damage
        HitPlane(bullet.Damage);
    }

    /// <summary>
    /// method call when the plane reeive damage. Is virtual just in case 
    /// </summary>
    /// <param name="damageReceived">plane damage reveived</param>
    protected void HitPlane(int damageReceived)
    {
        // deducting the damage to the HP
        HealthPoints -= damageReceived;
        //if HP less of equal 0, plane destroyed
        if (HealthPoints <= 0)
        {
            DestroyPlane();
        }
    }

    /// <summary>
    /// method call when the plane is destroyed. Can be destroyed by the player of self destroy( ussually when go out the screen )
    /// </summary>
    /// <param name="killedByPlayer">specify if the plane was destroyed by the player or not</param>
    public abstract void DestroyPlane(bool killedByPlayer = true);


}
