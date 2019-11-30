using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailorCostumeWindow : A
{
    public void EnableCostumeWindow()
    {
        C(GameObject.FindGameObjectWithTag("CostumeSwap").transform,0).gameObject.SetActive(true);
    }
}
