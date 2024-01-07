using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().enabled = false; 
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(FindObjectOfType<Player>().transform.position.x, transform.position.y, 0);
    }

    public void ShowScreen()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void HideScreen()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
}
