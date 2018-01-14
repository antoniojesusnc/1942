using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// class that controls the game over menu. Extend of gui dialog menu for same behavior in all the DialogMenus
/// Showing a text with the gameover or the victory text and apturing the click in the elements for play again or go to main menu
/// </summary>
public class GUIGameOverMenu : GUIDialogMenu
{
    /// <summary>
    /// public reference to the header text, this text will show the victory or game over text
    /// </summary>
    public Text _headerText;

    /// <summary>
    /// var to set if the game is won with a victory or a loose
    /// </summary>
    private bool _isVictory;

    /// <summary>
    /// method for set the end game as a victory, this is called before open the dialog
    /// </summary>
    /// <param name="isVictory">set if the end game is with victory or loose</param>
    public void SetEndGame(bool isVictory)
    {
        _isVictory = isVictory;
    }

    /// <summary>
    /// override method that is call when the dialog is showed.
    /// Change the header text if is victory or not
    /// </summary>
    public override void OnOpenDialogMenu()
    {
        base.OnOpenDialogMenu();

        if (_isVictory)
        {
            _headerText.text = "Victory!";
            _headerText.color = Color.green;
        }
        else
        {
            _headerText.text = "Game Over";
            _headerText.color = Color.red;
        }
    }

    /// <summary>
    /// method call when click in play again button in game over menu, reloading the play scene to start again
    /// </summary>
    public void ClickInPlayAgainButton()
    {
        SceneManager.Instance.ReloadCurrentScene();
    }

    /// <summary>
    /// method call when click in main menu button in game over menu, loading the main menu scenes
    /// </summary>
    public void ClickInMainMenuButton()
    {
        SceneManager.Instance.ChangeScene(SceneManager.MainMenuSceneIndex);
    }
}
