using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagModel
{
	public class ItemInfo
	{
		public string Id {get; private set;}
		public int Num {get; set;}
		public int Pos {get; set;}

		public ItemInfo(string _Id, int _Num)
		{
			Id = _Id;
			Num = _Num;
		}
	}

	public class BagInfo
	{
		private List<ItemInfo> _Items;
		public List<ItemInfo> Items 
		{
			get
			{ 
				return _Items;
			}

			private set
			{
				Debug.Log("list set");
				_Items = value;

				if(ItemListChanged != null) 
					ItemListChanged.Invoke();
			}
		}

		//列表改变事件
		public delegate void VoidHandler();
		public event VoidHandler ItemListChanged;

		public BagInfo(List<ItemInfo> _Items)
		{
			Items = _Items;
		}
	}
}
