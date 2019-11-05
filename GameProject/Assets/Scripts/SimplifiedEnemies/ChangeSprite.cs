using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
    public class ChangeSprite : Ability
    {
        [SerializeField] private Sprite sprite;
        public override IEnumerator Use()
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
            yield return null;
        }
    } 
}
