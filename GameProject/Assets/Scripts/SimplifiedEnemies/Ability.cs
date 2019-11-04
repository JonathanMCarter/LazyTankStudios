using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
    public abstract class Ability : MonoBehaviour
    {
        public PlayerVariable Player;
        public float ExpiryTime = 0f;
        public abstract IEnumerator Use();
    } 
}
