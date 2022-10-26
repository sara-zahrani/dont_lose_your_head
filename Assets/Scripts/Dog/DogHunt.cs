using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogHunt : MonoBehaviour
{

    /*Most Important Of This Script */
    //1- Dod Follow Player
    //2-Dog Stop Follow Player

    bool canFollow;//To let dog follow player also To stop dog from moving and push the player , that make bad experiance in 2d.5 world 

    public GameObject player;

    GameObject dog;
    float speed;
    float distance;

    DogSounds sound;

    void Start()
    {
        canFollow = false;
        speed = 3f;
        dog = transform.parent.gameObject;
        //sound = transform.parent.gameObject.GetComponent<DogSounds>();

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(dog.transform.position, player.transform.position);

        if (canFollow)
        {
                       

            dog.transform.position = Vector2.MoveTowards(dog.transform.position,player.transform.position,speed*Time.deltaTime);
            StopFollow();


        }
   
 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.gameObject.tag == "Throwable") 
        {

            canFollow = true;
            //sound.NoiseBarking();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {

        //StopFollow(other);
        StopFollow();

    }


    private void OnTriggerExit2D(Collider2D other)
    {
       
        if (other.gameObject.tag == "Player")
        {
            canFollow = false;
            StopFollow();
        }

    }


    //public void StopFollow(Collider other) 
    //{
    //    if (distance < 2f)
    //    {
    //        if (other.gameObject.tag == "Player")
    //        {
    //            canFollow = false;
    //        }
    //    }
    //    else
    //    {
    //        canFollow = true;

    //    }

    //}
    public void StopFollow( )
    {
        if (distance < 2f)
        {
            if (player.gameObject.tag == "Player")
            {
                canFollow = false;
                //sound.StopBarking();
            }
        }
        else if (!canFollow) 
        {
            //sound.StopBarking();
        }
        else
        {
            canFollow = true;

        }
    }

 
}
