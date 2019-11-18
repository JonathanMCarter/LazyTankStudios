using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : A
{
    public bool[] items;
    public int equippedA = -1;
    public int equippedB = -1;
    public InvSlot[] Slots;
    public bool isOpen;
    [HideInInspector]
    public bool active = true;
    InputManager IM;
    static int Coins = 0;
    [HideInInspector]
    public const int COLUMN = 4;
    public const int ROWS = 3;
    public int selected = 0;
    int column = 4;
    public bool VendorMode = false;
    SoundPlayer audioManager;
    Text cointext;
    bool delayed = false;
    void Awake()
    {
        Slots = GetComponentsInChildren<InvSlot>();
        cointext = GameObject.Find("Coins").GetComponentInChildren<Text>();
    }
    void Start()
    {        
        addCoins(0);       
        items = new bool[Slots.Length];       
        IM = FindObjectOfType<InputManager>();
        audioManager=FindObjectOfType<SoundPlayer>();
    }
    void OnLevelWasLoaded(int level)
    {
        addCoins(0);
    }
    void Update()
    {
        GetComponent<CanvasGroup>().alpha = isOpen ? 1 : 0;
        if (isOpen && !delayed)
        {
            if (IM.Button_Menu())
            {
                open();
                StartCoroutine(delay());
            }
            if (IM.Button_A() && !VendorMode)
            {
                equipItem(getSelectedID(), !isEquipped(getSelectedID(), 1), 1);
                StartCoroutine(delay());
            }
            if (IM.Button_B() && !VendorMode)
            {
                equipItem(getSelectedID(), !isEquipped(getSelectedID(), 2), 2);
                StartCoroutine(delay());
            }
            if (active)
            {
                if (IM.X_Axis() == 1)
                {
                    if (selected < column - 1) selected++;
                    StartCoroutine(delay());
                }
                else if (IM.X_Axis() == -1)
                {
                    if (selected > column - 1 - ROWS) selected--;
                    StartCoroutine(delay());
                }
                if (IM.Y_Axis() == 1)
                {
                    if (selected - COLUMN >= 0)
                    {
                        selected -= COLUMN;
                        column -= COLUMN;
                    }
                    StartCoroutine(delay());
                }
                else if (IM.Y_Axis() == -1)
                {
                    if (selected + COLUMN < items.Length)
                    {
                        selected += COLUMN;
                        column += COLUMN;
                    }
                    StartCoroutine(delay());
                }
            }
            for (int i = 0; i < items.Length; i++)
            {
                InvSlot slot = transform.GetChild(i).GetComponent<InvSlot>();
                slot.selected = (i == selected);
            }
        }
    }
    public void addItem(int id, int quantity, bool remove)
    {
        items[id] = !remove;
        Slots[id].hasItem = !remove;
        Slots[id].quantity = remove ? 0 : quantity;
        Slots[id].updateIcon();
        audioManager.Play("Pick_Up_Item_1");
    }
    public void addItem(int id, int quantity, bool remove, bool Increase)
    {
        quantity = Increase ? quantity += 1 : quantity -= 1;
        items[id] = !remove;
        Slots[id].hasItem = !remove;
        Slots[id].quantity = remove ? 0 : quantity;
        Slots[id].updateIcon();
        audioManager.Play("Pick_Up_Item_1");
    }
    public bool hasItem(int id)
    {
        return items[id];
    }
    public int getSelectedID()
    {
        return Slots[selected].ID;
    }
    public void equipItem(int id, bool equip, int button)
    {
        if (items[id])
        {
            Slots[id].equip(equip ? button : 0);
            switch (button) {
                case 1:
                    equippedA = equip ? id : -1;
                    equippedB = equippedA == equippedB ? -1 : equippedB;
                    break;
                case 2:
                    equippedB = equip ? id : -1;
                    equippedA = equippedA == equippedB ? -1 : equippedA;
                    break;
            }
            for (int i = 0; i < items.Length; i++) Slots[i].equip(i == equippedA ? 1 : i == equippedB ? 2 : 0);
        }
    }

    public bool isEquipped(int id, int button)
    {
        return Slots[id].equipped == button;
    }
    public void open()
    {
        isOpen = !isOpen;
        string effectToPlay = isOpen ? "Open_Inventory_1" : "Close_Inventory_1";
        audioManager.Play(effectToPlay);
        if(!isOpen && !VendorMode)GameObject.FindGameObjectWithTag("Map").SetActive(false);
        VendorMode = false;
        FindObjectOfType<PlayerMovement>().enabled = !isOpen;
        selected = 0;
        column = 4;
    }
    public int getCoins()
    {
        return Coins;
    }
    public void addCoins(int coinsToBeAdded)
    {
        Coins += coinsToBeAdded;
        cointext.text = Coins+"";
    }
    IEnumerator delay()
    {
        delayed = true;
        yield return new WaitForSeconds(0.2f);
        delayed = false;
    }
}
