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

    private void Awake()
    {
        gameObject.AddComponent<DoNotDes>();
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
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
         yield return new WaitForSeconds(4.5f);
        Destroy(gameObject);

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
