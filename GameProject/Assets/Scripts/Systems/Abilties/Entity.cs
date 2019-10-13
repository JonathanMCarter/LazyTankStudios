using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public List<Ability> abilities = new List<Ability>();
    public Transform mTransform;

    private void Start()
    {
        mTransform = transform;
    }

    private void Update()
    {
        for (int i = abilities.Count - 1; i >= 0; i--)
        {
            abilities[i].Use(this);
        }
    }
}