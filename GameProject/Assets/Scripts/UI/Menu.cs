using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Menu Script
 * 
 * 
 * 
 * Owner: Toby Wishart
 * Last Edit : 12/10/19 Added delay and opens inventory
 * 
 * Also Edited by : Tony Parsons
 * Last Edit: 05/06.10.19
 * Reason: Testing purposes only. Quickest way I could think of to access combat through the game
 * 
 * */

public class Menu : MonoBehaviour
{
    enum menu { START, ITEMS, STATUS, MAP, SAVE, CONFIG, END };
    public GameObject combatOverlay;//the combat panel thing
    menu curInd = menu.ITEMS;

    private bool delayed = false;

    void Update()
    {
        if (!delayed)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                switch (curInd)
                {
                    //Tony - I've made it so items opens combat for testing purposes, remove it after finding a better way for accessing combat
                    case menu.ITEMS:
                        GameObject.Find("InventoryHotbar").GetComponent<Inventory>().open();
                        //combatOverlay.SetActive(true);
                        break;
                    case menu.START:
                    case menu.END:
                    default:
                        //invalid/not implemented
                        break;
                }
            }

            if (Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3"))
            {
                gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
            }

            if (Input.GetAxisRaw("Vertical") == -1)
            {
                curInd++;
                if (curInd == menu.END) curInd = menu.ITEMS;
                StartCoroutine(delay());
            }
            else if (Input.GetAxisRaw("Vertical") == 1)
            {
                curInd--;
                if (curInd == 0) curInd = menu.CONFIG;
                StartCoroutine(delay());
            }
            foreach (Transform child in transform) child.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            transform.GetChild((int)curInd - 1).GetComponent<Image>().color = new Color(1, 1, 1, 0.5F);
        }
    }

    IEnumerator delay()
    {
        delayed = true;
        yield return new WaitForSeconds(0.1f);
        delayed = false;
    }
}
