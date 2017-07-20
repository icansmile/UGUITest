using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/// <summary>
/// 
/// 参考
/// 官方API文档 https://docs.unity3d.com/ScriptReference/EditorWindow.html
/// </summary>
public class EditorWindow_Learn : EditorWindow
{
	string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    [MenuItem("Editor Tool/EditorWindow")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        EditorWindow_Learn window = (EditorWindow_Learn)EditorWindow.GetWindow(typeof(EditorWindow_Learn));
		window.titleContent = new GUIContent("这里是标题");
        window.Show();
		window.ShowNotification((new GUIContent("提示你一下")));
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
    }
}
