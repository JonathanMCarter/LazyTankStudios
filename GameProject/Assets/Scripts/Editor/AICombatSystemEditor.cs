using UnityEngine;
using UnityEditor;

namespace AI
{
    [CustomEditor(typeof(AICombatSystem))]
    public class AICombatSystemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            AICombatSystem ai = (AICombatSystem)target;

            if (GUILayout.Button("Use Ability"))
            {
                ai.UseRandomAbility();
            }
        }
    }
}