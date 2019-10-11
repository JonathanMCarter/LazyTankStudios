using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Input System", menuName = "Systems/Input System")]
public class InputSystem : ScriptableObject
{
    public KeyEvent KeyEvent;
    
    public List<KeyCode> keys = new List<KeyCode>();

    public void Update()
    {
        foreach (var key in keys)
        {
            if (Input.GetKeyDown(key)) KeyEvent.Raise(key);
        }
    }
}
