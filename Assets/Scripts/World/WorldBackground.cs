using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class that controls the backgrounds and his movement. The idea is to set the backgrounds with the current level ( now is set in 3 backgrounds ). 
/// Those background will have the level background setted one on top of the before one, making a collumn. 
/// All will move down and when one go out the screen, this one will be reallocated on top and change the sprite to follow the level secuence.
/// This class extend from GameComponent that provides a CustomUpdate to be easyly conrolled
/// </summary>
public class WorldBackground : GameComponent
{
    /// <summary>
    /// list of background setted as endless list. The way that when reach the las position, go to the first
    /// </summary>
    public List<SpriteRenderer> _rendererImages;

    /// <summary>
    /// this var containt the ship (and the water around ) background Sprite, will be setted by editor
    /// </summary>
    [Header("InitialImage")]
    public Sprite _initalImageSprite;

    /// <summary>
    /// this var containt a repeteable water Sprite, will be setted by editor
    /// </summary>
    [Header("Repeatable Water")]
    public Sprite _repeatableWaterSprite;

    /// <summary>
    /// this var is a list of list, containt a list with all the levels sprites, and each level contain all the backgrounds
    /// The level background must to be repeteable the way that the start of the first background and the finish of the last one, must match
    /// </summary>
    [Header("SpriteLevels")]
    public List<WorldBackgroundLevelsSpritesInfo> _levelsSprites;

    /// <summary>
    /// background movement speed, this var is setted when the level start and is setted by World Entity
    /// </summary>
    private Vector3 _movementSpeedVector3;

    /// <summary>
    /// var storaging the current level
    /// </summary>
    private int _currentLevel;

    /// <summary>
    /// auxiliar var that contains the down center position of the screen in world coordenate,
    /// is used to know when a background is out the screen
    /// </summary>
    private float _screenBotPositionY;

    /// <summary>
    /// index storaging the nextBackground that will be removed
    /// </summary>
    private int _backgroundIndexToBeRemoved;
    /// <summary>
    /// auxiliar var height of the candidate background that will be removed
    /// will be used to calculate when the background is out the screen, is storaging to avoid calculate each frame
    /// </summary>
    private float _heightOfElementToBeRemoved;

    /// <summary>
    /// index storaging the last background setted
    /// </summary>
    private int _backgroundIndexLastSetted;

    /// <summary>
    /// index for the last sprite image from the level setted
    /// </summary>
    private int _levelIndexLastSetted;

