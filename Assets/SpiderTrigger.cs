using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderTrigger : MonoBehaviour
{
    public GameObject spider;
    // Start is called before the first frame update
    void Start()
    {
        spider.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag== "Player")
        {

            spider.SetActive(true);
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spider.SetActive(false);
        }
    }
}
