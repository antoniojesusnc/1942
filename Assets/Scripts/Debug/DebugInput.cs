using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class used to debug the game
/// this class will detect some key imput and will do diffente debug actions
/// this class must be removed if final game version
/// </summary>
public class DebugInput : MonoBehaviour
{
    /// <summary>
    /// float used to debug propose
    /// </summary>
    public float _floatValue;
    /// <summary>
    /// vector2 used to debug propose
    /// </summary>
    public Vector2 _vector2Value;
    /// <summary>
    /// vector3 used to debug propose
    /// </summary>
    public Vector3 _vector3Value;

    // Update is called once per frame
    void Update()
    {
        // if key U is press
        if (Input.GetKey(KeyCode.U))
        {
            Debug.Log(Camera.main.ViewportToWorldPoint(_vector2Value));
        }

        // if key I is press
        if (Input.GetKey(KeyCode.I))
        {
            Debug.Log(Camera.main.ViewportToWorldPoint(_vector3Value));
        }

    }
}
