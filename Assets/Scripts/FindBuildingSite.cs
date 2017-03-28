/// <summary>
/// Find building site.
/// Transparent building image follows mouse when looking for site to position base.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBuildingSite : MonoBehaviour
{
	public float Cost = 200;
	// Place the base
	public float MaxBuildDistance = 30;
	public GameObject BuildingPrefab;
	public PlayerSetupDefinition Info;
	// The drone
	public Transform Source;

	// Colour effect on ghost building image, showing where we can build
	Renderer rend;
	Color Red = new Color (1, 0, 0, 0.5f);
	Color Green = new Color (0, 1, 0, 0.5f);

	void Start ()
	{
		// Disable mouse clicks if we're about to place a base
		MouseManager.Current.enabled = false;
		rend = GetComponent<Renderer> ();
	}

	// Add the building
	void Update ()
	{
		var tempTarget = RtsManager.Current.ScreenPointToMapPosition (Input.mousePosition);
		if (tempTarget.HasValue == false)
			return;
		transform.position = tempTarget.Value;

		// How far is the distance from the drone to the base, is it in range
		if (Vector3.Distance(transform.position, Source.position) > MaxBuildDistance) {
			rend.material.color = Red;
			return;
		}

		// Whether we can build here
		if (RtsManager.Current.IsGameObjectSafeToPlace (gameObject)) {
			rend.material.color = Green;
			// Building ready to place
			if (Input.GetMouseButtonDown(0)) {
				var go = GameObject.Instantiate(BuildingPrefab);
				go.AddComponent<ActionSelect>();
				go.transform.position = transform.position;
				Info.Credits -= Cost;
				go.AddComponent<Player>().Info = Info;
				Destroy(this.gameObject);
			}
		} else {
			rend.material.color = Red;
		}
	}

	// Reenable when done building a base
	void OnDestroy()
	{
		MouseManager.Current.enabled = true;
	}
}
