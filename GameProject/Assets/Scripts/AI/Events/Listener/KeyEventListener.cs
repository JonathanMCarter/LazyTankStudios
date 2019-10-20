using System.Collections.Generic;
using UnityEngine;

public class KeyEventListener : BaseGameEventListener<KeyCode, KeyEvent, UnityKeyEventResponse> { }
//public class MultipleKeyEventListener : BaseMultipleGameEventListener<KeyCode, KeyEvent, UnityKeyEventResponse>
//{
//    public List<KeyEventPair> pairs;

//    protected override void OnEnable()
//    {
//        foreach (var pair in pairs)
//        {
//            pair.gameEvent?.AddListener(pair);
//        }
//    }

//    protected override void OnDisable()
//    {
//        foreach (var pair in pairs)
//        {
//            pair.gameEvent?.RemoveListener(pair);
//        }
//    }
//}