﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTrigger : MonoBehaviour
{
    int m_OriginalSortingLayer=10;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        m_OriginalSortingLayer=otherCollider.GetComponent<SpriteRenderer>().sortingOrder;
        otherCollider.gameObject.GetComponent<SpriteRenderer>().sortingOrder=0;
    }
    void OnTriggerExit2D(Collider2D otherCollider)
    {
        otherCollider.gameObject.GetComponent<SpriteRenderer>().sortingOrder=m_OriginalSortingLayer;
    }
}
