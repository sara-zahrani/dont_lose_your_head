using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BoneMechanicMode
{
    Throw,
    Retrieve
};

public enum BoneSelectionType
{
    None,
    Arm,
    Leg
};

public class BoneManager : MonoBehaviour
{
    private SoundManager mSoundManager;
    public BoneMechanicMode mBoneMechanicMode;
    public BoneSelectionType mBoneSelectedType;
    public Bone mSelectedBoneToThrow;
    public GameObject mRetrieveParticalEffect;
    public float mForceBeforeAttachingLeg;
    public bool mIsTutorial;

    private Stack<Bone> mArms = new Stack<Bone>();
    private Stack<Bone> mLegs = new Stack<Bone>();
    public List<Bone> mThrownBones = new List<Bone>();


    private bool mCanThrow =  false ;
    private bool mCanRetrieve =  false;

    public Vector2 mThrowDirection;


    private Walk mPlayerWalk;
    private void Start()
    {
        mSoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

        mBoneMechanicMode = BoneMechanicMode.Throw;
        LoadBones(transform);
        mRetrieveParticalEffect.SetActive(false);
        EnableAllBoneColliders(false);
        mPlayerWalk = GetComponent<Walk>();
    }


    private void Update()
    {
        if(!mIsTutorial)
        {
            SwitchBetweenModes();

            if (mBoneMechanicMode == BoneMechanicMode.Throw)
            {
                mRetrieveParticalEffect.SetActive(false);
                ActivateBoneSelection();
                ActivateAiming();
                ActivateThrow();
            }

            else if (mBoneMechanicMode == BoneMechanicMode.Retrieve)
            {
                mRetrieveParticalEffect.SetActive(true);
                ActivateRetrieve();
            }


            if (mLegs.Count == 0)
            {
                Debug.Log("NO LEGS. cliders on.");
                EnableAllBoneColliders(true);
                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            }
            else
            {
                Debug.Log("THERE're LEGS. cliders off.");
                EnableAllBoneColliders(false);
                gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            }
        }
        else
        {

        }


        if(Input.GetMouseButtonDown(1))
        {
            UnselectBone();
        }

    }

    // Switch between throw and retrive modes 
    public void SwitchBetweenModes()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            

