using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class to allow the entity fire, the entity can have multiple weapons at same time.
/// 
/// 
/// For now the weapons are assign by editor, the idea is that this weapon can change in game, so you can improve the weapons or replace it
/// </summary>
public class EntityFire : GameComponent
{
    /// <summary>
    /// list of all weapons, 
    /// for know, is assign by editor
    /// </summary>
    [SerializeField]
    private List<Weapon> _weapons;
    public List<Weapon> Weapons
    {
        get
        {
            return _weapons;
        }
        set
        {
            _weapons = value;
        }
    }

    /// <summary>
    /// main shot class, another class will call this method when the entity should shot (because key press or AI, for example)
    /// calling the fire method for all the weapons
    /// </summary>
    public void Fire()
    {
        // calling fire method for all the weapons
        for (int i = _weapons.Count - 1; i >= 0; --i)
        {
            _weapons[i].Fire();
        }
    }

    /// <summary>
    /// override method call every frame ( is game is not in pause ) 
    /// calling the update for the weapons, some weapons can have special properties and maybe need to have a update
    /// </summary>
    /// <param name="deltaTime"></param>
    public override void CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);

        // calling fire method for all the weapons
        for (int i = _weapons.Count - 1; i >= 0; --i)
        {
            _weapons[i].WeaponUpdate(deltaTime);
        }
    }
}
