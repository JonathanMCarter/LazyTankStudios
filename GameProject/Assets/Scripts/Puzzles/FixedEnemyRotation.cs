using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FixedEnemyRotation : MonoBehaviour
{
    Quaternion MyRotation;

    void Start()
    {
        MyRotation = transform.rotation;
    }
    private void Update()
    {
        transform.rotation = MyRotation;
    }
}
