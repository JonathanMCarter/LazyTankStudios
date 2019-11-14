using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILanguageController : MonoBehaviour
{
    Text[] textObjects;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    public void Awake()
    {
       try{ if(FindObjectOfType<PlayerMovement>().optionsMenu!=null)FindObjectOfType<PlayerMovement>().optionsMenu.SetActive(true);}
       catch{}
        textObjects=FindObjectsOfType<Text>();   
        foreach(Text text in textObjects)
        {
            text.enabled=false;
            if(text.CompareTag("TextEnglish")&&LanguageSelect.isEnglish)
            {
                text.enabled=true;   
            }
            else if(text.CompareTag("TextGerman")&&!LanguageSelect.isEnglish)
            {
                text.enabled=true; 
            }
            else if (text.CompareTag("TextConstant"))
            {
                text.enabled=true; 
            }
            try
            {
            if(GameObject.FindGameObjectWithTag("Settings")!=null)GameObject.FindGameObjectWithTag("Settings").SetActive(false);
            if(FindObjectOfType<PlayerMovement>().optionsMenu!=null)FindObjectOfType<PlayerMovement>().optionsMenu.SetActive(false);
            }
            catch{}
        }
    }
    public void UpdateLanguage()
    {
        Awake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
