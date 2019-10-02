using UnityEngine;

namespace AI
{
    [System.Serializable]
    public abstract class Ability : ScriptableObject
    {
        public string Name;
        public float chance = 0;

        public virtual void Use()
        {
            Debug.Log($"{Name} used");
        }
    }

    public abstract class Spell : Ability
    {
    }

    [CreateAssetMenu(fileName = "Spell", menuName = "Abilities/Spell")]
    public class Paralyze : Spell
    {
        public int stunTime = 1;

        public override void Use()
        {
            Debug.Log($"Using {Name}");
        }
    }


    [CreateAssetMenu(fileName = "Defend", menuName = "Abilities/Defend")]
    public class Defend : Ability
    {

    }

    [CreateAssetMenu(fileName = "Attack", menuName = "Abilities/Attack")]
    public class Attack : Ability
    {
        public int amount;
    }

    [CreateAssetMenu(fileName = "Heal", menuName = "Abilities/Heal")]
    public class Heal : Ability
    {
        public int amount;
    } 
}
