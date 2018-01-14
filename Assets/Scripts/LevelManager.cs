using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class to control the level objects and flow. All the level actions go through this class
/// -Manage the score level
/// -Manage if game is paused or not
/// -Call update and late update for the GameComponent class ( this allow to pause everything )
/// -Control the victory or Loose condition ( counting the enemies alive )
/// </summary>
public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// var to control if the game is paused or not
    /// </summary>
    private bool _gamePaused;
    public bool IsGamePaused
    {
        get
        {
            return _gamePaused;
        }
    }

    /// <summary>
    /// event call when the level score change
    /// </summary>
    /// <param name="score"></param>
    public delegate void ScoreDelegate(int score);
    public event ScoreDelegate OnLevelScoreChange;

    /// <summary>
    /// control the current level score
    /// When the value change, the listener OnLevelScoreChange is called
    /// Also when change if the value is greater than HighScore, the highScore will be setted with the new value
    /// </summary>
    private int _levelScore;
    public int LevelScore
    {
        get
        {
            return _levelScore;
        }
        set
        {
            _levelScore = value;
            if (OnLevelScoreChange != null)
                OnLevelScoreChange(_levelScore);
            if (_levelScore > HighScore)
            {
                HighScore = _levelScore;
            }
        }
    }

    /// <summary>
    /// listener call when the highScorechange
    /// </summary>
    public event ScoreDelegate OnHighScoreChange;

    /// <summary>
    /// var setted at start of the game, represent the Game HighScore
    /// When the value change, the listener OnHighScoreChange will be called
    /// </summary>
    private int _highScore;
    public int HighScore
    {
        get
        {
            return _highScore;
        }
        set
        {
            _highScore = value;
            if (OnHighScoreChange != null)
                OnHighScoreChange(_highScore);
        }
    }

    /// <summary>
    /// var that saves the level time ( with count the pause )
    /// </summary>
    private float _levelTime;
    public float LevelTime
    {
        get
        {
            return _levelTime;
        }
    }

    /// <summary>
    /// var used for gameover condition, represent the enemies killed by the player.
    /// This var will be modify when a enemy die because the player
    /// </summary>
    private int _enemiesKilledByPlayer;
    /// <summary>
    /// var used for gameover condition, represent the enemies in the screen
    /// This var will be modify when a enemy die
    /// </summary>
    private int _enemiesAlive;
    /// <summary>
    /// Manager for the unit spawn, this class control the enemies spawn
    /// </summary>
    private EnemySpawnerManager _enemySpawnerManager;

    /// <summary>
    /// list of all GameComponent class, the element in this list will receive the call fro the update and late update when 
    /// required ( always except game in pause )
    /// </summary>
    List<GameComponent> _components;
    public List<GameComponent> GameComponents
    {
        get
        {
            if (_components == null)
                _components = new List<GameComponent>();
            return _components;
        }
    }

    /// <summary>
    /// method call when the class is created,
    /// -set itself to the Game MAnager
    /// -call the level to star
    /// 
    /// The idea is to have ome checking to know which level should be loaded, and then call Start level with the level number
    /// </summary>
    private void Awake()
    {
        _levelTime = 0;
        GameManager.Instance.LevelManager = this;
        _enemySpawnerManager = GetComponent<EnemySpawnerManager>();
        // call to start the level, by defaul will load the level 1, the idea is to change this value to load different levels
        StartLevel();
    }

    /// <summary>
    /// Method that starts the level, calling to another class about which level is being loaded
    /// -get the highScore
    /// -call the spawner class to load level ( with the level number )
    /// -call to the world entity class to start the level ( with the level number )
    /// </summary>
    /// <param name="level">level index to be loaded</param>
    public void StartLevel(int level = 1)
    {
        // getting the highscore
        HighScore = GameManager.Instance.HighScores[0];
        // calling the spawner to load level
        _enemySpawnerManager.LoadLevel(level);
        // calling the world to load level
        GameManager.Instance.World.StartLevel(level);
    }

    /// <summary>
    /// method call when the game shuld pause or unpause. This method will call the gui and set the var _gamePaused
    /// </summary>
    /// <param name="pause">setting game pause or not</param>
    public void PauseGame(bool pause)
    {
        _gamePaused = pause;
        GameManager.Instance.GUIManager.PauseGame(pause);
    }

    /// <summary>
    /// method call 
    /// when the level finish, calling the gui and calling to the gamemanger because the new score
    /// The game can finish because the player is destroyed (victory = false) of when all enemies are spanw and destroyed (victory = true)
    /// </summary>
    /// <param name="victory"></param>
    public void FinishLevel(bool victory)
    {
        // calling the game manager with the level score
        GameManager.Instance.NewScore(LevelScore);
        // calling the GUI to finish level
        GameManager.Instance.GUIManager.FinishLevel(victory);
        // pausing the game
        _gamePaused = true;
    }

    /// <summary>
    /// Main Update flow in the game, if the game is pause, breaking the process.
    /// -increment the game time
    /// -call the gameComponent updates
    /// </summary>
    void Update()
    {
        // if game is paused, break the process
        if (_gamePaused)
            return;

        //increment the game Time
        float deltaTime = Time.deltaTime;
        _levelTime += deltaTime;

        //calling the game component updates
        if (_components.Count > 0)
        {
            for (int i = _components.Count - 1; i >= 0; --i)
            {
                _components[i].CustomUpdate(deltaTime);
            }
        }
    }

    /// <summary>
    /// secondary main update of the game, the behavior is similar to Update but is call after that
    /// So breaking the process if game paused
    /// calling the game component late update method
    /// </summary>
    void LateUpdate()
    {
        // breaking the process if game paused
        if (_gamePaused)
            return;

        // calling the late update for the game component method
        if (_components.Count > 0)
        {
            float deltaTime = Time.deltaTime;
            for (int i = _components.Count - 1; i >= 0; --i)
            {
                _components[i].CustomLateUpdate(deltaTime);
            }
        }
    }

    /// <summary>
    /// method call when a enemy is spawned in the world, allow to have the control over how many enemies there are 
    /// alive for the game over condition incrementing the enemy alive var when the is created
    /// </summary>
    /// <param name="enemySpawned">new enemy spawned</param>
    public void EnemySpawned(EnemyEntity enemySpawned)
    {
        // increment this var to know how many eemies are alive
        ++_enemiesAlive;
    }

    /// <summary>
    /// Method call when a enemy is destroyed. Is call either when is destroyed because go out the screen or either when is killed by the player
    /// Also control when is game Over ( because no enemies alive and all enemies spawned
    /// -decrease the _enemiesAlive var always,
    /// -increment the _enemiesKilledByPlayer var when the enemy die and have no healp points (was killed)
    /// -check if game over checking if no enemies alive and all enemies spawned
    /// </summary>
    /// <param name="enemySpawned"></param>
    public void EnemyDestroy(EnemyEntity enemySpawned)
    {
        // decreasing the enemies alive in the screen
        --_enemiesAlive;
        // if no HP, meaning killed by player, so incremening the var
        if (enemySpawned.HealthPoints <= 0)
        {
            ++_enemiesKilledByPlayer;
        }
        //if no enemiy alive and all enemies spawn, meaning game over ( victory ) 
        if (_enemiesAlive <= 0 && _enemySpawnerManager.AreAllEnemiesSpawned())
        {
            FinishLevel(true);
        }
    }


}
