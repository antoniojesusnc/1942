using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// extendable class to allow the custom method awake, update, and late update. This allow to have more control about the updates and the late updates.
/// This class will register itself to a list in the level manager, so the level manager will call the update and the late update when required
/// </summary>
public class GameComponent : MonoBehaviour
{
    /// <summary>
    /// method call at start this class,
    /// this class will register itself to the level mangager list and will call a to a customAwake method
    /// </summary>
    private void Awake()
    {
        // calling to custom awake method
        CustomAwake();
        // registering this class into the level manager list
        GameManager.Instance.LevelManager.GameComponents.Add(this);
    }

    /// <summary>
    /// method call when this class start. the idea is use this method instead of the normal awake for avoir error in awake
    /// </summary>
    protected virtual void CustomAwake()
    {

    }

    /// <summary>
    /// method call every frame for the level manager when required ( game not paused ).
    /// Using this system will avoid control when the game is paused and will avoid use the unity Update ( has bad perfomance )
    /// </summary>
    /// <param name="deltaTime">time since last update</param>
    public virtual void CustomUpdate(float deltaTime)
    {

    }

    /// <summary>
    /// method call every frame after the update for the level manager when required ( game not paused ).
    /// Using this system will avoid control when the game is paused and will avoid use the unity LateUpdate ( has bad perfomance )
    /// </summary>
    /// <param name="deltaTime">time since last update</param>
    public virtual void CustomLateUpdate(float deltaTime)
    {

    }

    /// <summary>
    /// method call by Unity when the object is destory, if this happen this object must to be removed from the level manager list
    /// </summary>
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.LevelManager.GameComponents.Remove(this);
    }
}
