using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimatorBlendTrees : MonoBehaviour {
	private Animator animator;
	private void Awake() {
		animator = GetComponent<Animator>();
	}

	private void Update() {
		float speedValue = 0f;
		if (Input.GetKey(KeyCode.W)) {
			speedValue = 0.5f;
		}

		if (Input.GetKey(KeyCode.LeftShift)) {
			speedValue *= 2f;
		}

		animator.SetFloat("speed", speedValue, 0.1f, Time.deltaTime);
	}
}
