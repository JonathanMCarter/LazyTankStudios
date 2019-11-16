using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class IntroFade : A
{

    public Text titleText, companyText;
    public Image mapImage;
    public string loadLevel;

    IEnumerator Start()
    {
        titleText.canvasRenderer.SetAlpha(0.0f);
        companyText.canvasRenderer.SetAlpha(0.0f);
        mapImage.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        yield return new WaitForSeconds(4f);
        FadeOut();
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(loadLevel);
    }

    void FadeIn()
    {
        titleText.CrossFadeAlpha(1.0f, 1.5f, false);
        companyText.CrossFadeAlpha(1.0f, 1.5f, false);
        mapImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut()
    {
        titleText.CrossFadeAlpha(1.0f, 1.5f, false);
        companyText.CrossFadeAlpha(1.0f, 1.5f, false);
        mapImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }
}
