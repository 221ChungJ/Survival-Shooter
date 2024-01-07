using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public ScoreKeeper scoreKeeper;

    private bool GameOver; 

    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        GameOver = false;
    }

    /// <summary>
    /// Idles all enemies, sets "GameOver" to true, calls the showGameOverScreen() method
    /// </summary>
    public void endGame()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            enemy.Idle();
        }
        GameOver = true; 
         showGameOverScreen();
    }

    /// <summary>
    /// Makes the game over screen visible to the user
    /// </summary>
    void showGameOverScreen()
    {
        FindObjectOfType<GameOverScreen>().ShowScreen();
    }

    /// <summary>
    /// Makes the game over screen visible to the user
    /// </summary>
    void hideGameOverScreen()
    {
        FindObjectOfType<GameOverScreen>().HideScreen();
    }

    /// <summary>
    /// Resets the score, deletes all the enemies, calls the hideGameOverScreen() method,
    /// calls the revivePlayer() method, and calls restartSpawning() method on the Enemy 
    /// Spawner object
    /// </summary>
    void restartGame()
    {
        GameOver = false;
        scoreKeeper.resetScore(); 
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            enemy.ByeBye();
        }
        hideGameOverScreen();
        revivePlayer();
        FindObjectOfType<EnemySpawner>().restartSpawning();
    }

    /// <summary>
    /// Calls the revive() method on the player
    /// </summary>
    void revivePlayer()
    {
        FindObjectOfType<Player>().revive();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver && Input.GetKeyDown(KeyCode.R))
        {
            restartGame(); 
        }
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }
}
