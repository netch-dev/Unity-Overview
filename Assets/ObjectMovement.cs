using UnityEngine;

public class ObjectMovement : MonoBehaviour {
	// Simple script that moves an object around in a circle to test the trails
	public float speed = 1f;
	public float radius = 5f;
	public float height = 1f;

	private float angle = 0.0f;
	private Vector3 startingPosition;
	private void Awake() {
		startingPosition = transform.position;
	}

	private void FixedUpdate() {
		angle += speed * Time.deltaTime;
		float x = Mathf.Cos(angle) * radius;
		float y = Mathf.Sin(angle) * radius;
		float z = Mathf.Sin(angle) * height;
		transform.position = startingPosition + new Vector3(x, y, z);
	}
}
