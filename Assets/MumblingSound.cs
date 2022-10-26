using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MumblingSound : MonoBehaviour
{
    public AudioClip G, S;
    public AudioSource audioSource;    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartSkeletonMumblngSound() 
    {
        audioSource.clip = S;
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }


    public void GhostMumblingSound() 
    {
        audioSource.clip = G;
        audioSource.Play();
    }

}
