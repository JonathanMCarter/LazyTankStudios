using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxManager : MonoBehaviour
{

    private RectTransform panel;

    private void Awake()
    {
        panel = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        if (Camera.main.transform.InverseTransformDirection(GameObject.FindGameObjectWithTag("Player").transform.position - Camera.main.transform.position).y < 0)
        {
            if (panel.anchorMax.y != 1)
            {
                panel.position = new Vector3(panel.position.x, -panel.position.y, panel.position.z);
                panel.anchorMin = new Vector2(panel.anchorMin.x, 1);
                panel.anchorMax = new Vector2(panel.anchorMax.x, 1);
            }
        } else
        {
            if (panel.anchorMax.y != 0)
            {
                panel.anchorMin = new Vector2(panel.anchorMin.x, 0);
                panel.anchorMax = new Vector2(panel.anchorMax.x, 0);
                panel.position = new Vector3(panel.position.x, -panel.position.y, panel.position.z);
            }
        }
    }

}
