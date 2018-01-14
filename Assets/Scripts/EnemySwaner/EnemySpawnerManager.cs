using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class that manages the enemy spawn in the level. This class have a list of enemy spawn info objects that represent the enemys in the battle.
/// The enemies will spawn by time
/// Also this class manage the enemies in the screen updating their states machines
/// Extend from game component because only will spawn if the game is not paused.
/// </summary>
public class EnemySpawnerManager : GameComponent
{
    /// <summary>
    /// list with the enemy spawn data
    /// </summary>
    public List<EnemySpawnInfo> _enemiesSpawnInfo;
    /// <summary>
    /// auxiliar var that to know the time since level start, use for check enemy spawn 
    /// </summary>
    private float _timeSinceStarLevel;

    /// <summary>
    /// list with all enemy's state machines
    /// </summary>
    private List<FSMachine> _stateMachines;

    /// <summary>
    /// method call when the class is created
    /// -initialize the state machine list 
    /// -sort the enemy spawn info (just in case)
    /// </summary>
    protected override void CustomAwake()
    {
        base.CustomAwake();

        _stateMachines = new List<FSMachine>();

        // order the list, just in case
        _enemiesSpawnInfo.Sort(orderByTime);
    }

    /// <summary>
    /// method call by level manager when the level is loaded
    /// </summary>
    /// <param name="level">level to be load</param>
    public void LoadLevel(int level)
    {
        // The idea is to load different enemy spawn configurations depending of the level, reading different files
    }

    /// <summary>
    /// method that sort the spawn list by time, lower to greater
    /// </summary>
    /// <param name="info1"></param>
    /// <param name="info2"></param>
    /// <returns></returns>
    private int orderByTime(EnemySpawnInfo info1, EnemySpawnInfo info2)
    {
        if (info1.Time < info2.Time)
        {
            return -1;
        }
        if (info1.Time > info2.Time)
        {
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// method to know if all the level enemies had been spawned
    /// </summary>
    /// <returns></returns>
    public bool AreAllEnemiesSpawned()
    {
        return _enemiesSpawnInfo.Count <= 0;
    }

    /// <summary>
    /// method call every frame ( if game not paused )
    /// -updating the enemies state machines
    /// -updating the spawn list info
    /// </summary>
    /// <param name="deltaTime">time since last update</param>
    public override void CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);

        // updating the state machines for the enemies
        UpdateStateMachines(deltaTime);
        // updating the enemies spawn info, spawn if necessary
        UpdateEnemiesSpawnInfo(deltaTime);
    }

    /// <summary>
    /// method that update the state machine for the enemies calling to the update method
    /// if some state machine is finish, also it is removed from the list
    /// </summary>
    /// <param name="deltaTime">time since last update</param>
    private void UpdateStateMachines(float deltaTime)
    {
        // this for is inverse, because if some state machine is removed, this is not affecting the flow
        for (int i = _stateMachines.Count - 1; i >= 0; --i)
        {
            // update the state machines
            _stateMachines[i].Update(deltaTime);

            // if state machine is finish, remove from the list, because the for is inverse, 
            // doesn't matter remove in the middle of the loop
            if (_stateMachines[i].IsFinish())
            {
                _stateMachines.Remove(_stateMachines[i]);
            }
        }
    }

    /// <summary>
    /// updateh the enemy spawn list, checking if i have to spawn some, and doing it if necessary
    /// </summary>
    /// <param name="deltaTime"></param>
    private void UpdateEnemiesSpawnInfo(float deltaTime)
    {
        // increment the timer
        _timeSinceStarLevel += deltaTime;

        // checking if next unit should be spawn, could be that a few units have the same spawn time, 
        // if they have, i need to span all of them
        while (needProcessNext())
        {
            // spawn the enemy
            processEnemyInfo();
        }
    }

    /// <summary>
    /// check if the timer for the first element in the spawn info list, 
    /// if time less that level time, return true, other wise false
    /// </summary>
    /// <returns>if first element should be spawn or not</returns>
    private bool needProcessNext()
    {
        // only check if there are enemies to be spawned
        if (!AreAllEnemiesSpawned())
        {
            // comparing the spawn time with the level time
            return _enemiesSpawnInfo[0].Time <= _timeSinceStarLevel;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// method called when a enemy should be spawn,
    /// -create the enemy with the enemy factory
    /// -call the method "CreateEnemyBehaviorFSM" for create the enemy state machine with the spawn info data
    /// -set the FSM to the enemy
    /// -add the FSM to the list
    /// -remove the spawn info from the level spawn info
    /// </summary>
    private void processEnemyInfo()
    {
        // create the enemy
        EnemyEntity enemyBeingSpawn = FactoryEnemies.CreateEnemy(_enemiesSpawnInfo[0].EnemyPrefab);
        // create the FSM for the enemy with the spawn data
        FSMachine machine = CreateEnemyBehaviorFSM(enemyBeingSpawn, _enemiesSpawnInfo[0]);
        // set the behavior var for the enemy
        enemyBeingSpawn.GetComponentInChildren<EnemyBehavior>().Behavior = machine;
        // adding the enemy to the FSM
        AddNewEnemyFSM(machine);
        // remove for the info
        _enemiesSpawnInfo.RemoveAt(0);
    }

    /// <summary>
    /// method that create the state machine for the enemy, with the data from enemy spawn info.
    /// get the data and create the stane machine
    /// 
    /// the idea is have the data from the enemy spawn info of different way. So here depending how is the info,
    /// differents state machines will be created so the enemy will have differents behavior
    /// </summary>
    /// <param name="enemyBeingSpawn"></param>
    /// <param name="enemySpawnInfo"></param>
    /// <returns></returns>
    private FSMachine CreateEnemyBehaviorFSM(EnemyEntity enemyBeingSpawn, EnemySpawnInfo enemySpawnInfo)
    {
        // getting the data for the enemy state machine
        Vector3 initialWorldPosition = Camera.main.ViewportToWorldPoint(enemySpawnInfo.EnemyViewPortInitialPosition);
        Vector3 turnWorldPosition = Camera.main.ViewportToWorldPoint(enemySpawnInfo.EnemyViewPortTurnPosition);
        Vector3 finalWorldPosition = Camera.main.ViewportToWorldPoint(enemySpawnInfo.EnemyViewPortFinalPosition);

        // creating the FSM with the data from enemy spawn info
        return new FSMEnemyGoShotAndLeave(enemyBeingSpawn, initialWorldPosition, turnWorldPosition, finalWorldPosition);
    }

    /// <summary>
    /// method called when the enemy FSM is created, 
    /// Add the enemy FSM to the list an start the FSM
    /// </summary>
    /// <param name="machine"></param>
    private void AddNewEnemyFSM(FSMachine machine)
    {
        // add the new FSM to the list
        _stateMachines.Add(machine);
        // call to init the state machine
        machine.Create();
    }

}
