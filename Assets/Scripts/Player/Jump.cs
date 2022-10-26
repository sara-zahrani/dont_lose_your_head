using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public int jumpForce;
    private bool canJump;
    private bool landed;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private Rigidbody2D rb;
    bool grounded;
    //*new
    private SoundManager sound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //*new
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    //private void Update()
    //{

    //    //if (Input.GetButtonDown("Jump") && canJump == true)
    //    if (Input.GetButtonDown("Jump") && canJump == true)
    //    {
    //        Bounce();
    //    }

    //    //bool grounded = Physics.Linecast(new Vector3(transform.position.x, transform.position.y, transform.position.z), groundCheck.position, groundLayer);
    //    //Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z), groundCheck.position, Color.red);

    //    if (grounded == true)
    //    {
    //        canJump = true;
    //    }
    //    else
    //    {
    //        canJump = false;
    //    }
    //}

    //private void Bounce() 
    //{
    //    canJump = false;
    //    rb.AddForce(Vector2.up * jumpForce);
    //}


    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == 8)
    //    {
    //        grounded = true;
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == 8)
    //    {
    //        grounded = false;
    //    }
    //}
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            PlayerJump();
        }

        bool grounded = Physics2D.Linecast(new Vector3(transform.position.x, transform.position.y, transform.position.z), groundCheck.position, groundLayer);
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z), groundCheck.position, Color.red);

        if (grounded == true)
        {
            canJump = true;
            
        }
        else
        {
            canJump = false;
            landed = true;
        }
    }

    private void PlayerJump()
    {
        canJump = false;
        rb.AddForce(Vector3.up * jumpForce);
        //*new
        sound.JumpSound();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 8 && landed)
        {
            sound.JumpLandSound();
        }
    }
}
