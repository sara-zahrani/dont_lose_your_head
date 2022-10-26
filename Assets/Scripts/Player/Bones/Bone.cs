using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bone : MonoBehaviour
{
    public float mThrowSpeed;
    public LayerMask mHitLayer;
    Animator anim;

    public List<Transform> mBones = new List<Transform>();
    public Transform mExtremityBone; // The last bone in the arm or leg, the hand or feet.
    public Transform mUpperBone;
    public GameObject mPlayer;

    Color mBoneOriginalColor;
    Quaternion mBoneOrginalRotation;
    PolygonCollider2D mUpperBoneCollider;
    ParticleSystem mParticleLine;

    SoundManager mSoundManager;


    private void Awake()
    {
        // Reference all the bones we need
        GetAllChildren(transform);
        mExtremityBone = mBones[mBones.Count - 1];
        mUpperBone = mBones[0];
        mBoneOrginalRotation = mUpperBone.rotation;
        mUpperBoneCollider = mUpperBone.GetComponent<PolygonCollider2D>();
        mUpperBoneCollider.isTrigger = true;
        mParticleLine = mExtremityBone.GetComponent<ParticleSystem>();

        mSoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void Start()
    {
        mBoneOriginalColor = GetComponentInChildren<SpriteRenderer>().color;
        mPlayer = transform.root.gameObject;
        EnableParticleLine(false);
    }

    public void SelectBone()
    {
        mSoundManager.SelectBoneSound();

        SetBoneColor(Color.red);
    }

    public void EnableParticleLine(bool enable)
    {
        mParticleLine.enableEmission = enable;
    }


    public Vector2 Aim()
    {
        Vector2 bonePosition = transform.position;
        Vector2 mousePoistion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePoistion - bonePosition;
        transform.right = direction;

        // TODO add sound while aiming??

        Debug.DrawRay(mExtremityBone.position, mExtremityBone.transform.up * -25f, Color.blue);
        EnableParticleLine(true);
        RaycastHit2D hit = Physics2D.Raycast(mExtremityBone.position, mExtremityBone.transform.up, -25f, mHitLayer);

        if (hit.collider != null)
        {
            Debug.Log("HIT: " + hit.collider.transform.name);
            Vector2 throwDirection = hit.collider.transform.position - (mUpperBone.transform.position);
            return throwDirection.normalized;
        }

        // TODO think about this more 
        return mExtremityBone.transform.up * -1;
    }

    public void ThrowBone(Vector2 throwDirection)
    {

        if (throwDirection == Vector2.zero)
        {
            // TODO here we should give feedback that what they player has pointed at
            // is not allowed to hit so the bone won't fly off.
            return;
        }

        EnableParticleLine(false);
        SetBoneColor(mBoneOriginalColor);
        mUpperBoneCollider.enabled = true;
        mUpperBoneCollider.isTrigger = false;

        mUpperBone.transform.parent = null;
        mUpperBone.gameObject.AddComponent<Rigidbody2D>();
        mPlayer.GetComponent<Rigidbody2D>().isKinematic = true;
        mUpperBone.GetComponent<Rigidbody2D>().AddForce(throwDirection * mThrowSpeed);
        StartCoroutine("PausePlayerRigidBody");

        // TODO add sound when the bones is deattached from the body
        mSoundManager.ThrowingSound();
        // TODO add sound when the bone hits the floor or another object
       
    }

    public void AttachBone()
    {
        Destroy(mUpperBone.GetComponent<Rigidbody2D>());

        mUpperBoneCollider.isTrigger = true;
        mUpperBoneCollider.enabled = false;


        // TODO make the bone go back to the player with nice and clear movment
        mUpperBone.transform.position = Vector2.MoveTowards(transform.position, transform.position, 0.1f);
        

        Vector3 direction = mPlayer.transform.localScale;
        float scale = 1;
        if(direction.x > 0)
        {
            scale = 1;
            //mUpperBone.rotation = mBoneOrginalRotation;
        }
        else if (direction.x < 0)
        {
            scale = -1;
            //mBoneOrginalRotation.z *= -1;
            //mUpperBone.rotation = mBoneOrginalRotation;

        }

        //Debug.Log("bone direction: " + transform.localScale.x);


        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        mUpperBone.transform.parent = transform;
        mUpperBone.localScale = new Vector3(scale, mUpperBone.localScale.y, mUpperBone.localScale.z);
        mUpperBone.rotation = mBoneOrginalRotation;


        //mUpperBone.GetComponent<PolygonCollider2D>().isTrigger = false;

        // TODO add sound when bone is attached back to the body
        mSoundManager.RetreavSound();
        // TODO add sound when the bone is moving back to the body 

    }

    public void SetBoneColor(Color color)
    {
        foreach (Transform bone in mBones)
        {
            if (bone.GetComponent<SpriteRenderer>().color == Color.cyan) 
            {
                continue;
            }
            bone.GetComponent<SpriteRenderer>().color = color;
        }
    }


    IEnumerator PausePlayerRigidBody()
    {
        yield return new WaitForSeconds(0.1f);

        mPlayer.GetComponent<Rigidbody2D>().isKinematic = false;

    }


    public void GetAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            mBones.Add(child);

            if (child.childCount > 0)
            {
                GetAllChildren(child);
            }
        }
    }


    public Color GetBoneOriginalColor()
    {
        return mBoneOriginalColor;
    }


    public Quaternion GetBoneOriginalRotation()
    {
        return mBoneOrginalRotation;
    }
}
