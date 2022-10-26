using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip clip;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>(); 
    }

    public void Noise() 
    {
        audio.PlayOneShot(clip);
    }

}
