using UnityEngine;

[CreateAssetMenu(fileName = "Void Event", menuName = "Events/Void Event")] public class VoidEvent : BaseGameEvent<Void>{ public void Raise() => Raise(new Void()); }
public struct Void { }