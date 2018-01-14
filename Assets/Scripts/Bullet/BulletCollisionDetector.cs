using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class that know when a bullet collide with a trigger calling to the method "BulletCollisionDetected" from the listener IBulletDetector
/// This class have a public var for specify the the layer of the bullet to detect
/// This class must be placed with a collider2D
/// </summary>
public class BulletCollisionDetector : MonoBehaviour
{
    /// <summary>
    /// setted by editor var for know the layering for the bullet to detect.
    /// </summary>
    public LayerMask _bulletLayerToDetect;

    /// <summary>
    /// method call when a bullet enter in the trigger object, calling the checkIfBulletDetected method
    /// </summary>
    /// <param name="collider">object who collide with this object</param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        checkIfBulletDetected(collider);
    }

    /// <summary>
    /// method call when a bullet exit in the trigger object, calling the checkIfBulletDetected method
    /// </summary>
    /// <param name="collider">object who collide with this object</param>
    void OnTriggerExit2D(Collider2D collider)
    {
        checkIfBulletDetected(collider);
    }

    /// <summary>
    /// method call when a bullet stay in the trigger object, calling the checkIfBulletDetected method
    /// </summary>
    /// <param name="collider">object who collide with this object</param>
    void OnTriggerStay2D(Collider2D collider)
    {
        checkIfBulletDetected(collider);
    }

    /// <summary>
    /// method that is call from the collision detector checing if the object detected have the layering required and if is a bullete.
    /// if it is, call the method from the interface "BulletCollisionDetected" to the object and the bullet
    /// </summary>
    /// <param name="collider"></param>
    private void checkIfBulletDetected(Collider2D collider)
    {
        // checking if the layering is the searched one
        int layerValue = 1 << collider.gameObject.layer;
        if (layerValue == _bulletLayerToDetect.value)
        {
            // if is, get the component IBulletDetector and call the the method from the interface for the object and for the bullet
            IBulletDetector bulletDetector = GetComponentInParent<IBulletDetector>();
            if (bulletDetector != null)
            {
                Bullet bullet = collider.GetComponentInParent<Bullet>();
                bulletDetector.BulletCollisionDetected(bullet);
                bullet.BulletCollisionDetected(bullet);
            }
        }
    }
}
