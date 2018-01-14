using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class that sets some triggers outside the screen to destroy the bullets when they go outside screen
/// Thanks to this class we avoid have useless bullets outside screen
/// Set 4 triggers, one per screen side ( top, bot, left and right )
/// </summary>
public class WorldBulletLimits : MonoBehaviour, IBulletDetector
{
    /// <summary>
    /// trigger located on top of the screen
    /// </summary>
    public BoxCollider2D _topBulletLimit;
    /// <summary>
    /// trigger located on bot of the screen
    /// </summary>
    public BoxCollider2D _botBulletLimit;

    /// <summary>
    /// trigger located on left of the screen
    /// </summary>
    public BoxCollider2D _leftBulletLimit;
    /// <summary>
    /// trigger located on right of the screen
    /// </summary>
    public BoxCollider2D _rightBulletLimit;

    /// <summary>
    /// method call at start of the class, calling the method to set the limits
    /// </summary>
    private void Awake()
    {
        SetBulletLimits();
    }

    /// <summary>
    /// class that sets the position and the size of the 4 triggers outside the screen
    /// </summary>
    private void SetBulletLimits()
    {
        // first at all, i need to know what are the world position for the current screen size
        Vector3 botLeftPos = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 topRightPos = Camera.main.ViewportToWorldPoint(Vector3.one);
        Vector2 screenSize = new Vector2(topRightPos.x - botLeftPos.x, topRightPos.y - botLeftPos.y);

        // setting the size for top trigger, will be half screen per left and right of the screen and half screen taller
        _topBulletLimit.size = new Vector2(screenSize.x + screenSize.x, screenSize.y * 0.5f);
        // setting top limit, instead use 0.5 for a perfect location on top, i use 0.6 to leave a small offset
        _topBulletLimit.transform.position = Vector3.up * ( topRightPos.y + _topBulletLimit.size.y * 0.6f );


        // setting the size for bot trigger, will be half screen per left and right of the screen and half screen taller
        _botBulletLimit.size = new Vector2(screenSize.x + screenSize.x, screenSize.y * 0.5f);
        // setting bot limit, instead use 0.5 for a perfect location on top, i use 0.6 to leave a small offset
        _botBulletLimit.transform.position = Vector3.up * ( botLeftPos.y - _botBulletLimit.size.y * 0.6f );

        // setting the size for left trigger, will be half screen per left and right of the screen and half screen taller
        _leftBulletLimit.size = new Vector2(screenSize.x * 0.5f, screenSize.y);
        // setting left limit, instead use 0.5 for a perfect location on top, i use 0.6 to leave a small offset
        _leftBulletLimit.transform.position = Vector3.right * ( botLeftPos.x - _leftBulletLimit.size.x * 0.6f );

        // setting the size for right trigger, will be half screen per left and right of the screen and half screen taller
        _rightBulletLimit.size = new Vector2(screenSize.x * 0.5f, screenSize.y);
        // setting right limit, instead use 0.5 for a perfect location on top, i use 0.6 to leave a small offset
        _rightBulletLimit.transform.position = Vector3.right * ( topRightPos.x + _rightBulletLimit.size.x * 0.6f );


    }

    /// <summary>
    /// Method from Interface IBulletDetector called when a bullet go inside the collider.
    /// When happend, destroy the bullet
    /// </summary>
    /// <param name="bullet"></param>
    public void BulletCollisionDetected(Bullet bullet)
    {
        DestroyBullet(bullet);
    }

    private void DestroyBullet(Bullet bullet)
    {
        //FactoryBullet.DestroyBullet(bullet);
    }
}
