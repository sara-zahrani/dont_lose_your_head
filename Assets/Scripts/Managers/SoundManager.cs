using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip mBackgroundMusic;

    /*Skeleton*/
    public AudioClip mSkeletonWalkSound;
    public AudioClip mSkeletonCrawlingSound;
    public AudioClip mSkeletoHeadCutSound;
    public AudioClip mSkeletoHeadHittingGroundSound;
    public AudioClip mSkeletoSelectSound;
    public AudioClip mSkeletonThrowingSound;
    public AudioClip mSkeletonCancelThrowSound;
    public AudioClip mSkeletonShiftSound;
    public AudioClip mSkeletonRetreavSound;
    public AudioClip mSkeletonJumpSound;
    public AudioClip mSkeletonJumpLandSound;
    public AudioClip mSkeletonMumblingSound;
    public AudioClip mSkeletonMemoriesSound;


    /*Hazard*/
    public AudioClip mHazardHoleSound;
    public AudioClip mHazardSpiderAppearSound;
    public AudioClip mHazardSpiderDieSound;
    public AudioClip mHazardGreenWindSound;

    /*Ghost*/
    public AudioClip mGhostMumblingSound;
    public AudioClip mGhostDisappearSound;


    /*Gate*/
    public AudioClip mGateLoackSound;
    public AudioClip mGateUnloackSound;

    /*Scene and UI*/
    public AudioClip mToLevel1;

    /*Ladder*/
    public AudioClip mLadderSound;

    /*Camera*/
    public AudioClip mCameraSound;

    private AudioSource mAudioSource;

    private void Start()
    {
        mAudioSource = GetComponent<AudioSource>();
    }



    public void WalkSound()
    {
        mAudioSource.PlayOneShot(mSkeletonWalkSound);
    }

    public void CrawlingSound()
    {
        mAudioSource.PlayOneShot(mSkeletonCrawlingSound);
    }

    public void HeadFallingSound()
    {
        mAudioSource.PlayOneShot(mSkeletoHeadCutSound);
    }


    public void HeadHittingGroundSound()
    {
        mAudioSource.PlayOneShot(mSkeletoHeadHittingGroundSound);
    }

    public void ThrowingSound()
    {
        mAudioSource.PlayOneShot(mSkeletonThrowingSound);

    }

    public void SelectBoneSound()
    {
        mAudioSource.PlayOneShot(mSkeletoSelectSound);

    }

    public void CancelThrowSound()
    {
        mAudioSource.PlayOneShot(mSkeletonCancelThrowSound);
    }

    public void ShiftSound()
    {
        mAudioSource.PlayOneShot(mSkeletonShiftSound);

    }

    public void RetreavSound()
    {
        mAudioSource.PlayOneShot(mSkeletonRetreavSound);
    }

    public void JumpSound()
    {
        mAudioSource.PlayOneShot(mSkeletonJumpSound);
    }

    public void JumpLandSound()
    {
        mAudioSource.PlayOneShot(mSkeletonJumpLandSound);
    }

    public void SkeletomMumblingSound()
    {
        mAudioSource.PlayOneShot(mSkeletonMumblingSound);
    }

    public void MemoriesSound()
    {
        mAudioSource.PlayOneShot(mSkeletonMemoriesSound);
    }

    public void FallInHoleSound()
    {
        mAudioSource.PlayOneShot(mHazardHoleSound);
    }
    public void SpiderAppearSound()
    {
        mAudioSource.PlayOneShot(mHazardSpiderAppearSound);
    }
    public void SpiderDieSound()
    {
        mAudioSource.PlayOneShot(mHazardSpiderDieSound);
    }

    public void WindSound()
    {
        mAudioSource.PlayOneShot(mHazardGreenWindSound);
    }

    public void GhostMumblingSound()
    {
        mAudioSource.PlayOneShot(mGhostMumblingSound);
    }

    public void GhostDisappearSound()
    {
        mAudioSource.PlayOneShot(mGhostDisappearSound);
    }

    public void GateLoackSound()
    {
        mAudioSource.PlayOneShot(mGateLoackSound);
    }
    public void GateUnLockSound()
    {
        mAudioSource.PlayOneShot(mGateUnloackSound);
    }

    public void ToLevel1Sound()
    {
        mAudioSource.PlayOneShot(mToLevel1);
    }

    public void LadderSound()
    {
       mAudioSource.PlayOneShot(mLadderSound);

    }

    public void CameraSound()
    {
        mAudioSource.PlayOneShot(mCameraSound);

    }
    
}
