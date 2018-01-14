using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class with the spawn enemy info. Now is set by editor
/// 
/// Now this class is very specific, the idea is to do this class more generic ( with a string diccionary for example) 
/// and then this class will filled by text file, asset bundle, or something else.
/// Also now is set the prefab of the enemy, the idea is to set the type of the enemy, and not the object itself
/// </summary>
[System.Serializable]
public class EnemySpawnInfo
{
    /// <summary>
    /// Time when the enemy will be created
    /// </summary>
    public float Time;
    /// <summary>
    /// viewPort where the enmy will be created
    /// </summary>
    public Vector2 EnemyViewPortInitialPosition;
    /// <summary>
    /// viewport position whre the enemy will turn and shot
    /// </summary>
    public Vector2 EnemyViewPortTurnPosition;
    /// <summary>
    /// viewport position where the enemy will be destroy automatly
    /// </summary>
    public Vector2 EnemyViewPortFinalPosition;
    /// <summary>
    /// prefab of the enemy that will be spawn
    /// </summary>
    public EnemyEntity EnemyPrefab;


}
