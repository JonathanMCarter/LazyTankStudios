//using System;
//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using UnityEngine.UIElements;

//[CustomEditor(typeof(Quest))]
//public class QuestEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        Quest script = (Quest)target;

//        script.type = (Quest.Type)(EditorGUILayout.EnumPopup("Type", script.type));

//        if (script.type == Quest.Type.Return)
//        {
//            script.NPCToReturnTo = (GameObject)EditorGUILayout.ObjectField("Return to NPC ", script.NPCToReturnTo, typeof(GameObject), true);
//            script.Dialogue = (DialogueFile)EditorGUILayout.ObjectField("Dialogue File ", script.Dialogue, typeof(DialogueFile), false);
//            script.KillRequest = EditorGUILayout.Toggle("Kill Request", script.KillRequest);

//            if (script.KillRequest)
//            {
//                serializedObject.Update();
//                SerializedObject SO = new SerializedObject(script);
//                SerializedProperty list = SO.FindProperty("Kills");

//                EditorGUI.BeginChangeCheck();


//                EditorGUILayout.PropertyField(list, true);




//                if (EditorGUI.EndChangeCheck())
//                    serializedObject.ApplyModifiedProperties();
//            }

//            // Quest.Kills = EditorGUILayout.IntField("Kills to be made", Quest.Kills);
//            //if (Quest.Kills > 0)
//            //{
//            //    foreach (GameObject go in Quest.Kills)
//            //        EditorGUILayout.ObjectField(go.name, go, typeof(GameObject), true);
//            //}
//        }
//    }
//}
