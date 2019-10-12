using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<hearts.Length;i++)
        {
            //sets hearts to max health
            hearts[i].enabled=i<maxHealth?true:false;

            //switches heart sprites depending on the current health
            hearts[i].sprite=i<currentHealth?fullHeart:emptyHeart;
        }
    }
}
