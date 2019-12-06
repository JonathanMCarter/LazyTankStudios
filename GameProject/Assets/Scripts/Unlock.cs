using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock : A
{
    public int Id;
    public string Tag;
    // Start is called before the first frame update
    void Start()
    {
        if (!FindObjectOfType<QuestLog>().Check(Id, Tag)) gameObject.SetActive(false);
    }


}
