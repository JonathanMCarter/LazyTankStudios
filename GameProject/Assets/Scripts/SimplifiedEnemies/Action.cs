using System.Collections;
using UnityEngine;

namespace Final
{
    public class Action
    {
        public ActionDelegate ActionDelegate { get; set; }

        public virtual IEnumerator Use()
        {
            ActionDelegate();
            yield return null;
        }
    }
    public delegate void ActionDelegate();
}