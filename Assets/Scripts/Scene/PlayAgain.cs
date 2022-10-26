using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayAgain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("BackToLevel");
    }
    IEnumerator BackToLevel() 
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene("Stage1 1");
    }

}
