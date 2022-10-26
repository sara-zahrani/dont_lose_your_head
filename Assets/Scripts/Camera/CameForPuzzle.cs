using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameForPuzzle : MonoBehaviour
{
    public GameObject cam, close;
    public GameObject TheName;
    public Memory memory;
    public GameObject text;
    private Animator anim;
    private bool once;
    public GameObject player;
    public EndGoal endGoal;
    public GameObject ghost;

    private bool startConv;
    // Start is called before the first frame update

    // Update is called once per frame

    private void Start()
    {
        once = true;
        ghost.SetActive(false);
    }

    private void Update()
    {
        if (startConv) 
        {

        }
    }
    //private void OnTriggerStay2D(Collider2D other)
    //{
      

    //        if (other.gameObject.tag == "Player")
    //        {
    //            GameObject body = other.transform.root.GetChild(1).gameObject;


    //            if (body.transform.GetChild(2).childCount == 1
    //                && body.transform.GetChild(3).childCount == 1
    //                && body.transform.GetChild(4).childCount == 1
    //                && body.transform.GetChild(5).childCount == 1)
    //            {

    //                player = other.transform.root.gameObject;
    //            cam.SetActive(true);
    //            TheName.SetActive(true);
    //            memory.ColorBoneAsMemory("Toes");
    //            memory.ColorBoneAsMemory("Heal");
    //            //memory.ColorBoneAsMemory("LeftToes");
    //            //memory.ColorBoneAsMemory("LeftHeal");
    //            if (once)
    //            {
    //                text.GetComponent<Text>().color = Color.cyan;
    //                endGoal.isComplete = true;
    //                ghost.SetActive(true);


    //            }


    //            }
   

    //        }



    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject body = collision.transform.root.GetChild(1).gameObject;

            

            if (body.transform.GetChild(2).childCount == 1
                && body.transform.GetChild(3).childCount == 1
                && body.transform.GetChild(4).childCount == 1
                && body.transform.GetChild(5).childCount == 1)
            {

                player = collision.transform.root.gameObject;
                cam.SetActive(true);
                TheName.SetActive(true);
                //memory.ColorBoneAsMemory("Toes");
                //memory.ColorBoneAsMemory("Heal");


                player.GetComponent<Jump>().enabled=false;
                player.GetComponent<Walk>().enabled = false;
                player.GetComponent<BoneManager>().enabled = false;
                player.GetComponent<Animator>().SetBool("CanWalk",false);
                startConv = true;


                if (once)
                {
                    text.GetComponent<Text>().color = Color.cyan;
                    endGoal.isComplete = true;
                    ghost.SetActive(true);


                }


            }


        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cam.SetActive(false);
           // TheName.SetActive(false);
            ghost.SetActive(false);

        }
    }


}
