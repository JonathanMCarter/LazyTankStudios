using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AttackElement : MonoBehaviour
{
    public Element element;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        name = element?.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(element.defeatingElements.Contains(other.GetComponent<AttackElement>().element))
        {
            Destroy(other.gameObject);
        }
    }
}
