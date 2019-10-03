using UnityEngine;

namespace AI
{
    [System.Serializable]
    public abstract class Ability : ScriptableObject
    {
        public string Name;
        public float chance = 0;

        public virtual void Use(AIEnemy target)
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

        public override void Use(AIEnemy target)
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

        public override void Use(AIEnemy target)
        {
            target.HP -= amount;
        }
    }

    [CreateAssetMenu(fileName = "Heal", menuName = "Abilities/Heal")]
    public class Heal : Ability
    {
        public int amount;
    } 
}
