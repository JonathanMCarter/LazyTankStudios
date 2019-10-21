//using System;
//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using UnityEngine.UIElements;

//[CustomEditor(typeof(Quests))]
//public class QuestsEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        Quests script = (Quests)target;

//        script.Quest.type = (Quest.Type)(EditorGUILayout.EnumPopup("Type", script.Quest.type));

//        if (script.Quest.type == Quest.Type.Return)
//        {
//            script.Quest.NPCToReturnTo = (GameObject)EditorGUILayout.ObjectField("Return to NPC ", script.Quest.NPCToReturnTo, typeof(GameObject), true);
//            script.Quest.Dialogue = (DialogueFile)EditorGUILayout.ObjectField("Dialogue File ", script.Quest.Dialogue, typeof(DialogueFile), false);
//            script.Quest.Kill = EditorGUILayout.Toggle("Kill Quest", script.Quest.Kill);
//            script.Quest.Kills = EditorGUILayout.IntField("Kills to be made", script.Quest.Kills);
//            if (script.Quest.Killz>0)
//            {
//                foreach (GameObject go in script.Quest.Kills )
//                   EditorGUILayout.ObjectField(go.name, go, typeof(GameObject),true);
//            }
//        }
//    }
//}
