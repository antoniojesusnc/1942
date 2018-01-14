using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class to manage menu in the the main menu scene
/// </summary>
public class GUIMainMenu : MonoBehaviour
{
    /// <summary>
    /// method call when click on play game button in the main menu scene. 
    /// Load the play game scene
    /// </summary>
    public void ClickOnPlayGame()
    {
        SceneManager.Instance.ChangeScene(SceneManager.PlayGameSceneIndex);
    }

    /// <summary>
    /// method call when click on high score button in the main menu scene. 
    /// Load the high score scene
    /// </summary>
    public void ClickOnHighScores()
    {
        SceneManager.Instance.ChangeScene(SceneManager.HighScoreSceneIndex);
    }

    /// <summary>
    /// method call when click on credits button in the main menu scene. 
    /// Load the credits scene
    /// </summary>
    public void ClickOnCredits()
    {
        SceneManager.Instance.ChangeScene(SceneManager.CreditsSceneIndex);
    }

    /// <summary>
    /// method call when click on exit game button in the main menu scene. 
    /// if in unity editor, stop play mode.
    /// if normal game, quit game
    /// </summary>
    public void ClickOnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
