using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool canMove; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Throwable") 
        {

            //StartCoroutine(RigidbodyType());
            canMove = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(transform.right * 50);

        }
        if (collision.gameObject.tag=="Player") 
        {
            rb.bodyType = RigidbodyType2D.Static;

        }
    }

    IEnumerator RigidbodyType()
    {
        canMove = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(transform.right * 5);
        yield return new WaitForSeconds(0.5f);
        rb.bodyType = RigidbodyType2D.Kinematic;
        canMove = true;


    }
}
