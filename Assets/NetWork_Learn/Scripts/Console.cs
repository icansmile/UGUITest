using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		if(GUILayout.Button("服务端开启"))
		{
			var go = new GameObject("Server");
			go.AddComponent<ChatServer>();
		}

		if(GUILayout.Button("客户端开启"))
		{
			var go = new GameObject("Client");
			var client = go.AddComponent<ChatClient>();

		}
	}
}
