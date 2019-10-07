using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Eney Combat inititaion
 * 
 *  Starts combat if player collides with enemy if this script is attached to it
 *
 * Owner: Andreas Kraemer
 * Last Edit : 7/10/19
 * 
 * Also Edited by : <Enter name here if you edit this script>
 * Last Edit: <Date here if you edit this script>
 * 
 * */

public class EnemyCombatTrigger : MonoBehaviour
{
    //assign combat menu in the editor
    public GameObject combatMenu;
    BattleTransitionEffectCamera effectCamera;

    void Start()
    {
        effectCamera=GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BattleTransitionEffectCamera>();
    }

    ///initiates combat on collision
    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if(otherCollider.gameObject.tag=="Player")
        {
        effectCamera.PlayEffect();
        StartCoroutine(StartCombat());
        }
    }

    IEnumerator StartCombat()
    {
        yield return new WaitForSeconds(1f);

        
        combatMenu.SetActive(true);

    }
}
