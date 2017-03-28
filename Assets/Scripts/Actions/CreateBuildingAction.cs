/// <summary>
/// Create building action.
/// </summary>
using UnityEngine;
using System.Collections;

public class CreateBuildingAction : ActionBehaviour
{
	public float Cost = 0;
	public GameObject BuildingPrefab;
	public float MaxBuildDistance = 30f;

	public GameObject GhostBuildingPrefab;
	private GameObject active = null;

	public override System.Action GetClickAction ()
	{
		return delegate() {
			var player = GetComponent<Player>().Info;
			if (player.Credits < Cost) {
				Debug.Log("Not enough, this costs " + Cost);
				return;
			}

			var go = GameObject.Instantiate (GhostBuildingPrefab);
			// Set info needed to place building
			var finder = go.AddComponent<FindBuildingSite>();
			finder.BuildingPrefab = BuildingPrefab;
			finder.MaxBuildDistance = MaxBuildDistance;
			finder.Info = player;
			finder.Source = transform;
			active = go;
		};
	}

	void Update ()
	{
		// Press esc to stop looking for a building site
		if (active == null) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameObject.Destroy (active);
		}
	}

	void OnDestroy ()
	{
		if (active == null) {
			return;
		}

		Destroy (active);
	}
}
