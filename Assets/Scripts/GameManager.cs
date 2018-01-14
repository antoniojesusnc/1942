using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main class of the game, is singleton to allow to access from any part or the game and is ussed as link to acess to some 
/// other vars ( avoing gameobjects Find and multiples singlesont but making less flexible )
/// -Responsable of Storage important var to easy acess
/// -Responsable of save and have the highScores. The highScores only are necesary at some point, 
///     if the load from player pref is enough fast, the highScores could be loaded when required
/// </summary>
public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// Var with the string key that will be saved in the player pref to save the highScores
    /// </summary>
    private const string HighScoreKey = "HighScore";
    /// <summary>
    /// amount of the hightScore to be saved and loaded from the player pref
    /// </summary>
    public const int HighScoreAmount = 5;

    /// <summary>
    /// direct access to the level manager, to allow to access from everywhere and have control over it.
    /// This var will be setted by the own level manager when start
    /// </summary>
    private LevelManager _levelManager;
    public LevelManager LevelManager
    {
        get
        {
            return _levelManager;
        }
        set
        {
            _levelManager = value;
        }
    }

    /// <summary>
    /// direct access to the Gui manager, to allow to access from everywhere and have control over it
    /// This var will be setted by the own gui manager when start
    /// </summary>
    private GUIManager _GUIManager;
    public GUIManager GUIManager
    {
        get
        {
            return _GUIManager;
        }
        set
        {
            _GUIManager = value;
        }
    }

    /// <summary>
    /// direct access to the World Entity, to allow to access from everywhere and have control over it
    /// this var will be assign when required for first time
    /// </summary>
    private WorldEntity _world;
    public WorldEntity World
    {
        get
        {
            if (_world == null)
            {
                _world = FindObjectOfType<WorldEntity>();
                if (_world == null)
                {
                    Debug.LogError("GameManager World Error: World Entity not found in Scene");
                }
            }
            return _world;
        }
        set
        {
            _world = value;
        }
    }

    /// <summary>
    /// High Scores for the game, this var will be setted by the method loadHighScores
    /// </summary>
    private List<int> _scores;
    public List<int> HighScores
    {
        get
        {
            if (_scores == null)
                LoadHighScores();
            return _scores;
        }
    }

    /// <summary>
    /// Method that loads the highScores from player pref and assign them to the _scores list
    /// </summary>
    private void LoadHighScores()
    {
        _scores = new List<int>();
        for (int i = 0; i < HighScoreAmount; ++i)
        {
            _scores.Add(PlayerPrefs.GetInt(HighScoreKey + i, 0));
        }
    }

    /// <summary>
    /// Method call when a new Score is achieve, no need to be a highScore, just a score.
    /// This meaning that will be call when the level finish ( doesnt matter if finish with victory or not )
    /// </summary>
    /// <param name="score">score get at game over</param>
    public void NewScore(int score)
    {
        // assuming the score is in order ( greater to lower ), Only if the new score is greater than the last one, will be added
        if (score > _scores[HighScoreAmount - 1])
        {
            // add, order, remove the last one and saving the data in player pref
            _scores.Add(score);
            _scores.Sort(SortHighScores);
            _scores.RemoveAt(HighScoreAmount);
            SaveHighScore();
        }
    }

    /// <summary>
    /// auxiliar method that orders a list from greater to lower integer
    /// </summary>
    /// <param name="score1"></param>
    /// <param name="score2"></param>
    /// <returns></returns>
    private int SortHighScores(int score1, int score2)
    {
        if (score1 > score2)
            return -1;
        if (score1 < score2)
            return 1;

        return 0;
    }

    /// <summary>
    /// method that saves the scores in player pref, 
    /// setting the vars with the constant key HighScoreKey
    /// </summary>
    private void SaveHighScore()
    {
        for (int i = 0; i < HighScoreAmount; ++i)
        {
            PlayerPrefs.SetInt(HighScoreKey + i, _scores[i]);
        }
    }
}
