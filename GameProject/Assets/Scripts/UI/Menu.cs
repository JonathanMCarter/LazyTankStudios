using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Menu Script
 * 
 * 
 * 
 * Owner: ????
 * Last Edit : 
 * 
 * Also Edited by : Tony Parsons
 * Last Edit: 05/06.10.19
 * Reason: Testing purposes only. Quickest way I could think of to access combat through the game
 * 
 * */

public class Menu : MonoBehaviour
{
    enum menu { START, ITEMS, EQUIP, STATUS, MAP, SAVE, CONFIG, END };
    public GameObject combatOverlay;//the combat panel thing
    menu curInd = menu.ITEMS;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            switch (curInd)
            {
                //Tony - I've made it so items opens combat for testing purposes, remove it after finding a better way for accessing combat
                case menu.ITEMS:
                    combatOverlay.SetActive(true);
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
        } else if (Input.GetAxisRaw("Vertical") == 1)
        {
            curInd--;
            if (curInd == 0) curInd = menu.CONFIG;
        }
        foreach (Transform child in transform) child.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        transform.GetChild((int)curInd-1).GetComponent<Image>().color = new Color(255, 255, 255, 0.5F);

    }
}
