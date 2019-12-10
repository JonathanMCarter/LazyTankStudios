public class ItemCheck : A
{public int item;
void Update(){
if (G<Inventory>(FT("Inv")).items.Contains(item)) gameObject.SetActive(false);}}