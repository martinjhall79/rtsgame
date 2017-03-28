/// <summary>
/// Create drones ai.
/// Decides whether AI should create another drone.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDronesAi : AiBehaviour
{
	public int DronesPerBase = 5;
	public float Cost = 25;
	private AiSupport support;

	// Should the AI create another drone right now
	public override float GetWeight ()
	{
		if (support == null) {
			support = AiSupport.GetSupport(gameObject);
		}
		if (support.Player.Credits < Cost) {
			return 0;
		}

		var drones = support.Drones.Count;
		var bases = support.CommandBases.Count;

		// If there are more drones, than bases to support them, don't create another drone
		if (bases == 0) {
			return 0;
		}
		if (drones >= bases * DronesPerBase) {
			return 0;
		}
		// Safe to build a drone
		return 1;
	}

	public override void Execute ()
	{
		// Select a base to build the drone from
		var bases = support.CommandBases;
		var index = Random.Range(0, bases.Count);
		// Create the drone
		var ai = AiSupport.GetSupport(this.gameObject);
		Debug.Log(ai.Player.Name + " is creating a drone");
		var commandBase = bases[index];
		commandBase.GetComponent<CreateUnitAction>().GetClickAction()();
	}
}
