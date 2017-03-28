using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateUnitAction : ActionBehaviour {

	public GameObject Prefab;
	public float Cost = 0;
	private PlayerSetupDefinition player;

	// Use this for initialization
	void Start () {
		player = GetComponent<Player>().Info;
	}

	public override Action GetClickAction ()
	{
		return delegate() {
			if (player.Credits < Cost)
			{
				Debug.Log("Can't create, it costs " + Cost);
				return;
			}
			// Add a drone
			var go = (GameObject)GameObject.Instantiate(
				Prefab,
				transform.position,
				Quaternion.identity
			);
			go.AddComponent<Player>().Info = player;
			go.AddComponent<RightClickNavigation>();
			go.AddComponent<ActionSelect>();
			player.Credits -= Cost;
		};
	}
}
