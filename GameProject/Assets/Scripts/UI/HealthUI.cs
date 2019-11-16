using UnityEngine;
using UnityEngine.UI;

/*
 * Script for UI Hearts
 * 
 *  changes the hearts in the UI depending on how much health the player has
 *
 * Owner: Andreas Kraemer
 * Last Edit : 12/10/19
 * 
 * Also Edited by : Tony Parsons
 * Last Edit:  20/10/2019
 * 
 * Reason: optimisation made what was in update into it's own function so it's not running all the time
 * 
 * */


public class HealthUI : A
{
    public int currentHealth, maxHealth;
    public Image[] hearts;
    public Sprite fullHeart, emptyHeart;


    public void ShowHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            //sets hearts to max health
            hearts[i].enabled = (i < maxHealth);

            //switches heart sprites depending on the current health
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }
}
