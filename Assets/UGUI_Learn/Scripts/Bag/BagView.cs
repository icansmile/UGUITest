using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC,MVP,MVVM http://www.linuxidc.com/Linux/2015-10/124622.htm
/// https://yq.aliyun.com/articles/11877
/// </summary>
public class BagView : MonoBehaviour {

	public GameObject Items;
	public BagModel.BagInfo BagInfo;

	// Use this for initialization
	void Start () 
	{
		updateItems();
	}

	void updateItems()
	{
		for(int i = 0; i < 2; ++i)
		{
			GameObject cell = Items.transform.Find(string.Format("slot ({0})/item",i)).gameObject;
			cell.Get();
		}
	}

	public void OnDrag(GameObject go)
	{
		Debug.Log("drag");
	}
}
