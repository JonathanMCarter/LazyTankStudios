using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Created by Toby Wishart
 * Last edit: 12/10/19
 */
public class DialogueBoxManager : MonoBehaviour
{

    private RectTransform panel;

    private void Awake()
    {
        panel = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        //Check y position of player in relation to the camera
        if (Camera.main.transform.InverseTransformDirection(GameObject.FindGameObjectWithTag("Player").transform.position - Camera.main.transform.position).y < 0)
        {
            //Place panel at the top of the screen
            if (panel.anchorMax.y != 1)
            {
                panel.position = new Vector3(panel.position.x, -panel.position.y, panel.position.z);
                panel.anchorMin = new Vector2(panel.anchorMin.x, 1);
                panel.anchorMax = new Vector2(panel.anchorMax.x, 1);
            }
        } else
        {
            //Place panel at the bottom of the screen
            if (panel.anchorMax.y != 0)
            {
                panel.anchorMin = new Vector2(panel.anchorMin.x, 0);
                panel.anchorMax = new Vector2(panel.anchorMax.x, 0);
                panel.position = new Vector3(panel.position.x, -panel.position.y, panel.position.z);
            }
        }
    }

}
