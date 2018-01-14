using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// small class to set a behavior to go to main menu for some buttons
/// </summary>
public class GUIGoToMainMenu : MonoBehaviour
{
    /// <summary>
    /// method call when click in the button go to main menu. Change the scene to main menu
    /// </summary>
    public void ClickInButton()
    {
        SceneManager.Instance.ChangeScene(SceneManager.MainMenuSceneIndex);
    }
}
