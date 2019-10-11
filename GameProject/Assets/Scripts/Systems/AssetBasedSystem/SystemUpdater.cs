using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemUpdater : MonoBehaviour
{
    public List<InputSystem> systems = new List<InputSystem>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var system in systems)
        {
            system.Update();
        }
    }
}
