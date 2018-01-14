using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GUI script to show the high Scores in the list.
/// Generate X elements, as much as HighScore there are
/// </summary>
public class GUIShowHighScore : MonoBehaviour
{
    /// <summary>
    /// prefab for the highScore field, setted by editor
    /// </summary>
    public Text _highScorePrefab;

    /// <summary>
    /// methos call when create the script, create the highscore field as child of this object and set the highscore Text to each one
    /// </summary>
    void Awake()
    {
        // this will be the format for the highScore Text ( im assuming that the puntuation never will be greater than 99999
        string highScoreFormat = "00000";
        // list with all the highScores
        List<int> highScores = GameManager.Instance.HighScores;

        Text newHighScoreElement;
        // creating elements as child and setting the highScore value
        for (int i = 0; i < GameManager.HighScoreAmount; i++)
        {
            newHighScoreElement = Instantiate<Text>(_highScorePrefab, transform);
            newHighScoreElement.text = string.Format("{0}. {1}", ( i + 1 ).ToString(), highScores[i].ToString(highScoreFormat));
        }
    }
}
