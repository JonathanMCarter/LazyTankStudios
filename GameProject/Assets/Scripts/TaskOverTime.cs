using System.Collections;
using UnityEngine;
namespace AI
{
    public class TaskOverTime
    {
        private float progress; private IEnumerator coroutine; private MonoBehaviour mb; public TaskOverTime(MonoBehaviour mb)
        { this.mb = mb; }
        public void Start(float time, InterpolateDel task, CallbackDel callback = null)
        { coroutine = Execute(time, task, callback); mb.StartCoroutine(coroutine); }
        public void Stop() => mb.StopCoroutine(coroutine); private IEnumerator Execute(float time, InterpolateDel task, CallbackDel callback)
        {
            progress = 0f; while (progress < 1f)
            { task(progress); progress += Time.deltaTime / time; yield return null; }
            task(progress); callback?.Invoke();
        }
    }
    public delegate void InterpolateDel(float progress); public delegate void CallbackDel();
}