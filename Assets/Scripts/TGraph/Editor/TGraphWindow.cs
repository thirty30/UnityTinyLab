using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Callbacks;

public class TGraphWindow : EditorWindow
{
    private TGraphView mGraphView;

    [MenuItem("Window/TGraphWindow")]
    public static void OpenGraphWindow()
    {
        var window = GetWindow<TGraphWindow>();
        window.titleContent = new GUIContent("TGraph Window");
    }

    //open the object by double click or open button
    [OnOpenAssetAttribute(0)]
    public static bool OpenTGraphObject(int instanceID, int line)
    {
        Object obj = EditorUtility.InstanceIDToObject(instanceID);
        if (obj.GetType() != typeof(TGraphData))
        {
            return false;
        }
        var window = GetWindow<TGraphWindow>();
        window.titleContent = new GUIContent("TGraph Window");
        return true;
    }

    private void OnEnable()
    {
        this.InitializeGraphView();
    }

    private void OnDisable()
    {
        this.rootVisualElement.Remove(this.mGraphView);
    }

    private void InitializeGraphView()
    {
        this.mGraphView = new TGraphView();
        this.mGraphView.name = "TGraphView";
        this.mGraphView.StretchToParentSize();
        this.rootVisualElement.Add(this.mGraphView);
    }
}
