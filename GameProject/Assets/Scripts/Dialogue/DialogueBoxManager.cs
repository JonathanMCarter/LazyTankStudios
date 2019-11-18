using UnityEngine;
public class DialogueBoxManager : A
{
    RectTransform panel;

    void Awake()
    {
        panel = GetComponent<RectTransform>();
    }

    void Start()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
}
