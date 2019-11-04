using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : Ability
{
    public float SpinTime;
    private ActionOverTime aot;

    public void Init(float spinTime)
    {
        SpinTime = spinTime;
        aot = new ActionOverTime();
        aot.ExpiryTime = spinTime;
        aot.ActionDelegate = SpinExecute;
    }

    public override IEnumerator Use(MonoBehaviour mb, float deltaTime)
    {
        AnimationClip?.Play();
        yield return aot.Use(mb, deltaTime);
    }

    public void SpinExecute(MonoBehaviour mb, float deltaTime)
    {
        Debug.Log("Spinning");
    }
}