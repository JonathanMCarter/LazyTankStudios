using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSwitch:MonoBehaviour
{
    public void SwitchLanguage()
    {
        LanguageSelect.isEnglish=!LanguageSelect.isEnglish;
    }
}