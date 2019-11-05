using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    VisualElement rootElement;
    VisualTreeAsset moduleVisualTree;
    public void OnEnable()
    {
        rootElement = new VisualElement();
        moduleVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/GridEditorTemplate.uxml");

        var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/GridEditorStyles.uss");

        rootElement.styleSheets.Add(stylesheet);
    }

    public override VisualElement CreateInspectorGUI()
    {
        var root = rootElement;
        moduleVisualTree.CloneTree(root);


        return root;
    }
}
