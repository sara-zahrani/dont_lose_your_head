using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueLevel1 : MonoBehaviour
{

    public TextMeshProUGUI mTextDisplay;

    public int mIndex;
    public float mTypingSpeed;
    public GameObject mContinueButton;
    public GameObject mPlayer;


    Walk mPlayerWalk;
    Jump mPlayerJump;
    BoneManager mPlayerBones;

    bool mIsPlayerMoving;

    public string[] mSentences;
    private bool mIsPlayerWithGhost;

    // Start is called before the first frame update
    void Start()
    {
        mPlayerWalk = mPlayer.GetComponent<Walk>();
        mPlayerJump = mPlayer.GetComponent<Jump>();
        mPlayerBones = mPlayer.GetComponent<BoneManager>();

        mPlayerWalk.enabled = false;
        mPlayerJump.enabled = false;
        mPlayerBones.enabled = false;

        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {


        if (mIndex == 0 || mIndex == 1)
        {
            mPlayerWalk.enabled = false;
            mPlayerJump.enabled = false;
            mPlayerBones.enabled = false;

            if (mTextDisplay.text == mSentences[mIndex])
            {
                mContinueButton.SetActive(true);
            }
        }
        else if (mIndex == 2)
        {

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
                NextSentence();
            }

        }
        else if (mIndex == 3 && mIsPlayerMoving && mIsPlayerWithGhost)
        {

            mIsPlayerMoving = false;

            mPlayerWalk.enabled = false;
            mPlayerJump.enabled = false;

            if (mTextDisplay.text == mSentences[mIndex])
            {
                mContinueButton.SetActive(true);
            }
        }

    }


    IEnumerator Type()
    {
        foreach (char letter in mSentences[mIndex].ToCharArray())
        {
            mTextDisplay.text += letter;
            yield return new WaitForSeconds(mTypingSpeed);
        }
    }

    public void NextSentence()
    {
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mIsPlayerWithGhost = true;
        }
    }
}
