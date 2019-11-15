﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NewAIMove : MonoBehaviour
{
    public float MoveSpeed;
    public float RotateSpeed;
    float WaitTime;
    public float Direction;
    float TurnTime;
    public Transform me;
    Transform Player;
    Rigidbody2D MyRigid;
    public Vector2 Paramiers;
    public Vector2 WaitVarables;
    bool SeenPlayer;
    bool Turn;
    bool TurnCount;
    bool ToggleDirection;
    void Start()
    {
        TurnCount = true;
        MyRigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        me.transform.position = transform.position;
        if (SeenPlayer)
            RunToPlayer();
        else
            RandomWander();
    }
    private void FixedUpdate()
    {
        if (ToggleDirection || SeenPlayer)
        {
            ToggleDirection = true;
            if (!SeenPlayer)

                MyRigid.angularVelocity = 0;
            MyRigid.velocity = transform.up * (MoveSpeed * Time.deltaTime);
        }
        else
            MyRigid.velocity = transform.up * (-MoveSpeed * Time.deltaTime);
        if (Turn)
        {
            if (Direction < 50)
                transform.Rotate(0, 0, RotateSpeed * Time.deltaTime);
            else if (Direction >= 50)
                transform.Rotate(0, 0, -RotateSpeed * Time.deltaTime);
        }
    }
    void RandomWander()
    {
        if (TurnCount)
        {
            TurnTime -= Time.deltaTime;
            Direction = Random.Range(1, 100);
            WaitTime = Random.Range(WaitVarables.x, WaitVarables.y);
            Turn = false;
            if (TurnTime <= 0)
            {
                TurnCount = false;
                Turn = true;
            }
        }
        else
        {
            TurnTime = Random.Range(Paramiers.x, Paramiers.y);
            WaitTime -= Time.deltaTime;
            if (WaitTime <= 0)
            {
                TurnCount = true;
            }
        }
    }
    void RunToPlayer()
    {
        Transform PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Vector2 Direction = new Vector2(PlayerPosition.position.x - transform.position.x, PlayerPosition.position.y - transform.position.y);
        transform.up = Direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player = collision.gameObject.transform;
                SeenPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
                SeenPlayer = false;
                ToggleDirection = false;
                Player = null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ToggleDirection)
            ToggleDirection = false;
        else
            ToggleDirection = true;
    }
}
