using UnityEngine;
using UnityEngine.UI;

/**
 * Owner: Toby Wishart
 * Last edit: 10/11/19
 * Reason: Add option for levelling
 */

public class InvSlot : A
{

    public bool hasItem = false;
    public int ID = -1;
    public int quantity = 0;
    //0 = not equipped 1 = a equipped 2 = b equipped
    public int equipped = 0;
    //[HideInInspector]
    public bool selected = false;
    public int Value;
    Image img;
    //Set to true if item can level up
    public bool recievesXP = false;

    GameObject i;
    Text q, e;

    //void Awake()
    //{
       
    //}

    void Start()
    {
         //if (ID < 0) gameObject.SetActive(false); //moved here from Awake function to reduce code. If game breaks now pop this back into awake
        img = GetComponent<Image>();
        i = transform.GetChild(0).gameObject;
        q = transform.GetChild(1).gameObject.GetComponent<Text>();
        e = transform.GetChild(2).gameObject.GetComponent<Text>();
        updateIcon();
        selected = false;
    }

    //Call this when changing the variables to updAte the icon
    public void updateIcon()
    {
        i.SetActive(hasItem);
        q.text = quantity > 0 ? quantity+"" : "";
        e.text = equipped > 0 ? equipped == 1 ? "A" : "B" : "";
    }

    public void equip(int equip)
    {
        equipped = equip;
        updateIcon();
    }

    void Update()
    {
        img.color = selected ? new Color(0,0,1) : new Color(0.35f, 0.35f, 0.35f);
    }

    //public void UnselectedColourApplied()
    //{
    //    img.color =new Color(0.35f, 0.35f, 0.35f);
    //}
    ////Andreas edit
    //public void SelectedColourApplied()
    //{
    //   img.color =new Color(0,0,1);
    //}
}
