using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public GameObject textBox;
    public GameObject nameBox;
    public Image portrait1;
    public Image portrait2;
    public bool portrait1Active = false;
    public Color portrait2Percent;

    public Color cutDown;
    public Color cutUp;
    public string[] textBoxArray;
    public string[] nameBoxArray;
    public int dialogueIndex = 0;
    public int nameIndex = 0; 

    // Start is called before the first frame update
    void Start()
    {
        textBox.GetComponent<Text>().text = "Luther the dog and Jackson the cat meet up to discuss their plans!";
        nameBox.GetComponent<Text>().text = "";
        cutDown = portrait1.color;
        cutUp = portrait1.color;
        cutDown.a = .25f;
 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && dialogueIndex < textBoxArray.Length && nameIndex < nameBoxArray.Length)
        {
            textBox.GetComponent<Text>().text = textBoxArray[dialogueIndex];
            nameBox.GetComponent<Text>().text = nameBoxArray[nameIndex];
            dialogueIndex++;
            nameIndex++;
            portrait1Active = !portrait1Active;
            ChangeOpacity();

        }
    }

    void ChangeOpacity()
    {
        if (portrait1Active)
        {
            portrait2.color = cutDown;
            portrait1.color = cutUp;
        }
        else
        {
            portrait1.color = cutDown;
            portrait2.color = cutUp; ;
        }
    }
}
