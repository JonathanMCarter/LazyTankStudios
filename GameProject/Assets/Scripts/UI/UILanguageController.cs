using UnityEngine;
using UnityEngine.UI;

public class UILanguageController : A
{
    Text[] textObjects;
    // Start is called before the first frame update


    void Awake()
    {
        UpdateLanguage();
    }
    public void UpdateLanguage()
    {
        try{ if(FindObjectOfType<PlayerMovement>().Menu!=null)FindObjectOfType<PlayerMovement>().Menu.SetActive(true);}
       catch{}
        textObjects=FindObjectsOfType<Text>();   
        foreach(Text text in textObjects)
        {
            text.enabled=true;

            if((text.CompareTag("TextEnglish")&&!LanguageSelect.isEnglish)||(text.CompareTag("TextGerman")&&LanguageSelect.isEnglish))text.enabled=false; 


            try
            {
            if(GameObject.FindGameObjectWithTag("Settings")!=null)GameObject.FindGameObjectWithTag("Settings").SetActive(false);
            if(FindObjectOfType<PlayerMovement>().Menu!=null)FindObjectOfType<PlayerMovement>().Menu.SetActive(false);
            }
            catch{}
        }
    }

}
