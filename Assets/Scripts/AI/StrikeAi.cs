/// <summary>
/// Strike ai.
/// Decide when to launch an attack on the player. 
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeAi : AiBehaviour {

	public int DronesRequired = 10;
	// TODO tweak this during testing
	public float TimeDelay = 5;
	// How many units in the squad size
	public float SquadSize = 0.5f;
	public int IncreasePerWave = 10;

	public override void Execute ()
	{ 
		var ai = AiSupport.GetSupport(this.gameObject);
		// Log
		//Debug.Log (ai.Player.Name + " launching strike");

		// What's the current wave size
		int wave = (int)(ai.Drones.Count * SquadSize);
		DronesRequired += IncreasePerWave;

		// Find the location of the human player
		// TODO change this to a scouting system, where drones go out scouting the location of the player
		foreach (var p in RtsManager.Current.Players) {
			// Make sure we're not attacking another ai
			if (p.IsAi) {
				continue;
			}
			// Send the wave to the target
			for (int i = 0; i < wave; i++) {
				var drone = ai.Drones[i];
				var nav = drone.GetComponent<RightClickNavigation>();
				nav.SendToTarget(p.Location.position);
			}
			return;
		}
	}

	public override float GetWeight ()
	{
		// How much time has passed
		if (TimePassed < TimeDelay) {
			return 0;
		}
		TimePassed = 0;
		// Are there enough drones to attack
		var ai = AiSupport.GetSupport(this.gameObject);
		if (ai.Drones.Count < DronesRequired) {
			return 0;
		}
		// Time to attack
		return 1;
	}

}
