using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSounds : MonoBehaviour
{
    public AudioClip[] clips;
    AudioSource Hunt;
    AudioSource Attack;

    public int barking = 0
               , bite = 1
               , hurt = 2
               , happy = 3;

    // Start is called before the first frame update
    void Start()
    {
        Hunt = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        Attack = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
    }
    public void NoiseBarking() 
    {
        Hunt.clip = clips[barking];
        Hunt.Play();
    }
    public void StopBarking() 
    {
        Hunt.Stop();

    }
    public void NoiseAttack()
    {
        //Attack.PlayOneShot(clips[bite]);
        Attack.clip= clips[bite];
        Attack.Play();
    }
    public void StopAttack()
    {
        Attack.Stop();

    }

}
