using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Auxiliar class to change the scene automatly after a certain time
/// </summary>
public class AutoChangeScene : MonoBehaviour
{
    /// <summary>
    /// waiting time until change scene
    /// </summary>
    public float _timeToChange;

    /// <summary>
    /// scene index to change
    /// </summary>
    public int _indexSceneToChange;

    /// <summary>
    /// in the first frame start the counter ( made with a coroutine )
    /// </summary>
    public void Start()
    {
        StartCoroutine(ChangeSceneInCo());
    }

    /// <summary>
    /// coroutine that wait some time and the load scene
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeSceneInCo()
    {
        // waiting the time specify by the var
        yield return new WaitForSeconds(_timeToChange);

        // load scene specify by the var
        SceneManager.Instance.ChangeScene(_indexSceneToChange);
    }
}
