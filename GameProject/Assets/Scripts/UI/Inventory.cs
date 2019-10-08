using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    private struct StoredItem {
        public Item item;
        public int quantity;
        public void increaseQuantity(int quantity)
        {
            this.quantity += quantity;
        }

        public StoredItem(Item item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
    }

    List<StoredItem> inventoryItems;

    void Start()
    {
        inventoryItems = new List<StoredItem>();
    }

    public Item getItemByIndex(int index)
    {
        return inventoryItems[index].item;
    }

    //Add item to list, increases quantity if it already contains the item
    public bool addItem(Item item, int quantity)
    {
        if (containsItem(item)) {
            if (inventoryItems[getItemIndex(item)].quantity < item.stackSize)
            {
                inventoryItems[getItemIndex(item)].increaseQuantity(1);
                return true;
            } else
            {
                return false;
            }
        } else
        {
            inventoryItems.Add(new StoredItem(item, quantity));
            return true;
        }
    }

    //Check if item is contained in list
    public bool containsItem(Item item)
    {
        bool contains = false;

        inventoryItems.ForEach(si =>
        {
            if (!contains)
                contains = si.item.Equals(item);
        });
        return contains;
    }
    
    //Returns the index in the list for an item, returns -1 if invalid
    public int getItemIndex(Item item)
    {
        if (containsItem(item))
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].item.Equals(item)) return inventoryItems[i].quantity;
            }
        }
        return -1;
    }

    void Update()
    {
        
    }
}
