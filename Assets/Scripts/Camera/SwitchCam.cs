using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    [SerializeField] GameObject  puzzleCam;

    private void Awake()
    {
      puzzleCam.SetActive(false);  
    }
    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player") 
        {
            puzzleCam.SetActive(true);
            
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            puzzleCam.SetActive(false);
        }
    }
}
