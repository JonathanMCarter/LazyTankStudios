using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Created by: Toby Wishart
 * Last edit: 27/10/19
 */
public class ItemInfoHandler : A
{
    //Lists to store the names and descriptions
    public List<string> itemNames = new List<string>();
    public List<string> itemDescriptions = new List<string>();

    Text n, d;
    Inventory i, i2, activeI;
    CanvasGroup cg;

    void Start()
    {
        n = transform.GetChild(0).GetComponent<Text>();
        d = transform.GetChild(1).GetComponent<Text>();
        i = GameObject.FindGameObjectWithTag("Inv").GetComponent<Inventory>();
        i2 = GameObject.Find("VendorInventory").GetComponent<Inventory>();
        cg = GetComponent<CanvasGroup>();
        activeI = i;
    }

    void Update()
    {
        if (activeI.VendorMode)
        {
            activeI = i.active ? i : i2;
        }

        //Check if player has item if not set the text to blank
        int ID = activeI.getSelectedID();
        if (activeI.hasItem(ID))
        {
            //Set text using the lists and the selected ID as the index, set to blank if null which will hide the panel
            n.text = itemNames[ID] != null ? itemNames[ID] : "";
            if (activeI.VendorMode) n.text += " " + activeI.Slots[ID].Value + "G";
          //  if (i.canItemRecieveXP(ID)) n.text += " Lv." + i.getLevel(ID);
            d.text = itemDescriptions[ID] != null ? itemDescriptions[ID] : "";
        } else
        {
            n.text = "";
            d.text = "";
        }
        //Hide panel if inventory isn't open or name is blank
        cg.alpha = activeI.isOpen ? n.text.Length == 0 ? 0 : 1 : 0;
    }
}
