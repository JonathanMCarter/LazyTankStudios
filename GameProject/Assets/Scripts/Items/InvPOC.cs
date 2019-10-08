using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    You shouldn't be here.....
    If something throws an error that stops you working then let me know...


    Inv Activate Script - For POC
    -=-=-=-=-=-=-=-=-=-=-=-

    Made by: Jonathan Carter
    Last Edited By: Jonathan Carter
    Date Edited Last: 8/10/19

*/

public class InvPOC : MonoBehaviour
{

    public GameObject Inv;

    public bool HasItem;

    public Text ItemName;
    public Text ItemType;
    public Image Icon;
    public Button Drop;

    public GameObject ItemGO;

    private void Update()
    {
        if (HasItem)
        {
            Drop.gameObject.SetActive(true);
        }
        else
        {
            if (Drop.gameObject.activeInHierarchy)
            {
                Drop.gameObject.SetActive(false);
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!Inv.activeInHierarchy)
            {
                Inv.SetActive(true);
            }
            else
            {
                Inv.SetActive(false);
            }
        }
    }

    public void Add(Item I)
    {
        ItemName.text = I.itemName;
        ItemType.text = I.type.ToString();
        Icon.sprite = I.icon;
        HasItem = true;
    }

    public void Add(ItemOneHanded I)
    {
        ItemName.text = I.itemName;
        ItemType.text = I.type.ToString();
        Icon.sprite = I.icon;
        HasItem = true;
    }

    public void DropItem()
    {
        ItemGO.transform.position = GameObject.Find("Hero").transform.position;
        ItemGO.SetActive(true);
        ItemName.text = "";
        ItemType.text = "";
        Icon.sprite = null;
        HasItem = false;
    }
}
