using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreKeeper : MonoBehaviour
{
    AudioManager audioManager; 

    /// <summary>
    /// Score Keeper Object
    /// </summary>
    public ScoreKeeper scoreKeeper;

    /// <summary>
    /// Adds points to the score
    /// </summary>
    public void ScorePoints(int points)
    {
        scoreKeeper.ScorePointsInternal(points);
    }

    /// <summary>
    /// Holds the current score
    /// </summary>
    public int Score;

    /// <summary>
    /// Text component 
    /// </summary>
    private TMP_Text scoreDisplay;

    /// <summary>
    /// Initialize scoreKepper, aduioManager, scoreDisplay.
    /// </summary>
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        scoreDisplay = GetComponent<TMP_Text>();
        ScorePointsInternal(0);
    }

    /// <summary>
    /// Changes the score variable and updates it
    /// </summary>
    private void ScorePointsInternal(int delta)
    {
        Score += delta;
        audioManager.playScoreSound(); 
        scoreDisplay.text = "Score: " + Score;
    }

    private void resetScoreInternal()
    {
        Score = 0;
        scoreDisplay.text = "Score: " + Score;
    }

    public void resetScore()
    {
        scoreKeeper.resetScoreInternal(); 
    }

}