            if (mBoneMechanicMode == BoneMechanicMode.Throw && mThrownBones.Count != 0)
            {
                mSoundManager.ShiftSound();

                mBoneMechanicMode = BoneMechanicMode.Retrieve;
            }
            else if (mBoneMechanicMode == BoneMechanicMode.Retrieve && (mArms.Count > 0 || mLegs.Count > 0))
            {
                mBoneMechanicMode = BoneMechanicMode.Throw;
            }
        }

        if (mBoneMechanicMode == BoneMechanicMode.Retrieve && mThrownBones.Count == 0)
        {
            mSoundManager.ShiftSound();
            mBoneMechanicMode = BoneMechanicMode.Throw;
        }
        else if (mBoneMechanicMode == BoneMechanicMode.Throw && mArms.Count == 0 && mLegs.Count == 0)
        {
            mSoundManager.ShiftSound();
            mBoneMechanicMode = BoneMechanicMode.Retrieve;
        }
    }


    public void ActivateBoneSelection()
    {
        // Select upper or lower bone
        if (Input.GetKeyDown(KeyCode.W)) // select Arm
        {
            mPlayerWalk.canAnimate = false;
            SelectBone(mArms, BoneSelectionType.Arm);
        }
        else if (Input.GetKeyDown(KeyCode.S))  // select Leg
        {
            mPlayerWalk.canAnimate = false;
            SelectBone(mLegs, BoneSelectionType.Leg);
        }
    }

    public void ActivateAiming()
    {

        // Aim at target
        if (mSelectedBoneToThrow != null && mBoneSelectedType != BoneSelectionType.None)
        {
            mThrowDirection = mSelectedBoneToThrow.Aim();
            mCanThrow = true;
        }
        else
        {
            mCanThrow = false;
        }
    }

    public void ActivateThrow()
    {

        // throw the bone at target
        if (Input.GetMouseButtonDown(0) && mCanThrow)
        {            
            // TODO when the player can't shoot an object go back to aiming >> mThrowingDirectis is zero
            mSelectedBoneToThrow.ThrowBone(mThrowDirection);

            if (mBoneSelectedType == BoneSelectionType.Arm)
            {
                ThrowSelectedBone(mArms, "Arms");
            }
            else if(mBoneSelectedType == BoneSelectionType.Leg)
            {
                ThrowSelectedBone(mLegs, "Legs");
            }

            UnselectBone();
            mCanThrow = false;
            mCanRetrieve = true;
        }
    }

    public void ActivateRetrieve()
    {
        if (mThrownBones.Count == 0 || mSelectedBoneToThrow != null)
        {
            UnselectBone();
            mCanRetrieve = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            RetrievSelectedBone(mArms, "Arm");
        }
        // Retrieve Leg
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if(mLegs.Count == 0)
            {
                Debug.Log("LEG IS BACK GOTTA JUMP");
                transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * mForceBeforeAttachingLeg);
                StartCoroutine("AttachLegAfterSeconds");
            }
            else if(mLegs.Count > 0)
            {
                RetrievSelectedBone(mLegs, "Leg");
            }
        }
    }

    public void LoadBones(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if(child.tag == "Arm")
            {
                mArms.Push(child.gameObject.GetComponent<Bone>());
            }
            else if (child.tag == "Leg")
            {
                mLegs.Push(child.gameObject.GetComponent<Bone>());
            }

            if (child.childCount > 0 && (child.tag != "Leg" || child.tag != "Arm"))
            {
                LoadBones(child);
            }
        }
    }


    public void UnselectBone()
    {
        if(mSelectedBoneToThrow != null)
        {
           mSelectedBoneToThrow.SetBoneColor(mSelectedBoneToThrow.GetBoneOriginalColor());
           mSelectedBoneToThrow.transform.rotation =  mSelectedBoneToThrow.GetBoneOriginalRotation();
           mSelectedBoneToThrow.EnableParticleLine(false);
           mPlayerWalk.canAnimate = true;
        }

        mBoneSelectedType = BoneSelectionType.None;
        mSelectedBoneToThrow = null;

    }


    public void SelectBone(Stack<Bone> bones, BoneSelectionType boneType)
    {

        UnselectBone();

        if (bones.Count != 0)
        {
            mSelectedBoneToThrow = bones.Peek();
            mBoneSelectedType = boneType;
            mSelectedBoneToThrow.SelectBone();
        }

        mPlayerWalk.canAnimate = false;
    }


    public void ThrowSelectedBone(Stack<Bone> bones, string boneName = "bone")
    {
        if (bones.Count != 0)
        {
            mThrownBones.Add(bones.Pop());
            mBoneSelectedType = BoneSelectionType.None;
        }
        else
        {
            Debug.LogWarning("There're no " + boneName + " in the Stack to throw.");
        }

    }


    public void RetrievSelectedBone(Stack<Bone> bones, string tag)
    {
        if (mThrownBones.Count > 0)
        {
            Bone thrownArmBone = mThrownBones.Find(x => x.transform.tag == tag);

            if (thrownArmBone != null)
            {
                bones.Push(thrownArmBone);
                mThrownBones.Remove(thrownArmBone);

            }
            else
            {
                Debug.LogWarning("There's no thrown " + tag + "s found to retrieve.");
            }

        }

        if (bones.Count > 0)
        {
            mSelectedBoneToThrow = bones.Peek();
            mSelectedBoneToThrow.AttachBone();
            UnselectBone();
        }
        else
        {
            Debug.LogWarning(tag + " Stack is empty");
        }
    }

    IEnumerator AttachLegAfterSeconds()
    {
        yield return new WaitForSeconds(0.5f);
        RetrievSelectedBone(mLegs, "Leg");
    }


    public void EnableAllBoneColliders(bool enable)
    {
        PolygonCollider2D[] colliders = transform.GetComponentsInChildren<PolygonCollider2D>();

        foreach (var item in colliders)
        {
            item.enabled = enable;
        }
    }


    public void AdjustColliderAfterLegsThrow()
    {
        if (mLegs.Count == 0)
        {
            Debug.Log("NO LEGS. cliders on.");
            EnableAllBoneColliders(true);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

        }
        else
        {
            Debug.Log("THERE're LEGS. cliders off.");
            EnableAllBoneColliders(false);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }

}
