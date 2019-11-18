using UnityEngine;
using UnityEngine.UI;
public class HealthUI : A
{
    public int currentHealth, maxHealth;
    public Image[] hearts;
    public Sprite fullHeart, emptyHeart;
    public void ShowHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < maxHealth);
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }
}