    /// <summary>
    /// method call when the level start (is loaded) called by world entity with the level to be loaded.
    /// -set the background movement, 
    /// -set the screen position
    /// -set the initial backgrounds
    /// </summary>
    /// <param name="level">Level to be loaded</param>
    public void StartLevel(int level)
    {
        _currentLevel = level;

        // setting the movement from the var in world entity
        _movementSpeedVector3 = -Vector3.up * GameManager.Instance.World.WorldSpeed;
        // setting the middle bot world position of the screen
        _screenBotPositionY = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0)).y;

        // calling to set the initial background configuration
        SetFirstAndSecondImage();
    }

    /// <summary>
    /// method call when the level start (is loaded) setting the initial configuration for the backgrounds
    /// -set the first background image as the ship, 
    /// -set the position of the first background,
    /// -set the anothers backgrounds using the auxiliar method "SetNextBackgroundElement"
    /// -set the backgroudn to be removed as the first one because is the candidate
    /// -set the height of the first background
    /// </summary>
    private void SetFirstAndSecondImage()
    {

        // setting the ship image and settin his position
        _rendererImages[_backgroundIndexLastSetted].sprite = _initalImageSprite;
        _rendererImages[_backgroundIndexLastSetted].transform.position = Vector3.up * _screenBotPositionY;

        // setting the anothers backgrounds with the level sprites
        for (int i = 1; i < _rendererImages.Count; i++)
        {
            SetNextBackgroundElement(GetNextLevelSprite());
        }

        // setting the background 0 as candidate to be removed
        _backgroundIndexToBeRemoved = 0;
        _heightOfElementToBeRemoved = _initalImageSprite.rect.height * transform.lossyScale.y / _initalImageSprite.pixelsPerUnit;
    }

    /// <summary>
    /// method call when a new background must to be added on top with the new graphic
    /// -check position and heigh of the last background setted
    /// -set the next background graphic
    /// -set the next background position on top of the last
    /// -increment the candidate to be removed
    /// </summary>
    /// <param name="nextSprite"> This var is here as a possible expansion, just in case another different background want to be loaded</param>
    private void SetNextBackgroundElement(Sprite nextSprite)
    {
        // get the position and the height of the last background ( will be the one top of all the backgrounds )
        Vector3 posOfLastBackground = _rendererImages[_backgroundIndexLastSetted].transform.position;
        Sprite spriteLastBackground = _rendererImages[_backgroundIndexLastSetted].sprite;
        float heightOfLastBackground = spriteLastBackground.rect.height * transform.lossyScale.y / spriteLastBackground.pixelsPerUnit;

        // set the index for last background setted and set the sprite and position
        _backgroundIndexLastSetted = GetNextBackgroundIndexLastSetted();
        _rendererImages[_backgroundIndexLastSetted].sprite = nextSprite;
        _rendererImages[_backgroundIndexLastSetted].transform.position = posOfLastBackground + Vector3.up * heightOfLastBackground;

        // set the new candidate to be removed, index and height
        _backgroundIndexToBeRemoved = GetNextBackgroundIndexToBeRemoved();
        _heightOfElementToBeRemoved = _rendererImages[_backgroundIndexToBeRemoved].sprite.rect.height * transform.lossyScale.y / _rendererImages[_backgroundIndexToBeRemoved].sprite.pixelsPerUnit;
    }

    /// <summary>
    /// auxiliar method for the circular increment of the next background index to be removed
    /// </summary>
    /// <returns>the var incremented</returns>
    private int GetNextBackgroundIndexToBeRemoved()
    {
        return ( _backgroundIndexToBeRemoved + 1 ) % _rendererImages.Count;
    }

    /// <summary>
    /// auxiliar method for the circular increment of the next background index last setted
    /// </summary>
    /// <returns>the var incremented</returns>
    private int GetNextBackgroundIndexLastSetted()
    {
        return ( _backgroundIndexLastSetted + 1 ) % _rendererImages.Count;
    }

    /// <summary>
    /// auxiliar method for the circular increment of the next level index last setted
    /// </summary>
    /// <returns>the var incremented</returns>
    private int GetNextLevelIndexLastSetted()
    {
        return ( _levelIndexLastSetted + 1 ) % _levelsSprites[_currentLevel].sprites.Count;
    }

    /// <summary>
    /// method that gets the next (next From Circular List) sprite from the level and return it
    /// </summary>
    /// <returns>the next level sprite</returns>
    private Sprite GetNextLevelSprite()
    {
        Sprite nextSprite = _levelsSprites[_currentLevel].sprites[_levelIndexLastSetted];
        _levelIndexLastSetted = GetNextLevelIndexLastSetted();
        return nextSprite;
    }

    /// <summary>
    /// This Update move all the background down the value specify by movementSpeedVector. 
    /// Also check if the bottom background should be removed of not, 
    ///     if should be, call to set the next background
    /// Override method from GameComponent, this methos is call each frame by Level Manager in normal situation.
    /// For know special sceneario check Level Manager
    /// </summary>
    /// <param name="deltaTime">time since last update</param>
    public override void CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);

        // moving all the backgournds
        for (int i = _rendererImages.Count - 1; i >= 0; --i)
        {
            _rendererImages[i].transform.position += _movementSpeedVector3 * deltaTime;
        }

        // checking if the element is out the screen
        if (_rendererImages[_backgroundIndexToBeRemoved].transform.position.y + _heightOfElementToBeRemoved <= _screenBotPositionY)
        {
            // if out, setting the next background
            SetNextBackgroundElement(GetNextLevelSprite());
        }
    }

}
