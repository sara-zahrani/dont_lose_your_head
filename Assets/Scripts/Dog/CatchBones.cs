using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBones : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
      

        if (other.gameObject.tag=="Throwable" )
        {
            transform.root.GetChild(0).gameObject.SetActive(false);
            transform.root.GetChild(1).gameObject.SetActive(false);
            print("set active false done");

        }
    }
}
