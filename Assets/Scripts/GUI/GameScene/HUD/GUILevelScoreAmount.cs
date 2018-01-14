using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// gui class that shows the level score and update it when this change because suscribe to the listener
/// </summary>
public class GUILevelScoreAmount : MonoBehaviour
{
    /// <summary>
    /// test the text that will be updated with the level score data
    /// </summary>
    public Text _text;

    /// <summary>
    /// method call when the class start
    /// -suscribe to the level score listener
    /// -set the initial value calling the HandleLevelScoreChange with the initial value
    /// </summary>
    void Start()
    {
        // suscribing to the listener for know when the level score change
        GameManager.Instance.LevelManager.OnLevelScoreChange += HandleLevelScoreChange;
        // setting the initial value
        HandleLevelScoreChange(GameManager.Instance.LevelManager.LevelScore);
    }

    /// <summary>
    /// method suscribre to the level score change listener that will be call when the level score change.
    /// setting the text with the score value
    /// </summary>
    /// <param name="score">current score value</param>
    private void HandleLevelScoreChange(int score)
    {
        _text.text = score.ToString();
    }

    /// <summary>
    /// method call when the class is destroyed
    /// Unsuscribe for the listener
    /// </summary>
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.LevelManager.OnLevelScoreChange -= HandleLevelScoreChange;
    }
}
