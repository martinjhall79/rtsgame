/// <summary>
/// Attack in range.
/// AI shooting capability.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInRange : MonoBehaviour
{
	// Indicate a shot hit
	public GameObject ImpactVisual;
	// Time to wait before finding new target
	public float FindTargetDelay = 1;
	public float AttackRange = 20;
	// Attack delay - 4 shots per second
	public float AttackFrequency = 0.25f;
	// How much damage for every shot
	public float AttackDamage = 1;

	// What we're shooting at
	private ShowUnitInfo target;
	private PlayerSetupDefinition player;
	// Manage time that's passed since last shot
	private float findTargetCounter = 0;
	private float attackCounter = 0;

	// Use this for initialization
	void Start ()
	{
		player = GetComponent<Player> ().Info;
	}

	// Called when enough time has passed to get a new target
	void FindTarget ()
	{
		// Already got a target
		if (target != null) {
			if (Vector3.Distance(transform.position, target.transform.position) > AttackRange) {
				return;
			}
		}
		// Find player to attack
		foreach (var p in RtsManager.Current.Players) {
			// Don't attack own players units
			if (p == player) {
				continue;
			}
			// Find player units that are close enough to attack and check the distance
			foreach (var unit in p.ActiveUnits) {
				if (Vector3.Distance (unit.transform.position, transform.position) < AttackRange) {
					//Debug.Log("Target in range");
					// Target acquired, get access to player's health
					target = unit.GetComponent<ShowUnitInfo> ();
					return;
				}
			}
		}
		target = null;
	}

	// Target acquired, now attack
	void Attack ()
	{
		if (target == null) {
			return;
		}
		// If target moved too far away now, we can't attack, wait for next opportunity to find new target
		// TODO add upgrade to ai units, increase speed to find new targets
		if (Vector3.Distance (target.transform.position, transform.position) > AttackRange) {
			target = null;
			return;
		}
		// Attack
		//Debug.Log("Shooting");
		target.CurrentHealth -= AttackDamage;
		GameObject.Instantiate (ImpactVisual, target.transform.position, Quaternion.identity);
	}
	
	// Find targets to attack and attack
	void Update ()
	{
		findTargetCounter += Time.deltaTime;
		if (findTargetCounter > FindTargetDelay) {
			FindTarget ();
			findTargetCounter = 0;
		}

		attackCounter += Time.deltaTime;
		if (attackCounter > AttackFrequency) {
			Attack ();
			attackCounter = 0;
		}
	}
}
