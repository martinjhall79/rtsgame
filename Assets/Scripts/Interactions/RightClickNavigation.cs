using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class RightClickNavigation : Interaction {

	public float RelaxDistance = 5;

	private NavMeshAgent agent;
	private Vector3 target = Vector3.zero;
	private bool selected = false;
	private bool isActive = false;

	public override void Deselect ()
	{
		selected = false;
	}

	public override void Select ()
	{
		selected = true;
	}

	// Send ai drones to player to attack
	public void SendToTarget(Vector3 pos)
	{
		target = pos;
		SendToTarget();
	}

	public void SendToTarget()
	{
		agent.SetDestination (target);
		agent.Resume ();
		isActive = true;
	}
		
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}

	void Update () {
		if (selected && Input.GetMouseButtonDown (1)) {
			var tempTarget = RtsManager.Current.ScreenPointToMapPosition(Input.mousePosition);
			if (tempTarget.HasValue) {
				target = tempTarget.Value;
				SendToTarget();
			}
		}

		if (isActive && Vector3.Distance (target, transform.position) < RelaxDistance) {
			agent.Stop ();
			isActive = false;
		}
	}
}
