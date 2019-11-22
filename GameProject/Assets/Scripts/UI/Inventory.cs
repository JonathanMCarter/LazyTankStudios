using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : A
{
    public int current;
    static int Coins = 0;
    public List<Sprite> icons;
    public List<int> items;
    public Text t;
    public CanvasGroup cg;
    public Image i;

    void Update(){
        if (items.Count > 0){
            i.sprite = icons[items[current]];
            cg.alpha = 1;
        }
        else cg.alpha = 0;
    }
    void OnLevelWasLoaded(int level){
        addCoins(0);
    }
    public void change(){
        current = current < items.Count-1 ? current + 1 : 0;
    }
    public int getCoins(){
        return Coins;
    }
    public void addCoins(int coins){
        Coins += coins;
        t.text = Coins + "";
    }
}