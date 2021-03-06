﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
	public ToggleGroup toggleGroup;
	public GridLayoutGroup gridLayoutGroup;
	public Object itemPrefab;
	public Dropdown dropdown;

	public Canvas canvasRectTransform;
	public RectTransform rectTransform;
	public Slider rectTransformSlider;


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
		showGridLayoutGroup();
		showDropdown();

		testRectTransform();
	}

	void showDropdown()
	{
		if(GUILayout.Button("下拉选择框"))
		{
			dropdown.gameObject.SetActive(!dropdown.gameObject.activeInHierarchy);
			dropdown.captionText.text = "下拉选择框";

			dropdown.ClearOptions();
			List<Dropdown.OptionData> dataList = new List<Dropdown.OptionData>();
			for(int i = 0; i < 5; ++i)
			{
				var data = new Dropdown.OptionData();
				data.text = "option " + i;
				dataList.Add(data);
			}

			dropdown.options = dataList;

			dropdown.onValueChanged.RemoveAllListeners();
			dropdown.onValueChanged.AddListener(dataIndex => {
				Debug.Log("Select " + dataIndex);
			});
		}
	}

	//http://k79k06k02k.com/blog/542/unity/unity-ugui-%E5%8E%9F%E7%90%86%E7%AF%87%E4%BA%94%EF%BC%9Aauto-layout-%E8%87%AA%E5%8B%95%E4%BD%88%E5%B1%80
	void showGridLayoutGroup()
	{
		if(GUILayout.Button("Grid和Table"))
		{
			gridLayoutGroup.gameObject.SetActive(!gridLayoutGroup.gameObject.activeInHierarchy);
			
			int itemCount = 7;
			for(int i = 0; i < gridLayoutGroup.transform.childCount; ++i)
			{
				Destroy(gridLayoutGroup.transform.GetChild(i).gameObject);
			}

			for(int i = 0; i < itemCount; ++i)
			{
				GameObject itemGo = Instantiate(itemPrefab, Vector3.zero, new Quaternion(0,0,0,0)) as GameObject;
				itemGo.transform.SetParent(gridLayoutGroup.transform);
			}
		}
	}

	//http://www.jianshu.com/p/dbefa746e50d
	void testRectTransform()
	{
		GUILayout.BeginHorizontal();

		if(GUILayout.Button("测试RectTransform"))
		{
			canvasRectTransform.gameObject.SetActive(!canvasRectTransform.gameObject.activeInHierarchy);
			rectTransformSlider.onValueChanged.RemoveAllListeners();
			rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
			rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
			rectTransform.anchoredPosition = Vector2.zero;
			Debug.Log(rectTransform.offsetMax);
		}

		if(GUILayout.Button("anchorMax"))
		{
			rectTransformSlider.onValueChanged.RemoveAllListeners();
			rectTransformSlider.onValueChanged.AddListener(value => {
				rectTransformSlider.handleRect.transform.Find("Text").GetComponent<Text>().text = value.ToString();
				rectTransform.anchorMax = new Vector2(value, value);
			});
		}

		if(GUILayout.Button("anchorMin"))
		{
			rectTransformSlider.onValueChanged.RemoveAllListeners();
			rectTransformSlider.onValueChanged.AddListener(value => {
				rectTransformSlider.handleRect.transform.Find("Text").GetComponent<Text>().text = value.ToString();
				rectTransform.anchorMin = new Vector2(value, value);
			});
		}

		if(GUILayout.Button("anchorPosition"))
		{
			rectTransformSlider.onValueChanged.RemoveAllListeners();
			rectTransformSlider.onValueChanged.AddListener(value => {
				rectTransformSlider.handleRect.transform.Find("Text").GetComponent<Text>().text = value.ToString();
				rectTransform.anchoredPosition = new Vector2(value * 220, value * 220);
			});
		}

		GUILayout.EndHorizontal();
	}

	void showToggle()
	{
		if(GUILayout.Button("开关"))
		{
						
			toggleGroup.gameObject.SetActive(!toggleGroup.gameObject.activeInHierarchy);

			for(int i = 0; i < toggleGroup.transform.childCount; ++i)
			{
				//如果直接把 i 传入匿名函数， 则输出结果全都是 i 的最后一个值！
				int index = i;

				Toggle toggle = toggleGroup.transform.GetChild(i).GetComponent<Toggle>();

				toggle.isOn = false;
				toggle.onValueChanged.RemoveAllListeners();
				toggle.onValueChanged.AddListener(isOn => {
					Debug.Log(string.Format("Toggle {0} isOn: {1}", index.ToString(), isOn.ToString()));
				});

				toggle.group = toggleGroup;
			}
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
