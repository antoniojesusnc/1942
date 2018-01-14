using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Small Factory for the creation of enemies by prefabs.
/// Now is almos useless this class, but can be extend very easy, ( object pool, more create options, etc ) 
/// When create or destroy a enemy, the level is inform about it
/// </summary>
public class FactoryEnemies : MonoBehaviour
{
    /// <summary>
    /// Method to create a new enemy by the prefab from parameter. When a enemy is created, the level is notified about it
    /// </summary>
    /// <param name="EnemyPrefab">prefab to be created</param>
    /// <returns> instance of the enemy created from prefab</returns>
    public static EnemyEntity CreateEnemy(EnemyEntity EnemyPrefab)
    {
        // creating the enemy
        EnemyEntity newEnemy = Instantiate<EnemyEntity>(EnemyPrefab);
        // notification for the level manager
        GameManager.Instance.LevelManager.EnemySpawned(newEnemy);
        // retruning the new enemy
        return newEnemy;
    }

    /// <summary>
    /// Method to Remove a enemy from the game. When a enemy is removed, the level is notified about it
    /// </summary>
    /// <param name="enemy">Enemy to be destroyed</param>
    public static void DestroyEnemy(EnemyEntity enemy)
    {
        // notification for the level manager
        GameManager.Instance.LevelManager.EnemyDestroy(enemy);
        // destroing the enemy
        Destroy(enemy.gameObject);
    }
}
