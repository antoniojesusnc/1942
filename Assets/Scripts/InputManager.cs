using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class to manage the menus inputs in the game
/// for now only is managing when press escape key
/// </summary>
public class InputManager : MonoBehaviour
{
    /// <summary>
    /// method call every frame,
    /// check if the escape key is being pressed,
    /// if true check in which scene is the game, 
    ///     if in play mode, call the method PressedEscapeInGameScene
    ///     otherwise (I assume you are in some menu screen) change scene to main menu scnee
    /// </summary>
    void Update()
    {
        // detect if espake key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // is in play scene, call method PressedEscapeInGameScene
            if (SceneManager.Instance.IsPlayGameScene())
            {
                PressedEscapeInGameScene();
            }
            else
            {
                // other wise load main menu scene
                SceneManager.Instance.ChangeScene(SceneManager.MainMenuSceneIndex);
            }
        }
    }

    /// <summary>
    /// method call when press escape in game scene
    /// is the game is in pause, remove from pause
    /// is the game is not in pause, set the game in pause
    /// </summary>
    private void PressedEscapeInGameScene()
    {
        if (GameManager.Instance.LevelManager.IsGamePaused)
        {
            GameManager.Instance.LevelManager.PauseGame(false);
        }
        else
        {
            GameManager.Instance.LevelManager.PauseGame(true);
        }
    }
}
