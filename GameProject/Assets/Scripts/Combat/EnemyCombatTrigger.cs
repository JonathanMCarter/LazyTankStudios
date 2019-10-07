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
 * Also Edited by : Lewis Cleminson
 * Last Edit: 07.10.19
 * Reason: Lock player input when combat starts
 * 
 * */

public class EnemyCombatTrigger : MonoBehaviour
{
    //assign combat menu in the editor
    public GameObject combatMenu;
    BattleTransitionEffectCamera effectCamera;

    private GameManager gameManager; // added by LC

    void Start()
    {
        effectCamera=GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BattleTransitionEffectCamera>();
        gameManager = FindObjectOfType<GameManager>(); // Added by LC
    }

    ///initiates combat on collision
    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if(otherCollider.gameObject.tag=="Player")  StartCoroutine(StartCombat()); 
    }

    IEnumerator StartCombat()
    {
        effectCamera.PlayEffect(); //Moved into StartCombat() to optimize
        gameManager.TogglePlayerMovement(); //added by LC
        yield return new WaitForSeconds(1f);

        
        combatMenu.SetActive(true);

    }
}
