using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public ScoreKeeper scoreKeeper;

    /// <summary>
    /// The speed the enemy can move
    /// </summary>
    public float acceleration = 1;

    /// <summary>
    /// Animator component
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Transform from the player object
    /// Used to find the player's position
    /// </summary>
    private Transform player;

    /// <summary>
    /// RigidBody2D component
    /// </summary>
    private Rigidbody2D rigidBody;

    /// <summary>
    /// Keeps track of if the zombie has been killed (haha but they can't be bc they're already dead)
    /// </summary>
    private bool alive = true; 


    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>().transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            moveToPlayer();
            changeDirection();
        }
    }

    /// <summary>
    /// Moves the enemy towards the player
    /// </summary>
    private void moveToPlayer()
    {
        Vector2 playerDirection = new Vector2(player.position.x - transform.position.x, 0).normalized;
        Vector2 horizontalSpeed = new Vector2(playerDirection.x * acceleration, 0);
        rigidBody.velocity = horizontalSpeed;
    }

    /// <summary>
    /// Changes position based on whether the player is to the left or right of the enemy
    /// </summary>
    private void changeDirection()
    {
        if (transform.position.x > player.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (transform.position.x < player.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    /// <summary>
    /// Plays the animation to die and Invokes the "dieAndScore" method when colliding with a bullet
    /// Stops the spwaning of enemies, kills the player, and ends the game when colliding with a player
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.GetComponent<Bullet>())
        {
            alive = false;
            animator.SetTrigger("dies");
            Invoke("dieAndScore", 0.9f);        
        }
        if (collision.collider.gameObject.GetComponent<Player>() && alive)
        {
            FindObjectOfType<EnemySpawner>().stopSpawning();
            animator.SetTrigger("attack");
            Invoke("killPlayer", 0.2f);
            Invoke("endGame", 0.4f);
        }
    }

    /// <summary>
    /// Calls the "dead" method on the player object
    /// </summary>
    void killPlayer()
    {
        FindObjectOfType<Player>().dead();
    }


    /// <summary>
    /// Calls the end game method on the EventController object
    /// </summary>
    void endGame()
    {
        FindObjectOfType<EventController>().endGame();
    }

    /// <summary>
    /// Plays the ide animation and doesn't allow any other movement by changing the "alive" variable
    /// </summary>
    public void Idle()
    {
        alive = false;
        animator.SetTrigger("idle");
    }

    /// <summary>
    /// Calls ScorePoints on the ScoreKeeper object
    /// </summary>
    private void score()
    {
        scoreKeeper.ScorePoints(1);
    }

    /// <summary>
    /// Destroys the enemy object
    /// </summary>
    public void ByeBye()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Destroys the enemy object and increases the score
    /// </summary>
    public void dieAndScore()
    {
        score();
        ByeBye(); 
    }
}
