using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAIPause : MonoBehaviour
{
    private NewAIMove me;
    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        me=GetComponent<NewAIMove>();
        manager=FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.isPaused)me.MyRigid.velocity=Vector2.zero;
        me.enabled=!manager.isPaused;
    }
}
