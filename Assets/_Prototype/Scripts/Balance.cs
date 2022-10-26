using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{

    public float mTargetRotation;
    public float mForce;

    Rigidbody2D mRigidBody;

    private void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        mRigidBody.MoveRotation(Mathf.LerpAngle(mRigidBody.rotation, mTargetRotation, mForce * Time.deltaTime));
    }
}
