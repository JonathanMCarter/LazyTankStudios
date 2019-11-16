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
        int ID = i.getSelectedID();
        if (i.hasItem(ID))
        {
            //Set text using the lists and the selected ID as the index, set to blank if null which will hide the panel
            n.text = itemNames[ID] != null ? itemNames[ID] : "";
            if (i.canItemRecieveXP(ID)) n.text += " Lv." + i.getLevel(ID);
            d.text = itemDescriptions[ID] != null ? itemDescriptions[ID] : "";
        } else
        {
            n.text = "";
            d.text = "";
        }
        //Hide panel if inventory isn't open or name is blank
        cg.alpha = i.isOpen ? n.text.Length == 0 ? 0 : 1 : 0;
    }
}
