﻿using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CanEditMultipleObjects]
public class LookAtPoint : MonoBehaviour {

	public Vector3 lookAtPoint = Vector3.zero;

	// Update is called once per frame
	public void Update () {
		transform.LookAt(lookAtPoint);
	}
}
