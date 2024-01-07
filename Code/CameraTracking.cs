using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    /// <summary>
    /// Player's transform component
    /// </summary>
    public Transform player;

    /// <summary>
    /// Follows the x position of the player
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, 0, -10);
    }
}
