using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    enum menu { START, ITEMS, EQUIP, STATUS, MAP, SAVE, CONFIG, END };

    menu curInd = menu.ITEMS;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            switch (curInd)
            {
                case menu.ITEMS:
                    //open inventory here                    
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
