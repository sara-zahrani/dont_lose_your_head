using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{

    public GameManager GM;
    public bool isComplete;
    private bool once;

    private SoundManager sound;
    private void Start()
    {
        isComplete = false;
        once = true;
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player" && isComplete) 
        {
            GameObject body = collision.transform.root.GetChild(1).gameObject;
            if (body.transform.GetChild(2).childCount == 1
                  && body.transform.GetChild(3).childCount == 1
                  && body.transform.GetChild(4).childCount == 1
                  && body.transform.GetChild(5).childCount == 1)
            {
                once = true;
                if (once) 
                {
                    once = false;
                    sound.GateUnLockSound();


                }

                GM.mCurrentGameStatus = GameStatus.EndLevel;
            }
            else 
            {
                if (once)
                    sound.GateLoackSound();
                once = false;
            }
        }
        else if (collision.gameObject.tag == "Player" && !isComplete)
        {
            if (once)
                sound.GateLoackSound();
            once = false;


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            once = true;
        }
    }
}
