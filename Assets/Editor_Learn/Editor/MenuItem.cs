using System.Collections;
using UnityEngine;
using UnityEditor;

public class Editor_MenuItem{

	[MenuItem("Editor Tool/Get Selection Objects\n")]
	private static void GetSelectionObjects()
	{
		// Transform[] transforms = Selection.GetTransforms(SelectionMode.TopLevel |　SelectionMode.OnlyUserModifiable);
		var objs = Selection.objects;
		foreach(var obj in objs)
		{
			Debug.Log(obj.name);
		}
	}
}
