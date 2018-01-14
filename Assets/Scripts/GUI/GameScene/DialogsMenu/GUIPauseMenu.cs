using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// class that controls the pause menu. Extend of gui dialog menu for same behavior in all the DialogMenus
/// Capturing the click in the elements and resuming game, reloading or going to main menu
/// </summary>
public class GUIPauseMenu : GUIDialogMenu
{
    /// <summary>
    /// method call when click in resume button in pause menu, un pausing the game
    /// </summary>
    public void ClickInResumeButton()
    {
        GameManager.Instance.LevelManager.PauseGame(false);
    }

    /// <summary>
    /// method call when click in play again button in pause menu, reloading play scene
    /// </summary>
    public void ClickInPlayAgainButton()
    {
        //SceneManager.Instance.ChangeScene(SceneManager.PlayGameSceneIndex);
        SceneManager.Instance.ReloadCurrentScene();
    }

    /// <summary>
    /// method call when click in main manu button in pause menu, opening the main menu scene
    /// </summary>
    public void ClickInMainMenuButton()
    {
        SceneManager.Instance.ChangeScene(SceneManager.MainMenuSceneIndex);
    }
}
