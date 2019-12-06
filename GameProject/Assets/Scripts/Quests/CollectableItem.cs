using UnityEngine;
public class CollectableItem : A
{
    public string ItemName;
    public int QId;
    public string QTag;

    public void Collect()
    {
        if (FindObjectOfType<QuestLog>().Collect(QId, QTag)) this.gameObject.SetActive(false);
    }
}