using UnityEngine;
using UnityEditor;
public class Vendor: A {

    public GameObject p;
    public int price;
    bool openShop = false;
    CanvasGroup cg;
    InputManager IM;
    Inventory inv;
    private void Start()
    {
        cg = G<CanvasGroup>(F("VendorInventory"));
        IM = F<InputManager>();
        inv = G<Inventory>(F("PlayerInventory"));
    }
    private void Update()
    {
        if (openShop)
        {
            if (F<DialogueScript>().FileHasEnded && cg.alpha != 1)
                cg.alpha = 1;
            else if (cg.alpha == 1 && Input.GetKeyDown(KeyCode.Escape))
            {
                cg.alpha = 0;
                openShop = false;
            }
            if (IM.Button_A() && cg.alpha == 1 && inv.getCoins() >= price)
            {
                p.GetComponent<InventoryItem>().pickup();
                inv.addCoins(-price);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            openShop = true;
    }
}
