using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemInfoHandler : A
{
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
        int ID = activeI.getSelectedID();
        if (activeI.hasItem(ID))
        {
            n.text = itemNames[ID] != null ? itemNames[ID] : "";
            if (activeI.VendorMode) n.text += " " + activeI.Slots[ID].Value + "G";
            d.text = itemDescriptions[ID] != null ? itemDescriptions[ID] : "";
        } else
        {
            n.text = "";
            d.text = "";
        }
        cg.alpha = activeI.isOpen ? n.text.Length == 0 ? 0 : 1 : 0;
    }
}
