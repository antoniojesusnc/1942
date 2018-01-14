using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// auxiliar class use only to set the sprites level by editor
/// 
/// The idea is have this list by code and setting by json, xml, asset bundle or something else, so this class will be nor required
/// </summary>
[System.Serializable]
public class WorldBackgroundLevelsSpritesInfo
{
    /// <summary>
    /// list of level sprites
    /// </summary>
    public List<Sprite> sprites;
}
