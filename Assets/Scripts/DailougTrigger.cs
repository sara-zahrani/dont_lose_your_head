using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailougTrigger : MonoBehaviour
{
    public GameObject dailougCam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            dailougCam.SetActive (true);
            StartCoroutine (EndConve());
        }
    }

    IEnumerator EndConve()
    {
        yield return new WaitForSeconds(20f);
        dailougCam.SetActive(false);

    }

}
