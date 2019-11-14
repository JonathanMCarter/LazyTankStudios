﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NewAIMove : MonoBehaviour
{
    public float MoveSpeed;
    public float RotateSpeed;
    float WaitTime;
    float Direction;
    float TurnTime;
    public Transform me;
    //public Transform _me;
    Rigidbody2D MyRigid;
    public Vector2 Paramiers;
    public Vector2 WaitVarables;
    bool SeenPlayer;
    bool Turn;
    bool TurnCount;
    bool ToggleDirection;
    public bool Coridor;
    void Start()
    {
        TurnCount = true;
        MyRigid = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    { 
         me.transform.position = transform.position;
        //_me.transform.position = transform.position;
        if (SeenPlayer)
            RunToPlayer();
        else if(!Coridor)
            RandomWander();
    }
    private void FixedUpdate()
    {
        if (ToggleDirection || SeenPlayer || Coridor)
        {
            if (Coridor)
                MyRigid.angularVelocity = 0;
            Debug.Log("Should not be rotating");
            MyRigid.velocity = transform.up * (MoveSpeed * Time.deltaTime);
        }
        else
            MyRigid.velocity = transform.up * (-MoveSpeed * Time.deltaTime);
        if (Turn)
        {
            Debug.Log("Turning");
            Debug.Log("Rotating");
            if (Direction < 50)
                transform.Rotate(0, 0, RotateSpeed * Time.deltaTime);
            else if(Direction >= 50)
                transform.Rotate(0, 0, -RotateSpeed * Time.deltaTime);
        }

    }
    public void Node(Vector2 Direction)
    {
        if (!Coridor)
        {
            transform.up = Direction;
            Coridor = true;
            ToggleDirection = true;
        }
    }
    public void LeaveNode()
    {
        Coridor = false;
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
            SeenPlayer = true;

        //if (collision.gameObject.CompareTag("Node"))
        //{

        //    if (!Coridor)
        //    {
        //        GameObject MyObj = collision.gameObject;
        //        transform.up = MyObj.GetComponent<AINode>().Directions();
        //        Coridor = true;
        //        ToggleDirection = true;
        //    }
        //}
        //if (collision.gameObject.CompareTag("End") )
        //{
        //    Coridor = false;
        //}
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            SeenPlayer = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ToggleDirection)
            ToggleDirection = false;
        else
            ToggleDirection = true;
    }
}
