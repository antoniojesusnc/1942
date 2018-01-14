using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// interface for bullet detection, called for BulletDetector when a bullet detect some collision
/// </summary>
public interface IBulletDetector
{
    /// <summary>
    /// method called by bullet detector when the detect a bullet collision
    /// </summary>
    /// <param name="bullet"></param>
    void BulletCollisionDetected(Bullet bullet);
}
