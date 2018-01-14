using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that manages the actions related with the World
/// Contain the World Speed, var that controls how fast the world move
/// Contain the World Background, that manages the images in the background
/// </summary>
public class WorldEntity : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// Move Down Speed for the world, setted by inspector
    /// </summary>
    private float _worldMovementSpeed;
    public float WorldSpeed
    {
        get
        {
            return _worldMovementSpeed;
        }
        set
        {
            _worldMovementSpeed = value;
        }
    }

    /// <summary>
    /// var to have direct access to world background, this is not fully necesary because we can do a get component when require
    /// </summary>
    public WorldBackground _worldBackground;

    /// <summary>
    /// method call by Level Manager when the level must be loaded, the level manager will specify the level to be load to change the background
    /// </summary>
    /// <param name="level">level to be loaded</param>
    public void StartLevel(int level)
    {
        _worldBackground.StartLevel(level);
    }
}
