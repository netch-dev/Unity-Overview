using UnityEngine;
using UnityEngine.AI;

public class PlayerNavmesh : MonoBehaviour {
	[SerializeField] private Transform[] targets;
	private Transform currentTarget;
	private NavMeshAgent navMeshAgent;

	private void Awake() {
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void Update() {
		if (currentTarget == null || Vector3.Distance(transform.position, currentTarget.position) < 1) {
			currentTarget = targets[Random.Range(0, targets.Length)];
		}

		navMeshAgent.SetDestination(currentTarget.position);
	}
}
