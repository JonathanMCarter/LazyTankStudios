using System.Collections.Generic;

public class MultipleVoidListener : BaseMultipleGameEventListener<Void, VoidEvent, UnityVoidEventResponse>
{
    public List<VoidEventPair> pairs;

    protected override void OnEnable()
    {
        foreach (var pair in pairs)
        {
            pair.gameEvent?.AddListener(pair);
        }
    }

    protected override void OnDisable()
    {
        foreach (var pair in pairs)
        {
            pair.gameEvent?.RemoveListener(pair);
        }
    }
}
