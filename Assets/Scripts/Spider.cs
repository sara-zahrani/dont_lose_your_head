using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public GameObject player;
    private SoundManager sound;

    private AudioSource audioSource;
    private bool once; 

    // Start is called before the first frame update
    void Start()
    {
        once = true;
        sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        audioSource = GetComponent<AudioSource>();
        SpiderSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Throwable" && once)
        {
            once = false;
            StartCoroutine(killSpider());
           
        }
    }
    IEnumerator killSpider() 
    {
        StopSpiderSound();
        player.GetComponent<Jump>().enabled = false;
        player.GetComponent<Walk>().enabled = false;
        player.GetComponent<BoneManager>().enabled = false;



        transform.GetChild(0).gameObject.SetActive(true);
        //*new
        sound.SpiderDieSound();
        ////////////
        yield return new WaitForSeconds(1.3f);
        player.GetComponent<Jump>().enabled = true;
        player.GetComponent<Walk>().enabled = true;
        player.GetComponent<BoneManager>().enabled = true;

        Destroy(transform.root.gameObject);
    }

    public void SpiderSound()
    {
        audioSource.clip = sound.mHazardSpiderAppearSound;
        audioSource.Play();
    }

    public void StopSpiderSound()
    {
        audioSource.Stop();
    }
}
