using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBarking : MonoBehaviour
{
    DogSounds sound;
    private void Start()
    {
        sound = transform.parent.gameObject.GetComponent<DogSounds>();
        print("DogBarking 1112");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag== "Player") 
        {
            print("DogBarking");
            sound.NoiseBarking();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sound.StopBarking();
        }
    }
}
