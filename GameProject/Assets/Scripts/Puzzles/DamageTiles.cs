using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTiles : MonoBehaviour
{
    public int Damage;
    private int _Damage;
    private int DamageTimer;
    private Coroutine MyCo = null;
    void Start()
    {
        _Damage = Damage;
    }
    
    private void OnCollisionEnter2D(Collision2D Hit)
    {
        
        if (Hit.gameObject.CompareTag("Player"))
        {
            if (MyCo == null) MyCo = StartCoroutine(Timer());
                
            
            Hit.gameObject.GetComponent<PlayerMovement>().health -= _Damage;
            gameObject.GetComponent<PlayerMovement>().RemoveHeart();
        }
       
    }
    //private void OnTriggerEnter2D(Collider2D Hit)
    //{
    //    Debug.Log("hit");
    //    if (Hit.gameObject.CompareTag("Player"))
    //    {
    //        MyCo = StartCoroutine(Timer());


    //        Hit.gameObject.GetComponent<PlayerMovement>().health -= _Damage;
    //        gameObject.GetComponent<PlayerMovement>().RemoveHeart();
    //    }
    //}


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (MyCo != null)
        {
            StopCoroutine(MyCo);
            MyCo = null;
        }
    }

    private void OnCollisionStay2D(Collision2D Hit)
    {
        if (Hit.gameObject.CompareTag("Player"))
        {
           
           Hit.gameObject.GetComponent<PlayerMovement>().health -= _Damage;
        }
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            print("Timer");
        }
        
    }
}
