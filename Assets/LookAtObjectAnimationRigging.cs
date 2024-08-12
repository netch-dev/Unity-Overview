using UnityEngine;
using UnityEngine.Animations.Rigging;

public class LookAtObjectAnimationRigging : MonoBehaviour {

	private Rig rig;
	private float targetWeight;
	private void Awake() {
		rig = GetComponent<Rig>();
	}

	private void Update() {
		rig.weight = Mathf.Lerp(rig.weight, targetWeight, Time.deltaTime * 10f);

		if (Input.GetKeyDown(KeyCode.Space)) {
			targetWeight = targetWeight == 0f ? 1f : 0f;
		}
	}
}
