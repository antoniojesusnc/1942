using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// factory for the bullets, creating and destroying it.
/// 
/// 
/// The idea is to create a pool of bullets, to avoid instantiates and destroy
/// Also this class can be used for stadistics like total bullet shots,
/// </summary>
public class FactoryBullet : MonoBehaviour
{
    /// <summary>
    /// call when a bullet must be created. The type and positions is sent by parameter
    /// </summary>
    /// <param name="BulletPrefab">bullet to be created</param>
    /// <param name="initialPosition">position when will be created the bullet</param>
    /// <returns>the bullet created</returns>
    public static Bullet CreateBullet(Bullet BulletPrefab, Vector3 initialPosition)
    {
        return Instantiate<Bullet>(BulletPrefab, initialPosition, Quaternion.identity);
    }

    /// <summary>
    /// call when a bullet must be destroyed
    /// </summary>
    /// <param name="bullet">the bullet to destroy</param>
    public static void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
