using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDes : MonoBehaviour
{
    public static bool Created;
    private void Awake()
    {
        //if (!Created) DontDestroyOnLoad(this); else Destroy(this.gameObject);
    }

    private void Start()
    {
        if (gameObject.name != "AudioManager")
        {
            DontDestroyOnLoad(this);
        }

        //Created = true;
    }
}
