using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Auxiliar component to set a object as dont destroy on load
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour
{

    /// <summary>
    /// method call at start the class, just set the dont destroy on load
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
