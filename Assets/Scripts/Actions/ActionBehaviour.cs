/// <summary>
/// Action behaviour.
/// Enables unit behaviour to be designed in the editor.
/// </summary>
using System.Collections;
using UnityEngine;
using System;

public abstract class ActionBehaviour : MonoBehaviour {

	// Get the click action from this action behaviour
	public abstract Action GetClickAction();

	public Sprite ButtonPic;
}
