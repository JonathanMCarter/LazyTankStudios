using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailorCostumeWindow : MonoBehaviour
{
    public void EnableCostumeWindow()
    {
        GameObject.FindGameObjectWithTag("CostumeSwap").transform.GetChild(0).gameObject.SetActive(true);
    }
}
