using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Created by: Toby Wishart
 * Last edit: 27/10/19
 */
public class ItemInfoHandler : MonoBehaviour
{
    //Lists to store the names and descriptions
    public List<string> itemNames = new List<string>();
    public List<string> itemDescriptions = new List<string>();

    Text n, d;
    Inventory i;
    CanvasGroup cg;

    void Start()
    {
        n = transform.GetChild(0).GetComponent<Text>();
        d = transform.GetChild(1).GetComponent<Text>();
        i = GameObject.FindGameObjectWithTag("Inv").GetComponent<Inventory>();
        cg = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        //Check if player has item if not set the text to blank
        if (i.hasItem(i.selected))
        {
            //Set text using the lists and the selected ID as the index, set to blank if null which will hide the panel
            n.text = itemNames[i.selected] != null ? itemNames[i.selected] : "";
            if (i.canItemRecieveXP(i.selected)) n.text += " Lv." + i.getLevel(i.selected);
            d.text = itemDescriptions[i.selected] != null ? itemDescriptions[i.selected] : "";
        } else
        {
            n.text = "";
            d.text = "";
        }
        //Hide panel if inventory isn't open or name is blank
        cg.alpha = i.isOpen ? n.text.Length == 0 ? 0 : 1 : 0;
    }
}
