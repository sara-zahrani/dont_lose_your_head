using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSkeleton : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Horizontal")) 
        {
            anim.SetBool("CanWalk", true);
        }
        else if (Input.GetButton("Horizontal")) 
        {
            anim.SetBool("CanWalk", true);
        }
        else if (Input.GetButtonUp("Horizontal")) 
        {
            anim.SetBool("CanWalk",false);
        }
    }
}
