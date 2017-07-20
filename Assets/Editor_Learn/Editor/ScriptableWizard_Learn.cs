using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 只有两个按钮, Unity的Editor中提供创建向导的功能, 方便策划一步步创建
/// 参考
/// 官方api文档 https://docs.unity3d.com/ScriptReference/ScriptableWizard.html
/// http://www.cnblogs.com/zhaoqingqing/p/3812397.html
/// </summary>
public class ScriptableWizard_Learn : ScriptableWizard
{
	public string Name = "GameObject";
    public Color color = Color.red;

    [MenuItem("Editor Tool/Create GameObject Wizard")]
    private static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<ScriptableWizard_Learn>("Create GameObject", "Create", "Apply");
    }

    //点击[create]按钮
    void OnWizardCreate()
    {
        GameObject go = new GameObject("New GameObject");
        if(Selection.activeTransform != null)
        {
            go.transform.SetParent(Selection.activeTransform);
        }
        go.name = Name;
    }

    void OnWizardUpdate()
    {
        helpString = "这里是帮助信息";
    }

    // When the user presses the "Apply" button OnWizardOtherButton is called.
    //点击[other]按钮
    void OnWizardOtherButton()
    {
        if (Selection.activeTransform != null)
        {
            Selection.activeTransform.name = Name;
        }
    }
}
