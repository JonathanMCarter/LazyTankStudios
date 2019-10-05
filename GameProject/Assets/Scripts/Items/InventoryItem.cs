using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    private Item itemData;

    string itemName;

    void Start()
    {
        this.itemName = itemData.itemName;
        name = itemName;
        transform.GetChild(0).GetComponent<Text>().text = itemName;
    }

    private void Update()
    {
        
    }
}
