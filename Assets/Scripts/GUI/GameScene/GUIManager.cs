using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class that manage the GUI in the game mode.
/// For now only has two dialos, pause dialog and game over dialog. For open or cloase any dialogs you have to do by this class
/// </summary>
public class GUIManager : MonoBehaviour
{
    /// <summary>
    /// pause menu dialog setted by inspector
    /// </summary>
    public GUIDialogMenu _pauseMenu;
    /// <summary>
    /// game over dialog setted by inspector
    /// </summary>
    public GUIGameOverMenu _gameOverMenu;

    /// <summary>
    /// method called when the class is created,
    /// setting the var gui manager from the game manager to this, 
    /// Removing the cursos because we are in play mode
    /// </summary>
    private void Awake()
    {
        GameManager.Instance.GUIManager = this;
        Cursor.visible = false;
    }

    /// <summary>
    /// method called when the pause menu should show or hide
    /// show or hide the pause menu and set the cursos visible or not is pause or not ( pause so cursor, no pause no cursor)
    /// </summary>
    /// <param name="pause">set is must show or hide the pause menu</param>
    public void PauseGame(bool pause)
    {
        // show or hide the cursos is pause is open or close
        Cursor.visible = pause;
        // if pause, show pause menu
        if (pause)
        {
            _pauseMenu.Show();
        }
        else
        {
            // otherwise, hide pause menu
            _pauseMenu.Hide();
        }
    }

    /// <summary>
    /// method call by the level manager when finish the game
    /// will open the game over menu and will show the cursor
    /// </summary>
    /// <param name="isVictory">set if the game finish with a victory or loose</param>
    public void FinishLevel(bool isVictory)
    {
        // enable the cursor
        Cursor.visible = true;
        // set the game over menu if victory or loose
        _gameOverMenu.SetEndGame(isVictory);
        // open the game over menu
        _gameOverMenu.Show();
    }
}
