using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CodeTester {

	public static List<int> GetRandomIntList(int count)
	{
		List<int> randomList = new List<int>();
		System.Random rnd = new System.Random(0);

		for(int i = 0; i < count; ++i)
		{
			randomList.Add(rnd.Next(1, count-1));
		}
		
		return randomList;
	}
}

public class CodeTimeTester : CodeTester
{
	private System.DateTime startTime;
	private System.DateTime stopTime;

	public double duration
	{
		get
		{
			return (stopTime - startTime).TotalSeconds;
		}
	}

	public CodeTimeTester()
	{
	}

	public void StartTime()
	{
		System.GC.Collect();
		System.GC.WaitForPendingFinalizers();
		startTime = System.DateTime.Now;
	}

	public void StopTime()
	{
		stopTime = System.DateTime.Now;
	}
}
