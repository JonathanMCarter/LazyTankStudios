using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDes : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
