//using UnityEngine;
//using UnityEditor;

//namespace AI
//{
//    [CustomEditor(typeof(AIEnemy))]
//    public class AIEnemyEditor : Editor
//    {
//        public override void OnInspectorGUI()
//        {
//            base.OnInspectorGUI();

//            AIEnemy ai = (AIEnemy)target;
            
//            if (GUILayout.Button("Use Ability"))
//            {
//                ai.UseRandomAbility();
//            }

//            if (GUILayout.Button("Start Combat"))
//            {
//                ai.InitCombat();
//            }

//        }
//    }
//}