using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    /// <summary>
    /// Camera object
    /// </summary>
    private new Transform camera;

    /// <summary>
    /// Starting position of the camera
    /// </summary>
    private Vector3 cameraStartPosition;

    /// <summary>
    /// Background material
    /// </summary>
    private Material[] material;

    /// <summary>
    /// Distance from camera
    /// </summary>
    private float distance;

    /// <summary>
    /// Backgrounds
    /// </summary>
    private GameObject[] backgrounds;

    /// <summary>
    /// Background speeds
    /// </summary>
    private float[] backgroundSpeed;

    /// <summary>
    /// Furthest background
    /// </summary>
    private float furthestBack;

    /// <summary>
    /// Speed controller
    /// </summary>
    [Range(0f, 0.5f)]
    public float parallaxSpeed = 0.2f; 



    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform; 
        cameraStartPosition = camera.position;

        int numBackgrounds = transform.childCount;
        material = new Material[numBackgrounds];
        backgroundSpeed = new float[numBackgrounds];
        backgrounds = new GameObject[numBackgrounds];

        for (int i = 0; i < numBackgrounds; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            material[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        backgroundSpeedCalculator(numBackgrounds); 
    }

    /// <summary>
    /// Calculates the speed at which each background element moves
    /// </summary>
    void backgroundSpeedCalculator(int numBackgrounds)
    {
        for (int i = 0; i < numBackgrounds; i++)
        {
            if((backgrounds[i].transform.position.z - camera.position.z) > furthestBack)
            {
                furthestBack = backgrounds[i].transform.position.z - camera.position.z; 
            }
        }

        for (int i = 0; i < numBackgrounds; i++)
        {
            backgroundSpeed[i] = 1 - (backgrounds[i].transform.position.z - camera.position.z) / furthestBack;
        }
    }

    /// <summary>
    /// With each update move each background element the calculated amount
    /// </summary>
    private void LateUpdate()
    {
        distance = camera.position.x - cameraStartPosition.x;
        transform.position = new Vector2(camera.position.x, transform.position.y); 

        for(int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backgroundSpeed[i] * parallaxSpeed;
            material[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed); 
        }
    }
}
