using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDes : MonoBehaviour
{
    public bool Created;

    private int TimesFound;

    private void Awake()
    {
        //if (!Created) DontDestroyOnLoad(this); else Destroy(this.gameObject);
        DontDestroyOnLoad(this);

    }

    private void Start()
    {
        if (gameObject.name != "AudioManager") Created = true; //temp added by LC. excludes AudioManager as temp fix
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
