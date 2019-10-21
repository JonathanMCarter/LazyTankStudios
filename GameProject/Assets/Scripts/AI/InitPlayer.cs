using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPlayer : MonoBehaviour
{
    public PlayerVariable player;

    private void Awake()
    {
        player.Value = GetComponent<TestPlayer>();
    }
}
