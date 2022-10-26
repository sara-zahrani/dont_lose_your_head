using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour
{
    private GameObject head, head_not, player;
    public int force;
    private Transform spawn;
    private bool ONCE;

    //new
    private SoundManager sound;

    private void Start()
    {
        spawn = GameObject.Find("Spawn1").transform;
        ONCE = true;
        //*new
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player"&& ONCE)
        {


            ONCE = false;
            player = collision.transform.root.gameObject;

            //new
            sound.HeadFallingSound();
            /////////////////

            head = player.transform.GetChild(1).GetChild(1).gameObject;
            head_not = player.transform.GetChild(4).gameObject;

            GameObject copy_head_not =player.transform.GetChild(4).gameObject;
            //new code

            GameObject new_head=Instantiate(copy_head_not, copy_head_not.transform.position, Quaternion.identity);
            new_head.transform.SetParent(player.transform);
            new_head.transform.localScale =new Vector2(copy_head_not.transform.localScale.x, copy_head_not.transform.localScale.y);

            //
            head.gameObject.SetActive(false);
            head_not.gameObject.SetActive(true);
            head_not.transform.SetParent(null);
            player.GetComponent<Jump>().enabled = false;
            StartCoroutine(Spawn());
            //Rigidbody2D rb = head_not.gameObject.GetComponent<Rigidbody2D>();
            //rb.AddForce(transform.right * force);
        }
    }

    IEnumerator Spawn() 
    {
        yield return new WaitForSeconds(2);
        player.transform.position = spawn.position;
        Destroy(head_not.gameObject);
        head.gameObject.SetActive(true);
        player.GetComponent<Jump>().enabled = true;
        ONCE = true;

    }
}
