using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvSlot : MonoBehaviour
{

    public bool hasItem = false;
    public int quantity = 0;
    public bool equipped = false;
    [HideInInspector]
    public bool selected = false;

    GameObject i, e;
    Text q;

    void Start()
    {
        i = transform.GetChild(0).gameObject;
        q = transform.GetChild(1).gameObject.GetComponent<Text>();
        e = transform.GetChild(2).gameObject;
        updateIcon();
        selected = false;
    }

    //Call this when changing the variables to updAte the icon
    public void updateIcon()
    {
        i.SetActive(hasItem);
        q.text = quantity > 0 ? quantity.ToString() : "";
        e.SetActive(equipped);
    }

    public void equip(bool equip)
    {
        equipped = equip;
        updateIcon();
    }

    void Update()
    {
        GetComponent<Image>().color = selected ? new Color(1,1,1) : new Color(0.35f, 0.35f, 0.35f);
    }

    public void UnselectedColourApplied()
    {
        GetComponent<Image>().color =new Color(0.35f, 0.35f, 0.35f);

    }
}
