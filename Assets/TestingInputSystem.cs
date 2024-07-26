using System;
using UnityEngine;

public class TestingInputSystem : MonoBehaviour {
	private void Awake() {
		PlayerInputActions playerInputActions = new PlayerInputActions();
		playerInputActions.Enable();
		playerInputActions.Player.Shoot.performed += ctx => Debug.Log("Shoot performed");
	}
}
