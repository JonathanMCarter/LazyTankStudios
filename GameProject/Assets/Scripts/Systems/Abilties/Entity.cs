using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public List<AbilitySet> abilities = new List<AbilitySet>();
    public Ability a;
    public Transform mTransform { get; private set; }
    public AbilitySet abilitySet { get; private set; }

    private void Awake()
    {
        mTransform = transform;
        abilitySet = abilities?[0];
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) a?.Use(this);
        for (int i = abilitySet.abilities.Count - 1; i >= 0; i--)
        {
            abilitySet.abilities[i].Use(this);
        }
    }
}