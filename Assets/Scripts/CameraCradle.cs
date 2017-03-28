using UnityEngine;
using System.Collections;

public class CameraCradle : MonoBehaviour
{

	public float Speed = 20;
	// Follow the player
	public float height = 80;

	// Use this for initialization
	void Start ()
	{
		// Find the human player and set the camera position there
		foreach (var p in RtsManager.Current.Players) {
			if (p.IsAi) {
				continue;
			}

			var pos = p.Location.position;
			pos.y = height;
			transform.position = pos;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (
			Input.GetAxis ("Horizontal") * Speed * Time.deltaTime,
			Input.GetAxis ("Vertical") * Speed * Time.deltaTime,
			0);
	}
}
