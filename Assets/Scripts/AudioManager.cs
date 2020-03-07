using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip doorAudio;
    public AudioClip grappleReloaded;
    public AudioClip grappleShot;
    public AudioClip introClip;


    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

   
}
