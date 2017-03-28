/// <summary>
/// Ai support.
/// Gathers the info needed to support the AI.
/// Drone and unit counts for each individual AI controller and their behaviours.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSupport : MonoBehaviour
{
	// Manage drones and command bases
	public List<GameObject> Drones = new List<GameObject>();
	public List<GameObject> CommandBases = new List<GameObject>();

	public PlayerSetupDefinition Player = null;

	// Simplify process of getting components
	public static AiSupport GetSupport(GameObject go)
	{
		return go.GetComponent<AiSupport>();
	}

	public void Refresh()
	{
		Drones.Clear();
		CommandBases.Clear();
		foreach (var u in Player.ActiveUnits) {
			if (u.name.Contains("Drone")) Drones.Add(u);
			if (u.name.Contains("Command Base")) CommandBases.Add(u);
		}
	}

}
