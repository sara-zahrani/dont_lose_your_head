using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstecalsButton : MonoBehaviour
{
    public Animator breadg;
    bool redbutton = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        redbutton = !redbutton;
        if (collision.gameObject.tag=="Throwable"&&redbutton) 
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.down*-100);
            breadg.SetBool("CanOpen", true);
            
        }
        else if (collision.gameObject.tag == "Throwable" && !redbutton)
        {
            breadg.SetBool("CanOpen", false);
        }
    }
}
