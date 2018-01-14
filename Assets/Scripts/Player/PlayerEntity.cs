using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for the player Plane, the special behavior respect a another plane is what happen when is destroy.
/// When is destoyed, happen the game over
/// </summary>
public class PlayerEntity : EntityPlane
{
    /// <summary>
    /// override method called when the plane is destroyed, calling the level manager for game over
    /// </summary>
    /// <param name="getPoints"></param>
    public override void DestroyPlane(bool getPoints = true)
    {
        // call the finish level to game over
        GameManager.Instance.LevelManager.FinishLevel(false);
    }

}
