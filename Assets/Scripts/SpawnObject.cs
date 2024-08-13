using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

	[SerializeField] private Transform prefab;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.T)) {
			Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
		}
	}
}
