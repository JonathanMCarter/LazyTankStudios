using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKeyEventListener : KeyEventListener
{
    public KeyCode key;

    public override void OnEventRaised(KeyCode key)
    {
        if (this.key == key) EventResponse?.Invoke(key);
    }
}
