using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject portal;
    public float XOffset;
    public float YOffset;
    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector2 NewPosition = new Vector2(portal.transform.position.x + XOffset, portal.transform.position.y +YOffset);
       collision.transform.position = NewPosition;
    }
}
