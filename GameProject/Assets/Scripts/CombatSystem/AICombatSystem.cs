using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AICombatSystem : MonoBehaviour
    {
        [SerializeField] private List<Ability> abilities;

        public void UseRandomAbility()
        {
            if (!CalcChances())
            {
                Debug.LogError("Ability chances sum must be 100");
                return;
            }

            int r = Random.Range(0, 100);
            float threshold = 0;

            //Check all Abilities to use except the last one
            for (int i = 0; i < abilities.Count - 1; i++)
            {
                if (r > threshold && r < threshold + abilities[i + 1].chance)
                {
                    abilities[i].Use();
                    return;
                }

                threshold += abilities[i].chance;
            }

            //If the others weren't use
            abilities[abilities.Count - 1].Use();
        }

        //Summs up all Chances of the abilitiy set. Returns true when 100%
        private bool CalcChances()
        {
            float sum = 0;

            for (int i = 0; i < abilities.Count; i++)
            {
                sum += abilities[i].chance;
            }

            return Mathf.Ceil(sum) == 100f;
        }
    } 
}