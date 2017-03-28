using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNothingAi : AiBehaviour {
	// Return weight value the designer can set easily
	public float ReturnWeight = 0.5f;

	public override float GetWeight()
	{
		return ReturnWeight;
	}

	public override void Execute ()
	{
		//Debug.Log("Do nothing");
	}
}
