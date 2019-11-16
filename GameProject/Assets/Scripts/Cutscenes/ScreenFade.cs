using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScreenFade : A
{

    public Text titleText;
    public Image mapImage;
    public string loadLevel;

    IEnumerator Start()
    {
        titleText.canvasRenderer.SetAlpha(0.0f);
        mapImage.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        yield return new WaitForSeconds(8f);
        FadeOut();
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(loadLevel);
    } 
    
    void FadeIn()
    {
        titleText.CrossFadeAlpha(1.0f, 1.5f, false);
        mapImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut()
    {
        titleText.CrossFadeAlpha(0.0f, 2.5f, false);
       mapImage.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
