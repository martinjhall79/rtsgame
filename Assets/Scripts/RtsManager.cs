using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;


public class RtsManager : MonoBehaviour
{

	public static RtsManager Current = null;

	public List<PlayerSetupDefinition> Players = new List<PlayerSetupDefinition> ();

	public TerrainCollider MapCollider;

	public Vector3? ScreenPointToMapPosition (Vector2 point)
	{
		var ray = Camera.main.ScreenPointToRay (point);
		RaycastHit hit;
		if (!MapCollider.Raycast (ray, out hit, Mathf.Infinity))
			return null;

		return hit.point;
	}

	public RtsManager()
	{
		Current = this;
	}

	// Is the base model safe to place
	public bool IsGameObjectSafeToPlace (GameObject go)
	{
		// Get all of the base model vertices to compare against gameworld
		var verts = go.GetComponent<MeshFilter>().mesh.vertices;

		var obstacles = GameObject.FindObjectsOfType<NavMeshObstacle>();
		var colliders = new List<Collider>();
		foreach (var o in obstacles) {
			if (o.gameObject != go) {
				colliders.Add(o.gameObject.GetComponent<Collider>());
			}
		}

		foreach (var v in verts) {
			// Vertices position in relation to gameworld, rather than local position
			NavMeshHit hit;
			var vRealWorld = go.transform.TransformPoint(v);
			NavMesh.SamplePosition(vRealWorld, out hit, 20, NavMesh.AllAreas);

			// Are we valid on the x and z
			bool onXAxis = Mathf.Abs(hit.position.x - vRealWorld.x) < 0.5f;
			bool onZAxis = Mathf.Abs(hit.position.z - vRealWorld.z) < 0.5f;
			// Are we hitting anything
			bool hitCollider = colliders.Any(c => c.bounds.Contains(vRealWorld));

			if (!onXAxis || !onZAxis || hitCollider) {
				return false;
			}
		}

		// Success - there's no reason that the location is invalid
		return true;
	}

	// Use this for initialization
	void Start ()
	{
		foreach (var p in Players) {
			foreach (var u in p.StartingUnits) {
				var go = (GameObject)GameObject.Instantiate (u, p.Location.position, p.Location.rotation);

				var player = go.AddComponent<Player> ();
				player.Info = p;
				if (!p.IsAi) {
					if (Player.Default == null)
						Player.Default = p;
					go.AddComponent<RightClickNavigation> ();
					go.AddComponent<ActionSelect> ();
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
