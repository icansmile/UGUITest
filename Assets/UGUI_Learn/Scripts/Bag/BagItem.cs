using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagItem : MonoBehaviour,
UnityEngine.EventSystems.IDragHandler,
UnityEngine.EventSystems.IBeginDragHandler,
UnityEngine.EventSystems.IEndDragHandler
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
	{
		Debug.Log("begin drag.." + eventData.pointerDrag);
	}

	public void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
	{

	}

    public void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
	{
		Debug.Log("end drag.." + eventData.pointerCurrentRaycast.gameObject);
	}
}

public static class BagItemExtension
{
	public static BagItem Get(this GameObject go)
	{
		BagItem component = go.GetComponent<BagItem>();
		if(component == null)
			go.AddComponent<BagItem>();
		return component;
	}
}