using UnityEngine.UI;
public class LanguageSwitch:A{
Dropdown d;
void Awake(){
d=G<Dropdown>();
if(!LanguageSelect.isEnglish)d.value=1;}
public void SwitchLanguage(){
switch(d.value){
case 0: LanguageSelect.isEnglish=true;
break;
case 1: LanguageSelect.isEnglish=false;
break;}}}