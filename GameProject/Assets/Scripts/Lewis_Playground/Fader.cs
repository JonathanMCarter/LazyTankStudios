using System.Collections;
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

public class Fader : A
{
    private Image myImage;
    private Text myText;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        myImage = GetComponentInChildren<Image>();
        myText = GetComponentInChildren<Text>();
        StartCoroutine(Fade());
    }

    // Update is called once per frame


    IEnumerator Fade()
    {
        for (int i = 0; i < 10; i++)
        {
            UpdateColor();
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject, 4.5f);

    }

    void UpdateColor()
    {
        Color c = myImage.color;
        c.a += 0.1f;
        myImage.color = c;

        c = myText.color;
        c.a += 0.1f;
        myText.color =c;
       
    }
}
