using UnityEngine;

public class SKeyEventListener : KeyEventListener
{
    public KeyCode key;

    public override void OnEventRaised(KeyCode key)
    {
        if (this.key == key) eventResponse?.Invoke(key);
    }
}
