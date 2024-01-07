using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// Destroys the bullet once it passes out of the player's view
    /// </summary>
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Called when the bullet makes contact with another object
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.gameObject.GetComponent<Bullet>())
        {
            Destroy(gameObject);
        }
    }
}
