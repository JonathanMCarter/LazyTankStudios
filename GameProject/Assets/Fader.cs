using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * Temp script added by LC
 * 
 * 
 * 
 * 
 * 
 * */

public class Fader : MonoBehaviour
{
    private Image myImage;
    private Text myText;
   
    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponentInChildren<Image>();
        myText = GetComponentInChildren<Text>();
        StartCoroutine(Fade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Fade()
    {
        for (int i = 0; i < 10; i++)
        {


            UpdateColor();
            yield return new WaitForSeconds(0.1f);
        }
    }

    void UpdateColor()
    {
        Color mycolor = myImage.color;
        mycolor.a += 0.1f;
        myImage.color = mycolor;

        mycolor = myText.color;
        mycolor.a += 0.1f;
        myText.color = mycolor;
    }
}
