using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestAnimatorBlendTrees : MonoBehaviour {
	private Animator animator;
	private PlayerInputActions playerInputActions;

	private bool isCrouching;
	private float speedValue = 0f;
	private void Awake() {
		animator = GetComponent<Animator>();
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();

		playerInputActions.Player.Crouch.performed += CrouchPerformed;
		playerInputActions.Player.Sprint.performed += SprintPerformed;
		playerInputActions.Player.Sprint.canceled += SprintCancelled;
	}

	private void Update() {
		//BlendTreeExample_1();
		BlendTreeExample_2();
	}

	private void BlendTreeExample_1() {
		// Using the 1D blend type
		float speedValue = 0f;
		if (Input.GetKey(KeyCode.W)) {
			speedValue = 0.5f;
		}

		if (Input.GetKey(KeyCode.LeftShift)) {
			speedValue *= 2f;
		}

		animator.SetFloat("speed", speedValue, 0.1f, Time.deltaTime);
	}

	private void BlendTreeExample_2() {
		// Using the 2d directional blend type
		Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
		Debug.Log(inputVector);
		animator.SetFloat("sideMovement", inputVector.x, 0.1f, Time.deltaTime);
		animator.SetFloat("forwardMovement", inputVector.y, 0.1f, Time.deltaTime);
	}

	private void CrouchPerformed(InputAction.CallbackContext obj) {
		isCrouching = !isCrouching;
		animator.SetBool("isCrouching", isCrouching);
	}

	private void SprintPerformed(InputAction.CallbackContext obj) {
		speedValue = 1f;
		animator.SetFloat("speed", speedValue);
	}

	private void SprintCancelled(InputAction.CallbackContext obj) {
		speedValue = 0f;
		animator.SetFloat("speed", speedValue);
	}
}
