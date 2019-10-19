using UnityEngine;

namespace AI
{
    public class Player : MonoBehaviour
    {
        public int Health = 100;
        public PlayerVariable playerStats;

        private void Awake()
        {
            playerStats.Value = this;
        }
    }
}