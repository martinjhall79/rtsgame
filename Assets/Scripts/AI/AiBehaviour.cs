/// <summary>
/// AI behaviour.
/// Weighted list of AI behaviours.
/// Each AI decision returns a weighted value of it's importance.
/// We decide on whether to let it run based on its purpose and value
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiBehaviour : MonoBehaviour
{
	// Allow designer to adjust the randomness of the AI decision making
	public float WeightMultiplier = 1;
	public float TimePassed = 0;
	// The importance of the AI behaviour
	public abstract float GetWeight ();
	// Execute the decision
	public abstract void Execute ();
}
