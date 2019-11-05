using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public int BouldersPushed;
    void Start()
    {
        BouldersPushed = 0;
    }

    public void AddBoulder()
    {
        BouldersPushed++;
    }
    void Update()
    {
        if(BouldersPushed == 2)
        {
            gameObject.SetActive(false);
        }
             
    }
}
