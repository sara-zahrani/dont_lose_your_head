using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSound : MonoBehaviour
{
    public SoundManager sound;
    private AudioSource audioSource;

    private void Start()
    {
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            PlayWindSound();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopWimdSound();
        }
    }
    public void PlayWindSound() 
    {
        audioSource.clip = sound.mHazardGreenWindSound;
        audioSource.Play();
    }

    public void StopWimdSound() 
    {
        audioSource.Stop();

    }
}
