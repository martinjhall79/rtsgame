/// <summary>
/// Ai controller.
/// Find the most valuable AI behaviour and execute that behaviour.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
	// Which AI player are we are using
	public string PlayerName;
	// Randomness to AI decision making
	public float Confusion = 0.3f;
	// Slow decision making to optimise performance
	public float Frequency = 1;

	private PlayerSetupDefinition player;
	private float waited = 0;
	private List<AiBehaviour> Ais = new List<AiBehaviour> ();

	public PlayerSetupDefinition Player { get { return player; } }

	void Start ()
	{
		// Get list of AI's added to this controller
		foreach (var ai in GetComponents<AiBehaviour>()) {
			Ais.Add (ai);
		}
		// Which player are we using
		foreach (var p in RtsManager.Current.Players) {
			if (p.Name == PlayerName) {
				player = p;
			}
		}
		// Set the player info and add AISupport
		gameObject.AddComponent<AiSupport> ().Player = player;
	}

	void Update ()
	{
		// Have we waited long enough
		waited += Time.deltaTime;
		if (waited < Frequency) {
			return;
		}

		// Debug log
		string aiLog = "";

		// Decide which AI in the list is most important
		// Start with the least valuable and sort from there
		float bestAiValue = float.MinValue;
		AiBehaviour bestAi = null;
		// Get the latest info
		AiSupport.GetSupport (gameObject).Refresh ();
		// What's this AI worth
		foreach (var ai in Ais) {
			ai.TimePassed += waited;
			var aiValue = ai.GetWeight () * ai.WeightMultiplier + Random.Range (0, Confusion);
			// Debug log
			aiLog += ai.GetType ().Name + ": " + aiValue + "\n";
			// Is this AI value better than the current best
			if (aiValue > bestAiValue) {
				// Found new most valuable AI value
				bestAiValue = aiValue;
				bestAi = ai;
			}
		}
		// Debug log
		Debug.Log (aiLog);
		// This is the best AI, execute
		bestAi.Execute ();
		waited = 0;
	}
}
