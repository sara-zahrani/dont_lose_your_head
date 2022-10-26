using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public float mSpeed;
    public bool mCanMove;

    private Rigidbody2D mRigidB;
    Transform mPlayerBody;

    Animator anim;
    public bool canAnimate;

    private bool mFacingRight = true;
    //*new
    private SoundManager sound;
    private bool playsound;
    private bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        mRigidB = GetComponent<Rigidbody2D>();
        mPlayerBody = transform.GetChild(0).transform;
        // TODO animation
        anim = gameObject.GetComponent<Animator>();
        canAnimate = true;
        //new
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        playsound = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mCanMove)
        {
            StartWalking();
        }
    }


    private void StartWalking()
    {


        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * mSpeed, 0);
      
        mRigidB.velocity = new Vector2(movement.x, mRigidB.velocity.y);
        //new
        if (Input.GetButton("Horizontal") && playsound && isGround)
        {
            StartCoroutine(WalkSound());
        }
        ///////////////////////////////////////
        // TODO fix animation
        if (canAnimate) 
        {
            anim.enabled = true;
            if (movement.x != 0)
            {
                anim.SetBool("CanWalk", true);
            }

            if (movement.x == 0)
            {
                anim.SetBool("CanWalk", false);
            }

        }
        else  
        {
            StartCoroutine(BackIdel());
        }
       
        if (movement.x > 0 && !mFacingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && mFacingRight)
        {
            Flip();
        }
        
        
    }

    private void Flip()
    {
        mFacingRight = !mFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    IEnumerator BackIdel()
    {
        anim.SetBool("CanWalk", false);
        yield return new WaitForSeconds(0.2f);
        anim.enabled = false;
    }
    //*new
    IEnumerator WalkSound()
    {

        playsound = false;
        GameObject body = transform.GetChild(1).gameObject;

        if (body.transform.GetChild(4).childCount == 1 && body.transform.GetChild(5).childCount == 1)
        {
            sound.WalkSound();
        }

        else
            sound.CrawlingSound();

        yield return new WaitForSeconds(0.3f);
        playsound = true;


    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) 
        {
            print("play walk");
            isGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isGround = false;
        }
    }
}
