using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilitySet : ScriptableObject
{
    public List<Ability> abilities = new List<Ability>();
}
