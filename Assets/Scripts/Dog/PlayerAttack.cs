using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // throw arm or legs bones
    // retrieve bone

    public Stack<GameObject> mArms = new Stack<GameObject>();
    public Stack<GameObject> mLegs = new Stack<GameObject>();


    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform bone = transform.GetChild(i);

            if (bone.tag == "Arm")
            {
                mArms.Push(bone.gameObject);
            }
            else if (bone.tag == "Leg")
            {
                mLegs.Push(bone.gameObject);
            }
        }

    }


    // should the player choose wich bone to throw? Or throw bones in order?
    // in what order should the player retrieve the bones?


    private void Update()
    {
        
        
    }

    // Bone status
    // Thrown
    // Attached
    // Selected


    // WASD to move left & right
    // Shift to switch between attack & movement modes
    // WASD
    // W => select arms
    // S => select legs
    // D => Aim


}
