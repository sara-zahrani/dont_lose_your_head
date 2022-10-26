using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWebs : MonoBehaviour
{
    bool isActive=false;
    GameObject spider;

    //ADD TO LOSE HEAD AND SPAEN
    private GameObject head, head_not, player;
    public int force;
    private Transform spawn;

    //*new
    private SoundManager sound;
    //private bool ONCE;
    private void Start()
    {
        spider = transform.GetChild(0).gameObject;
        //
        spawn = GameObject.Find("Spawn1").transform;
        //ONCE = true;
        //*new
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();


    }

    private void FixedUpdate()
    {
        //if (isActive) 
        //{
        //    StartCoroutine(Active());

        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other;
        isActive = true;

        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<Walk>().enabled = false;
            //collision.gameObject.GetComponent<Jump>().enabled = false;
            //GameObject skeletonInWebs = collision.transform.GetChild(4).gameObject;
            //skeletonInWebs.SetActive(true);

            if (collision.gameObject.name == "Head" || collision.gameObject.name == "Hip" || collision.gameObject.name == "Body") 
            {
                other = collision.transform.root.gameObject;
            }
            else 
            {
                other = collision.gameObject;

            }
            
            StartCoroutine(Action(other));

        }


        else if (collision.gameObject.tag == "Throwable")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }


    }

    IEnumerator Action(GameObject other) 
    {
        other.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        other.GetComponent<Walk>().enabled = false;
        other.GetComponent<Jump>().enabled = false;
        other.GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(0f);

        //*new 
        sound.HeadFallingSound();

        GameObject skeletonInWebs = other.transform.GetChild(3).gameObject;
        skeletonInWebs.SetActive(true);
        //GameObject head = other.transform.GetChild(1).GetChild(1).gameObject;
        //head.SetActive(false);
        //GameObject head_not= other.transform.GetChild(4).gameObject;
        //head_not.SetActive(true);

        //ONCE = false;
        player = other.transform.root.gameObject;

        head = player.transform.GetChild(1).GetChild(1).gameObject;
        head_not = player.transform.GetChild(4).gameObject;

        GameObject copy_head_not = player.transform.GetChild(4).gameObject;
        //new code

        GameObject new_head = Instantiate(copy_head_not, copy_head_not.transform.position, Quaternion.identity);
        new_head.transform.SetParent(player.transform);
        new_head.transform.localScale = new Vector2(copy_head_not.transform.localScale.x, copy_head_not.transform.localScale.y);

        ////
        head.gameObject.SetActive(false);
        head_not.gameObject.SetActive(true);
        head_not.transform.SetParent(null);
        player.GetComponent<Jump>().enabled = false;
        StartCoroutine(Spawn());

    }

    //IEnumerator Active() 
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    spider.SetActive(true);
    //    yield return new WaitForSeconds(0.2f);
    //    spider.GetComponent<SpriteRenderer>().enabled = true;

    //}

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2);
        player.transform.position = spawn.position;
        Destroy(head_not.gameObject);
        head.gameObject.SetActive(true);

        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        player.GetComponent<Jump>().enabled = true;
        player.GetComponent<Walk>().enabled = true;
        player.GetComponent<Animator>().enabled = true;

        GameObject skeletonInWebs = player.transform.GetChild(3).gameObject;
        skeletonInWebs.SetActive(false);
        //ONCE = true;

    }
}
