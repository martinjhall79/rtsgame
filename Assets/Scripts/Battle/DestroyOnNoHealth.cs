/// <summary>
/// Destroy on no health.
/// Destroy units when they run out of health
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnNoHealth : MonoBehaviour
{
	public GameObject ExplosionPrefab;
	private ShowUnitInfo info;

	void Start ()
	{
		info = GetComponent<ShowUnitInfo> ();
	}
	
	// Destroy unit when it runs out of health
	void Update ()
	{
		if (info.CurrentHealth <= 0) {
			Destroy (this.gameObject);
			// Explosion effect
			GameObject.Instantiate (ExplosionPrefab, transform.position, Quaternion.identity);
		}

	}
}
