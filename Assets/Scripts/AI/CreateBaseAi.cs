/// <summary>
/// Create base ai.
/// Determines when the AI should create a base.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBaseAi : AiBehaviour
{
	private AiSupport support = null;

	public float Cost = 200;
	public int UnitsPerBase = 5;

	// Can the drone build a base here
	public float RangeFromDrone = 30;
	public int TriesPerDrone = 3;

	public GameObject BasePrefab;

	// Decide if AI needs to creat a base
	public override float GetWeight ()
	{
		if (support == null) {
			support = AiSupport.GetSupport (gameObject);
		}
		if (support.Player.Credits < Cost || support.Drones.Count == 0) {
			return 0;
		}
		if (support.CommandBases.Count * UnitsPerBase <= support.Drones.Count) {
			return 1;
		}
		return 0;
	}

	public override void Execute ()
	{
		Debug.Log ("Creating base");

		var go = GameObject.Instantiate (BasePrefab);
		go.AddComponent<Player> ().Info = support.Player;

		foreach (var drone in support.Drones) {
			// How many tries per drone
			for (int i = 0; i < TriesPerDrone; i++) {
				// Start in drone position and change direction in random direction
				var pos = drone.transform.position;
				pos += Random.insideUnitSphere * RangeFromDrone;
				// Set height on terrain so random range doesn't include vertical
				pos.y = Terrain.activeTerrain.SampleHeight (pos);
				go.transform.position = pos;

				// Is this a safe place for base
				if (RtsManager.Current.IsGameObjectSafeToPlace (go)) {
					// Build a base
					support.Player.Credits -= Cost;
					return;
				}
			}
		}

		Destroy(go);
	}
}
