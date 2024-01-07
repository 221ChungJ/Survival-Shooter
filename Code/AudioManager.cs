using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Audio source component
    /// </summary>
    public AudioSource source;

    /// <summary>
    /// Audio clip component
    /// </summary>
    public AudioClip scoreSound;

    /// <summary>
    /// Plays the sound that is made when scored
    /// </summary>
    public void playScoreSound()
    {
        source.PlayOneShot(scoreSound); 
    }
}
