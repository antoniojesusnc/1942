using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// gui class for the dialogs menus same behavior, this specify the normal behavior for all the dialogs, 
/// behavior like showing and closing.
/// 
/// The idea is that all the dialogs menus will extend this and override OnOpenDialogMenu and/or Init if required
/// </summary>
public class GUIDialogMenu : MonoBehaviour
{
    /// <summary>
    /// method call then the class is created. Calling the virtual method Init
    /// </summary>
    void Awake()
    {
        Init();
    }

    /// <summary>
    /// overwritable method called at start of the game, hiding the dialog if open because no dialog at start the game
    /// </summary>
    public virtual void Init()
    {
        if (gameObject.activeSelf)
            Hide();
    }

    /// <summary>
    /// method call to show the dialog, enabling the object and calling the virtual method OnOpenDialogMenu
    /// 
    /// The idea is to add animations when show, then all the dialog will have the same anims
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
        OnOpenDialogMenu();
    }

    /// <summary>
    /// method call when show the dialog, the idea is to overrite and do the logic of the dialogs with this method
    /// </summary>
    public virtual void OnOpenDialogMenu()
    {

    }

    /// <summary>
    /// method call to hide the dialog, disabling the object
    /// 
    /// The idea is to add animations when hide, then all the dialog will have the same anims
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
