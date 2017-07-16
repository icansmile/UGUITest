using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

	private float timeCounter = 0;
	private float timeAmount = 3;

	public Text text;
	public Image image;
	public Button button;
	public Slider slider;
	public Scrollbar scrollbar;
	public ScrollRect scrollView;
	public InputField inputFiled;
	public Toggle toggle;

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
		showSlider();
		showScrollbar();
		showScrollView();
		showInputField();
		showToggle();
	}

	void showToggle()
	{
		if(GUILayout.Button("开关"))
		{
			toggle.gameObject.SetActive(!toggle.gameObject.activeInHierarchy);
			toggle.isOn = false;
			toggle.onValueChanged.RemoveAllListeners();
			toggle.onValueChanged.AddListener(isOn => {
				Debug.Log("Toggle isOn: " + isOn);
			});
		}
	}

	void showInputField()
	{
		if(GUILayout.Button("输入框"))
		{
			inputFiled.gameObject.SetActive(!inputFiled.gameObject.activeInHierarchy);
			inputFiled.text = "";
			inputFiled.onEndEdit.RemoveAllListeners();
			inputFiled.onEndEdit.AddListener((str) => {
				Debug.Log("Input Field: " + str);
			});
		}
	}

	void showScrollView()
	{
		GUILayout.BeginHorizontal();

		if(GUILayout.Button("滚动窗"))
		{
			scrollView.gameObject.SetActive(!scrollView.gameObject.activeInHierarchy);
			scrollView.onValueChanged.RemoveAllListeners();
			scrollView.onValueChanged.AddListener(pos => {
				Debug.Log("Scroll View Pos: " + pos);
			});
		}

		if(GUILayout.Button("重置垂直滚动条"))
		{
			scrollView.verticalScrollbar.value = 1;
		}

		GUILayout.EndHorizontal();
	}

	void showScrollbar()
	{
		if(GUILayout.Button("滚动条"))
		{
			scrollbar.gameObject.SetActive(!scrollbar.gameObject.activeInHierarchy);
			scrollbar.value = 0;
			scrollbar.onValueChanged.RemoveAllListeners();
			scrollbar.onValueChanged.AddListener(value => {
				image.transform.eulerAngles = new Vector3(0, 0, 360 * value);
			});
		}
	}

	void showSlider()
	{
		if(GUILayout.Button("进度条"))
		{
			slider.gameObject.SetActive(!slider.gameObject.activeInHierarchy);
			slider.value = 0;
			slider.onValueChanged.RemoveAllListeners();
			slider.onValueChanged.AddListener((value) => {
				Debug.Log("Slider Value: " + slider.value);
			});
		}
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
			image.gameObject.SetActive(!image.gameObject.activeInHierarchy);
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
			text.gameObject.SetActive(!text.gameObject.activeInHierarchy);
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
