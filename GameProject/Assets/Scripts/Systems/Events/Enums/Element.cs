using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Element", menuName = "Elements/Element")]
public class Element : ScriptableObject
{
    public List<Element> defeatingElements = new List<Element>();
}
