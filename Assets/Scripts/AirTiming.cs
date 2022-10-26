using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTiming : MonoBehaviour
{
    bool playAir;

    public GameObject[] airs; 
    // Start is called before the first frame update
    void Start()
    {
        playAir = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //private void FixedUpdate()
    //{
    //    //if (playAir)
    //    //{

    //    //}
    //    //else
    //    //{

    //    //}
    //    for (int i=0; i<airs.Length;i++) 
    //    {
    //        StartCoroutine(TimeAir(i));
    //    }


    //}
    //IEnumerator TimeAir(int i)
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(2);
    //        //playAir = !playAir;
    //        airs[i].gameObject.SetActive(false);
    //        yield return new WaitForSeconds(2);
    //        airs[i].gameObject.SetActive(true);


    //    }
    //}

    //private void TimeForAir(float time) 
    //{

    //}
}
