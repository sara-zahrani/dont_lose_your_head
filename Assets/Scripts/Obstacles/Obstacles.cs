using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Obstacles : MonoBehaviour
{
    public Transform spawn;
    public float timeSpawn;

    private SoundManager sound;
    private bool once;

    private void Start()
    {
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        once = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {

            if (once)
                sound.FallInHoleSound();

            once = false;

            StartCoroutine(SpawnTime(other));
             //other.transform.root.transform.position = spawn.position;

        }

    }
    
    IEnumerator SpawnTime(Collider2D other) 
    {
        yield return new WaitForSeconds(timeSpawn);
        other.transform.root.transform.position = spawn.position;
        once = true;
    }


}
