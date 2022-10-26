using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    SpriteRenderer[] mSprites;

    void Start()
    {
        mSprites = GetComponentsInChildren<SpriteRenderer>();
        //ColorBoneAsMemory("LeftToes");
    }

    public void ColorBoneAsMemory(string boneName)
    {
        foreach (var item in mSprites)
        {
            if (item.transform.name.Contains(boneName))
            {
                item.color = Color.cyan;
            }

        }
    }

}
