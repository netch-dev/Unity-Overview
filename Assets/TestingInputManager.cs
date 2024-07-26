using System;
using UnityEngine;

// Legacy input manager
public class TestingInputManager : MonoBehaviour {

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Left mouse button clicked");
		}

		if (Input.GetKeyDown(KeyCode.T)) {
			Debug.Log("T key pressed");
		}
	}
}
