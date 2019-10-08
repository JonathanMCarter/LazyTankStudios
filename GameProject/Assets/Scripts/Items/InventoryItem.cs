using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    private Item itemData;

    public InvPOC Inv;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Hero"))
        {
            Debug.Log(collision.gameObject.name);

            if (Input.GetButton("Submit"))
            {
                Inv.Add(itemData);
                Inv.ItemGO = gameObject;
                gameObject.SetActive(false);
            }
        }
    }
}
