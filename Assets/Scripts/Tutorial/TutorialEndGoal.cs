using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEndGoal : MonoBehaviour
{

    public GameObject mPanel;
    public GameObject mPlayer;
    public DialogueManager mDialogueManager;
    private SoundManager mSoundManager;
    //BoneManager mBoneManager;
    private bool mPlaySound;
    public BoneManager mBoneMaager;

    private void Start()
    {
        mSoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        //mBoneManager = mPlayer.GetComponent<BoneManager>();
        mPlaySound = true;
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {

        //Debug.Log("PLAYER OUTSIDE" + collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {

            //Debug.Log("PLAYER" + collision.gameObject.name);
            mBoneMaager = collision.transform.root.gameObject.GetComponent<BoneManager>();

            //Debug.Log("thrown bones: " + mBoneMaager.mThrownBones.Count);
            //Debug.Log("Open gate: " + mDialogueManager.mOpenGate);

            if (mDialogueManager.mOpenGate && mBoneMaager.mThrownBones.Count == 0)
            {
                if (mPlaySound)
                {
                    mPlaySound = false;
                    mSoundManager.GateUnLockSound();
                    //Debug.Log("UNNNNLOCKED");
                }

                StartCoroutine(DisplayLoadingPanel());
            }
            else
            {
                //Debug.Log("LOCKED");
                if (mPlaySound)
                {

                    mPlaySound = false;
                    mSoundManager.GateLoackSound();
                }

            }

        }


    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mPlaySound = true;
        }
    }

    IEnumerator DisplayLoadingPanel()
    {
        mPanel.SetActive(true);
        mPlayer.SetActive(false);
        yield return new WaitForSeconds(7f);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
