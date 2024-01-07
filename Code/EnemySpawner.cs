using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// Keeps track of the last time an object was spawned
    /// </summary>
    public float SpawnTimeTracker = 0;

    /// <summary>
    /// Object to spawn
    /// </summary>
    public GameObject Prefab;

    /// <summary>
    /// Seconds between spawn
    /// </summary>
    public float SpawnInterval = 5;

    /// <summary>
    /// Keeps track of global time
    /// </summary>
    public float currentTime = 0;

    /// <summary>
    /// Keeps track of whether enemies should be spawned 
    /// </summary>
    public bool spawn = true;

    /// <summary>
    /// Maximum number of enemies that can be spawned
    /// </summary>
    public int maxEnemies = 35;

    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            currentTime += Time.deltaTime;

            if (currentTime > SpawnTimeTracker && !tooManyEnemies())
            {
                SpawnTimeTracker += SpawnInterval;
                Instantiate(Prefab);
                Vector2 spawnLocation = RandomPoint();
                Prefab.transform.position = spawnLocation;

                if (SpawnInterval > 0.6)
                    SpawnInterval -= 0.2f;
            }
        }
    }

    /// <summary>
    /// Checks if there are less than the maximum number of enemies 
    /// </summary>
    bool tooManyEnemies()
    {
        if (FindObjectsOfType<Enemy>().Length > maxEnemies)
            return true;
        return false; 
    }

    /// <summary>
    /// Stops enemies from spawning
    /// </summary>
    public void stopSpawning()
    {
        spawn = false;
    }

    /// <summary>
    /// Continuously creates a random point (up to 50 times) that is not occupied by
    /// another object
    /// </summary>
    Vector2 RandomPoint()
    {
        var position = new Vector2(RandomX(), -2.252f);
        for (var i = 0; i < 50 && !FreeSpace(position); i++)
            position = new Vector2(RandomX(), -2.252f);
        return position;
    }

    /// <summary>
    /// Picks a random x value (either left or right of the player) that is outside the
    /// view of the camera but within the barriers in scene 
    /// </summary>
    float RandomX()
    {
        float LeftBarX = GameObject.Find("Left Barrier").transform.position.x + 2;
        float RightBarX = GameObject.Find("Right Barrier").transform.position.x - 2;

        float cameraLeftSide = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float cameraRightSide = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0, 0)).x;

        float xLeft = Random.Range(LeftBarX, cameraLeftSide); 
        float xRight = Random.Range(cameraRightSide, RightBarX);

        float LorR = Random.Range(-1, 1);

        if (LorR < 0)
        {
            if (cameraLeftSide < LeftBarX)
                return xRight;
            return xLeft; 
        }
        else 
        {
            if (cameraRightSide > RightBarX)
                return xLeft;
            return xRight;
        }
    }

    /// <summary>
    /// Checks if there the point given is occupied by another object
    /// </summary>
    public static bool FreeSpace(Vector2 position)
    {
        return Physics2D.CircleCast(position, 2, Vector2.up, 0);
    }

    /// <summary>
    /// Resumes the spawning of Enemies
    /// </summary>
    public void restartSpawning()
    {
        spawn = true;
        SpawnInterval = 5;
        currentTime = 0;
        SpawnTimeTracker = 0; 
    }
}
