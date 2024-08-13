using UnityEngine;

public class CharacterSpawnRagdoll : MonoBehaviour {
	[SerializeField] private Transform ragdollPrefab;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			SpawnRagdoll();
		}
	}

	private void SpawnRagdoll() {
		Transform ragdoll = Instantiate(ragdollPrefab, transform.position, transform.rotation);
		MatchAllChildTransforms(transform, ragdoll);
		ApplExplosionToRagdoll(ragdoll, 500f, new Vector3(0f, 0.85f, -0.38f), 10f);
		//ragdoll.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
		Destroy(gameObject);
	}

	private void MatchAllChildTransforms(Transform root, Transform clone) {
		foreach (Transform child in root) {
			Transform cloneChild = clone.Find(child.name);
			if (cloneChild) {
				cloneChild.position = child.position;
				cloneChild.rotation = child.rotation;

				MatchAllChildTransforms(child, cloneChild);
			}
		}
	}

	private void ApplExplosionToRagdoll(Transform root, float force, Vector3 explosionPosition, float explosionRange) {
		foreach (Rigidbody rb in root.GetComponentsInChildren<Rigidbody>()) {
			rb.AddExplosionForce(force, explosionPosition, explosionRange);
		}
	}
}
