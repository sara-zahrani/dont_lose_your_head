using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI mTextDisplay;
    
    public int mIndex;
    public float mTypingSpeed;
    public GameObject mContinueButton;
    public GameObject mPlayer;
    public Image mInputImage1;
    public Image mInputImage2;
    public SpriteRenderer mGhost;


    Walk mPlayerWalk;
    Jump mPlayerJump;
    BoneManager mPlayerBones;

    bool mIsPlayerMoving;

    public string[] mSentences;
    public Sprite[] mInputSprites;
    private bool mIsPlayerWithGhost;
    private bool mGhostFlipped;
    public bool mIsAiming;
    public bool mIsThrowing;
    private bool mGoNext;
    private bool mIsBoneThrown;
    private bool mIsBoneRetrieved;
    private bool mGhostVisible;
    public bool mOpenGate;
    public MumblingSound mMumblingSound;
    SoundManager mSoundManager;

    //*NEW
    public ButtonSound button;
    private bool mStartDialogue;
    private bool mLearningControls;

    //public SpriteRenderer sprite;



    // Start is called before the first frame update
    void Start()
    {
        mSoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

        mPlayerWalk = mPlayer.GetComponent<Walk>();
        mPlayerJump = mPlayer.GetComponent<Jump>();
        mPlayerBones = mPlayer.GetComponent<BoneManager>();


        mPlayerWalk.enabled = false;
        mPlayerJump.enabled = false;
        mPlayerBones.enabled = false;

        mInputImage1.enabled = false;
        mInputImage2.enabled = false;

        //StartCoroutine(Type());
        StartCoroutine(StartDialogue());


    }

    // Update is called once per frame
    void Update()
    {
        if (mStartDialogue)
        {
            if (mIndex == 0)
            {
                mPlayerWalk.enabled = false;
                mPlayerJump.enabled = false;
                mPlayerBones.enabled = false;

                if (mTextDisplay.text == mSentences[mIndex])
                {
                    mContinueButton.SetActive(true);
                }
            }
            else if (mIndex == 1)
            {
                mInputImage1.enabled = true;
                mInputImage2.enabled = true;

                mPlayerWalk.enabled = true;
                mPlayerJump.enabled = true;

                if (Input.GetKeyDown(KeyCode.Space) ||
                    Input.GetAxis("Horizontal") > 0 ||
                    Input.GetAxis("Horizontal") < 0)
                {
                    Debug.Log("Player clicked on walk and jump");
                    mIsPlayerMoving = true;
                }

                if (mIsPlayerWithGhost)
                {

                    mInputImage1.enabled = false;
                    mInputImage2.enabled = false;

                    mPlayerWalk.enabled = false;
                    mPlayerJump.enabled = false;
                    GetComponent<BoxCollider2D>().isTrigger = true;
                    mPlayer.GetComponent<Animator>().SetBool("CanWalk", false);

                    NextSentence();
                }

            }
            else if (mIndex == 2 && !mIsPlayerMoving && mIsPlayerWithGhost)
            {
                mIsPlayerMoving = false;
                if (mTextDisplay.text == mSentences[mIndex])
                {
                    mContinueButton.SetActive(true);
                }
            }
            else if (mIndex == 3 && !mIsPlayerMoving && mIsPlayerWithGhost)
            {
                if (mTextDisplay.text == mSentences[mIndex])
                {
                    mGhostFlipped = true;
                    mContinueButton.SetActive(true);
                }

                //StartCoroutine(FlipGhost());
            }
            else if ((mIndex >= 4 && mIndex <= 13) && mGhostFlipped && !mIsPlayerMoving && mIsPlayerWithGhost)
            {
                if (mTextDisplay.text == mSentences[mIndex])
                {
                    mContinueButton.SetActive(true);
                }
            }
            else if (mIndex == 14)
            {
                mPlayer.GetComponent<Animator>().enabled = false;
                mPlayerBones.mIsTutorial = true;
                mPlayerBones.mBoneMechanicMode = BoneMechanicMode.Throw;
                mPlayerBones.ActivateBoneSelection();
                mPlayerBones.ActivateAiming();


                mInputImage2.sprite = mInputSprites[0];
                mInputImage1.sprite = mInputSprites[1];

                mInputImage1.enabled = true;
                mInputImage2.enabled = true;

                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                {
                    Debug.Log("player selected a bone");

                    mIsAiming = true;
                }

                if (mIsAiming && mPlayerBones.mSelectedBoneToThrow != null && mPlayerBones.mThrowDirection != Vector2.zero)
                {
                    mGoNext = true;
                    StartCoroutine(DelayNextSentence());
                    mIsAiming = false;

                }

            }
            else if (mIndex == 15)
            {
                mPlayer.GetComponent<Animator>().enabled = false;
                mPlayerBones.mIsTutorial = true;
                mPlayerBones.mBoneMechanicMode = BoneMechanicMode.Throw;
                mPlayerBones.ActivateBoneSelection();
                mPlayerBones.ActivateAiming();
                mPlayerBones.ActivateThrow();
                mPlayerBones.AdjustColliderAfterLegsThrow();

                mInputImage1.sprite = mInputSprites[2];

                mInputImage1.enabled = true;
                mInputImage2.enabled = false;


                if (Input.GetMouseButtonDown(0))
                {
                    mIsThrowing = true;
                }

                if (mIsThrowing && mPlayerBones.mThrownBones.Count > 0)
                {
                    mGoNext = true;
                    StartCoroutine(DelayNextSentence());
                    mIsThrowing = false;
                }
            }
            else if (mIndex == 16)
            {

                mPlayer.GetComponent<Animator>().enabled = false;
                mPlayerBones.enabled = true;
                mPlayerBones.mIsTutorial = false;

                mInputImage2.sprite = mInputSprites[3];
                mInputImage1.sprite = mInputSprites[0];

                mInputImage1.enabled = true;
                mInputImage2.enabled = true;


                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    mIsBoneRetrieved = true;

                }

                if (mPlayerBones.mThrownBones.Count == 0)
                {
                    Debug.Log("BONES ARE ATTACHED!!");
                    mGoNext = true;
                    StartCoroutine(DelayNextSentence());
                }
            }
            else if (mIndex == 17)
            {

                mInputImage1.enabled = false;
                mInputImage2.enabled = false;


                if (mTextDisplay.text == mSentences[mIndex])
                {
                    //StartCoroutine(GhostExit());
                    mGhostVisible = true;
                    mContinueButton.SetActive(true);
                }

            }

        }


    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(Type());
        mStartDialogue = true;
    }

    IEnumerator Type()
    {
        if (mIndex == 2 || mIndex == 4 || mIndex == 5
            || mIndex == 7 || mIndex == 9 || mIndex == 11)
        {
            mMumblingSound.GhostMumblingSound();
            mGhost.transform.parent.GetComponent<Animator>().SetBool("IsTalking", true);
        }
        else if(mIndex >= 13 && mIndex <= 17 && mLearningControls)
        {
            mLearningControls = false;
            mMumblingSound.GhostMumblingSound();
            mGhost.transform.parent.GetComponent<Animator>().SetBool("IsTalking", true);
        }
        else if (mIndex == 0 || mIndex == 1 || mIndex == 3
            || mIndex == 6 || mIndex == 8 || mIndex == 10 || mIndex == 12)
        {

            mMumblingSound.StartSkeletonMumblngSound();
        }

        foreach (char letter in mSentences[mIndex].ToCharArray())
        {
            mTextDisplay.text += letter;
            yield return new WaitForSeconds(mTypingSpeed);
        }

        mIsPlayerMoving = false;

        mGhost.transform.parent.GetComponent<Animator>().SetBool("IsTalking", false);
        mMumblingSound.StopSound();
        mLearningControls = true;

    }

    IEnumerator FlipGhost()
    {
        yield return new WaitForSeconds(1f);
        mGhost.flipX = true;
        mGhostFlipped = true;
    }


    IEnumerator GhostExit()
    {
        mGhost.transform.parent.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        mGhost.transform.parent.gameObject.SetActive(false);
        mPlayerWalk.enabled = true;
        mPlayerJump.enabled = true;
        mOpenGate = true;
        mSoundManager.GhostDisappearSound();
    }


    IEnumerator DelayNextSentence()
    {
        yield return new WaitForSeconds(4f);

        if(mGoNext)
        {
            NextSentence();
        }

        mGoNext = false;
       
    }

    public void NextSentence()
    {
        //*new
        if (mIndex!=1 && mIndex != 14 && mIndex != 15 && mIndex != 16) 
        {
            
            button.Noise();

        }
        
        ////////////////////////
        if(mGhostFlipped)
        {
            mGhost.flipX = true;
            //mGhostFlipped = false;
        }

        if(mGhostVisible)
        {
            StartCoroutine(GhostExit());
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
            mIsPlayerWithGhost = true;
        }
    }
}
