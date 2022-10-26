using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private bool canLadder;
    private bool isClimp;
    private GameObject player;
    private int speed;
    //*new
    private SoundManager sound;
    private bool playSound;
    // Start is called before the first frame update
    void Start()
    {
        canLadder = false;
        isClimp = false;
        speed = 3;
        //*new
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        playSound = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump")) 
        {
            
            if (canLadder) 
            
            {

                isClimp = true;


            }
        }

        
    }

    private void FixedUpdate()
    {
        if (isClimp) 
        {
            Rigidbody2D rb = player.gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, 1*speed);
            //*new 
            if(playSound)
            StartCoroutine(LadderSound());
        }

   
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        GameObject body = collision.transform.root.GetChild(1).gameObject;
    //        if (body.transform.GetChild(0).childCount==1
    //            && body.transform.GetChild(1).childCount == 1
    //            && body.transform.GetChild(2).childCount == 1
    //            && body.transform.GetChild(3).childCount == 1)
    //        {

    //            canLadder = true;
    //            collision.gameObject.GetComponent<Jump>().enabled=false;

    //        }
    //        else 
    //        {
    //            print("miss pari");
    //        }
    //    }
    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.gameObject.layer = 0;

            GameObject body = collision.transform.root.GetChild(1).gameObject;
       
          
            if (body.transform.GetChild(2).childCount == 1
                && body.transform.GetChild(3).childCount == 1
                && body.transform.GetChild(4).childCount == 1
                && body.transform.GetChild(5).childCount == 1)
            {
                player = collision.transform.root.gameObject;
                canLadder = true;
                player.gameObject.GetComponent<Jump>().enabled = false;




            }
            else
            {
                print("miss pari");
                BackToJump();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player") 
        {
            transform.gameObject.layer = 8;
            if (canLadder) 
            {
                player.gameObject.GetComponent<Jump>().enabled = true;
                Rigidbody2D rb = player.gameObject.GetComponent<Rigidbody2D>();
                rb.gravityScale = 2.86f;


            }
            canLadder = false;
            isClimp = false;
            GameObject body = collision.transform.root.gameObject;
            print(collision.gameObject.name+" Ladder");
           ;

        }
    }

    private void BackToJump() 
    {
        if (canLadder)
        {
            player.gameObject.GetComponent<Jump>().enabled = true;
            Rigidbody2D rb = player.gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = 2.86f;


        }
        canLadder = false;
        isClimp = false;

    }

    //*new
    IEnumerator LadderSound() 
    {
        playSound = false;
        sound.LadderSound();
        yield return new WaitForSeconds(1f);
        playSound = true;

    }
}
