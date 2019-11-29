﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileSpawn : MonoBehaviour
{
    NewAIMove SC;
    public GameObject P;
    public float _T;
    float T;
    bool F;
    void Start()
    {
        SC = GetComponent<NewAIMove>();
        T = _T;
    }
    void Update()
    {
        Timer();
    }
    private void FixedUpdate()
    {
        if(F)
        {
            Instantiate(P, transform.position , transform.rotation);
            F = false;
        }
    }
    void Timer()
    {
        if (T <= 0 && SC.SeenPlayer)
        {
            F = true;
            T = _T;
        }
        else
            T -= Time.deltaTime;

    }
}
