using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public FloatReference speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Testo()
    {
        Debug.Log("Something happend");
    }

    public void Testo(KeyCode key)
    {
        Debug.Log($"I've heard that {key} was pressed!");
    }
}
