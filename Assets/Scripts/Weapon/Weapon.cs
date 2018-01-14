using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for a simple weapon, a weapon that just shot to a in one direction.
/// 
/// The idea is to extends this class and add more complex weapons, for example a rotate weapon or a weapon that shoots forward and backward at same time
/// Also can add a weapon with cooldown easyly
/// </summary>
[System.Serializable]
public class Weapon
{
    /// <summary>
    /// weapon bullet
    /// </summary>
    public Bullet bullet;
    /// <summary>
    /// initial position for the bullet
    /// </summary>
    public Transform InitialPosition;
    /// <summary>
    /// bullet direccion
    /// </summary>
    public Vector3 BulletDirection;

    /// <summary>
    /// method call when the weapon should shot.
    /// if the weapon can shot, create the new bullet using the factory and set the bullet vars ( the direction is the only one for now)
    /// </summary>
    public virtual void Fire()
    {
        // only if the weapon can shot will do it
        if (CanShot())
        {
            // create the bullet using the factory
            Bullet newBullet = FactoryBullet.CreateBullet(bullet, InitialPosition.position);
            // add the properties, for now only the direccion
            newBullet.GetComponent<BulletMovement>().BulletDirection = BulletDirection;
        }
    }

    /// <summary>
    /// virtual method to know if the bullet can shot or not.
    /// The normal weapon always can shot
    /// </summary>
    /// <returns>if can shot or not</returns>
    public virtual bool CanShot()
    {
        return true;
    }

    /// <summary>
    /// virutal method to allow the weapon do someting each update ( a cooldown for example )
    /// </summary>
    /// <param name="deltaTime">time since last update</param>
    public virtual void WeaponUpdate(float deltaTime)
    {

    }
}
