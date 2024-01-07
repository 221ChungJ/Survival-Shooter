using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Animator component
    /// </summary>
    private Animator animator;  

    /// <summary>
    /// RigidBody2D component
    /// </summary>
    private Rigidbody2D rigidBody;

    /// <summary>
    /// Prefab for the bullets
    /// </summary>
    public GameObject BulletPrefab;

    /// <summary>
    /// How fast the player accelerate left and right
    /// </summary>
    public float acceleration = 10;

    /// <summary>
    /// Bullet speed
    /// </summary>
    public float BulletVelocity = 10;

    /// <summary>
    /// Direction the player is facing (1 for right, -1 for left)
    /// </summary>
    private int direction = 1;

    /// <summary>
    /// Cooldown time for player to attack
    /// </summary>
    private float attackCD = 0.15f;

    /// <summary>
    /// Keeps track of time
    /// </summary>
    private float timer = Mathf.Infinity;

    /// <summary>
    /// Keeps track of if the player is alive
    /// </summary>
    private bool alive = true;


    // Start is called before the first frame update
    void Start()
    {
        alive = true;  
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            Movement();
            playerDirection();
            animationHandling();
            Attack();
            checkDirection();

            timer += Time.deltaTime;
        }
    }

    private void animationHandling()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        animator.SetBool("run", horizontalInput != 0);
    }

    private void checkDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput > 0.01f)
        {
            direction = 1;
        }
        else if (horizontalInput < -0.01f)
        {
            direction = -1;
        }
    }

    private void playerDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (direction == 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (direction == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 horizontalSpeed = new Vector2(horizontalInput * acceleration, rigidBody.velocity.y);
        rigidBody.velocity = horizontalSpeed;

    }

    private void Attack()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput == 0 && Input.GetKeyDown("space") && timer > attackCD)
        {
            timer = 0; 
            Fire();
        } 
    }

    private void Fire()
    {
        animator.SetTrigger("attack");

        Vector3 bulletPosition = new Vector3(rigidBody.position.x, rigidBody.position.y - 0.7f, 0);
        
        if (direction == 1)
        {
            createBulletRight(bulletPosition); 
        }
        else if (direction == -1)
        {
            createBulletLeft(bulletPosition);  
        }
            
    }

    void createBulletRight(Vector3 bulletPosition)
    {
        GameObject bullet = Instantiate(BulletPrefab, bulletPosition + transform.right, Quaternion.identity);
        bullet.GetComponent<SpriteRenderer>().flipX = false;
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * BulletVelocity;
    }

    void createBulletLeft(Vector3 bulletPosition)
    {
        GameObject bullet = Instantiate(BulletPrefab, bulletPosition - transform.right, Quaternion.identity);
        bullet.GetComponent<SpriteRenderer>().flipX = true;
        bullet.GetComponent<Rigidbody2D>().velocity = -transform.right * BulletVelocity;
    }

    public void dead()
    {
        alive = false; 
        animator.SetBool("run", false);
        animator.SetBool("dead", true);
    }

    public void revive()
    {
        alive = true;
        transform.position = new Vector3(0, transform.position.y, 0);
        animator.SetBool("dead", false);
    }

}
