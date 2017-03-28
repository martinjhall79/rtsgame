/// <summary>
/// Earnings.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earnings : MonoBehaviour
{
	public float CreditsPerSecond = 1;

	private PlayerSetupDefinition player;

	// Use this for initialization
	void Start ()
	{
		player = GetComponent<Player> ().Info;
	}
	
	// Update is called once per frame
	void Update ()
	{
		player.Credits += CreditsPerSecond * Time.deltaTime;
	}
}
