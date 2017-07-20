using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor只要是负责显示 附加在Inspector检视窗中的UI  https://docs.unity3d.com/Manual/editor-CustomEditors.html
/// Handles只要是用来描绘Scene窗口中的GUI， 例如坐标轴等 https://docs.unity3d.com/ScriptReference/Handles.html
/// 
/// 
/// 参考:
/// 
/// 有空看[如何在Unity编辑器中添加你自己的工具,绘制你自己的gizmo] https://code.tutsplus.com/zh-hans/tutorials/how-to-add-your-own-tools-to-unitys-editor--active-10047
/// http://blog.csdn.net/jjiss318/article/details/7435708
/// </summary>

[CustomEditor(typeof(LookAtPoint))]
[CanEditMultipleObjects]
public class Editor_Leran : Editor
{
    SerializedProperty lookAtPoint;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        lookAtPoint = serializedObject.FindProperty("lookAtPoint");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.LabelField("这里是LabelField,可以和在OnGUI一样使用各种描绘方法");
        serializedObject.ApplyModifiedProperties();
    }

    public void OnSceneGUI()
    {
        var t = (target as LookAtPoint);

        EditorGUI.BeginChangeCheck();
        Vector3 pos = Handles.PositionHandle(t.transform.position, Quaternion.identity);
        if(EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Move Point");
            t.lookAtPoint = pos;
            t.Update();
        }
    }
}