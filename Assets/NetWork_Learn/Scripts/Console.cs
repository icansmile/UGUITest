using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		if(GUILayout.Button("客户端"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("Client");
		}

		if(GUILayout.Button("服务端"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("Server");
		}
	}
}
