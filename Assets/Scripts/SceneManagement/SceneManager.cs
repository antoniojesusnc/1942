using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for easy scene change, is singleton for easy access to it.
/// Also this class have some const vars with the scene index
/// 
/// 
/// Another solution is have different method to change to different scenes
/// </summary>
public class SceneManager : Singleton<SceneManager>
{
    /// <summary>
    /// const var with the loading scene index in the build settings
    /// </summary>
    public const int LoadingSceneIndex = 0;
    /// <summary>
    /// const var with the main menu scene index in the build settings
    /// </summary>
    public const int MainMenuSceneIndex = 1;
    /// <summary>
    /// const var with the play game scene index in the build settings
    /// </summary>
    public const int PlayGameSceneIndex = 2;
    /// <summary>
    /// const var with the high score scene index in the build settings
    /// </summary>
    public const int HighScoreSceneIndex = 3;
    /// <summary>
    /// const var with the credits scene index in the build settings
    /// </summary>
    public const int CreditsSceneIndex = 4;

    /// <summary>
    /// method call to load a specific scene. This method will be call using some const as a parameter
    /// </summary>
    /// <param name="index">index scene to change</param>
    public void ChangeScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    /// <summary>
    /// method call to reload scene, is ussed for the buttons play again
    /// </summary>
    public void ReloadCurrentScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// method call to know if the current scene is play scene returning true if is or false otherwise
    /// </summary>
    /// <returns></returns>
    public bool IsPlayGameScene()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == PlayGameSceneIndex;
    }
}

