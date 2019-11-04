using System.Collections;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [SerializeField] protected PlayerVariable player;

    private IEnumerator currentAction;
}