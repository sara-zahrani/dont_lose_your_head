using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAttack : MonoBehaviour
{
    DogSounds sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = transform.parent.parent.gameObject.GetComponent<DogSounds>();
        print("Dog Attack "+transform.parent.parent.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player") 
        {
            sound.NoiseAttack();

        }


    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            sound.StopAttack();


        }

    }
}
