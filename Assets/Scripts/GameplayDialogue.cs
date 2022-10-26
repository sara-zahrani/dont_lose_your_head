using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayDialogue : MonoBehaviour
{
    public TextMeshProUGUI mTextDisplay;

    public int mIndex;
    public float mTypingSpeed;
    public GameObject mContinueButton;

    public string[] mSentences;
    public GameObject cam, close;
    public GameObject TheName;
    public Memory memory;
    public GameObject text;
    private Animator anim;
    public GameObject player;
    public EndGoal endGoal;
    public GameObject ghost;
    public GameObject out_ghost;


    private bool startConv;
    private bool once;

    //*new
    public MumblingSound sound;
    public ButtonSound button;
    private SoundManager mSoundManager;
    private AudioSource audioSource;
    public GameObject grivPartical;

    // Start is called before the first frame update
    void Start()
    {
        ghost.SetActive(false);
        startConv = false;
        once = false;
        out_ghost.SetActive(false);
        mSoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        audioSource = GetComponent<AudioSource>();



    }

    // Update is called once per frame
    void Update()
    {
        if (startConv) 
        {
            if ((mIndex >= 0 && mIndex <= 5))
            {
                if (mTextDisplay.text == mSentences[mIndex])
                {
                    mContinueButton.SetActive(true);
                }
            }
            else if (mIndex == 6) 
            {

                if (mTextDisplay.text == mSentences[mIndex] )
                {
                    mContinueButton.SetActive(true);
                    once = true;
                }
            }

        }

    }


    IEnumerator Type()
    {

        if (mIndex == 0 || mIndex == 2 || mIndex == 4 || mIndex == 6)
        {
            sound.GhostMumblingSound();
            ghost.GetComponent<Animator>().SetBool("IsTalking", true);
        }
        else if (mIndex == 1 || mIndex == 3 || mIndex == 5)
        {

            sound.StartSkeletonMumblngSound();
        }


        foreach (char letter in mSentences[mIndex].ToCharArray())
        {
            mTextDisplay.text += letter;
           
            yield return new WaitForSeconds(mTypingSpeed);
        }

        if (mIndex == 5) 
        {
            mSoundManager.MemoriesSound();
            text.GetComponent<Text>().color = Color.cyan;
            endGoal.isComplete = true;
            memory.ColorBoneAsMemory("Toes");
            memory.ColorBoneAsMemory("Heal");
        }
        sound.StopSound();
        ghost.GetComponent<Animator>().SetBool("IsTalking", false);

    }


    public void NextSentence()
    {
        button.Noise();
        if (once) 
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            player.GetComponent<Jump>().enabled = true;
            player.GetComponent<Walk>().enabled = true;
            player.GetComponent<BoneManager>().enabled = true;
            player.GetComponent<Animator>().SetBool("CanWalk", true);
            cam.SetActive(false);
            StartCoroutine(GohstDisappear());
            out_ghost.SetActive(true);
        }
        mContinueButton.SetActive(false);

        if (mIndex < mSentences.Length - 1)
        {
            mIndex++;
            mTextDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            mTextDisplay.text = "";
        }
 

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject body = collision.transform.root.GetChild(1).gameObject;

            //mSoundManager.CameraSound();

            if (body.transform.GetChild(2).childCount == 1
                && body.transform.GetChild(3).childCount == 1
                && body.transform.GetChild(4).childCount == 1
                && body.transform.GetChild(5).childCount == 1)
            {

                player = collision.transform.root.gameObject;
                cam.SetActive(true);
                TheName.SetActive(true);



                player.GetComponent<Jump>().enabled = false;
                player.GetComponent<Walk>().enabled = false;
                player.GetComponent<BoneManager>().enabled = false;
                player.GetComponent<Animator>().SetBool("CanWalk", false);
                startConv = true;


                //mSoundManager.GhostDisappearSound();

                StartCoroutine(GhostAppear());
                //*new
                grivPartical.SetActive(false);
            }


        }
    }

    IEnumerator GhostAppear()
    {
        yield return new WaitForSeconds(1);
        ghost.SetActive(true);
        audioSource.clip = mSoundManager.mGhostDisappearSound;
        audioSource.Play();
        StartCoroutine(Type());
    }

    IEnumerator GohstDisappear() 
    {
        ghost.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);

        //mSoundManager.GhostDisappearSound();

        ghost.SetActive(false);
        audioSource.clip = mSoundManager.mGhostDisappearSound;
        audioSource.Play();

    }
  
}
