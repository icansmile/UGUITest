using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

	public Text text;

	public Image image;
	float timeCounter = 0;
	float timeAmount = 3;

	public Button button;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if(timeCounter <= timeAmount)
		{
			timeCounter += Time.deltaTime;
			image.fillAmount = timeCounter/timeAmount;
		}
	}
	
	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		showText();
		showImage();
		showButton();
	}

	void showButton()
	{
		if(GUILayout.Button("监听按钮"))
		{
			button.gameObject.SetActive(true);

			button.onClick.AddListener(() => {
				Debug.Log("点击了按钮");

				button.onClick.RemoveAllListeners();
				button.gameObject.SetActive(false);
			});
		}
	}

	void showImage()
	{
		GUILayout.BeginHorizontal();

		if(GUILayout.Button("开始填充"))
		{
			image.fillMethod = Image.FillMethod.Radial360;
			image.fillOrigin = 0;
			timeCounter = 0;
		}

		GUILayout.EndHorizontal();
	}

	void showText()
	{
		GUILayout.BeginHorizontal();

		if(GUILayout.Button("文本"))
		{
			text.text = "UGUI <color=red>文本</color>";
			text.fontSize = 20;
			text.color = Color.cyan;
		}

		if(GUILayout.Button("富文本开关"))
		{
			text.supportRichText = !text.supportRichText;
		}

		if(GUILayout.Button("Best Fit"))
		{
			text.resizeTextForBestFit = !text.resizeTextForBestFit;
		}

		GUILayout.EndHorizontal();
	}
}
