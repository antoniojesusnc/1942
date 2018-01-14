using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// main class for the bullet, 
/// -setting the damage 
/// -calling to factory bullet destry when a collision is detected
/// </summary>
public class Bullet : MonoBehaviour, IBulletDetector
{
    /// <summary>
    /// Bullet damage, now set by editor
    /// </summary>
    [SerializeField]
    public int _damage;
    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }

    /// <summary>
    /// implementation of interface IBulletDetector and called from BulletDetetor when a bullet detect a collision
    /// When a collision is detect, calling the factory bullet to destroy the bullet
    /// </summary>
    /// <param name="bullet">bullet to be destroyed ( will be bullet == this )</param>
    public void BulletCollisionDetected(Bullet bullet)
    {
        // calling the factory bullet to destroy the bullet
        FactoryBullet.DestroyBullet(bullet);
    }
}
