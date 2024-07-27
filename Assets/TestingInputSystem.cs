using UnityEngine;
using UnityEngine.InputSystem;

#region Using Generated C# Class
public class TestingInputSystem : MonoBehaviour {
	private Rigidbody rb;
	private PlayerInputActions playerInputActions;

	private void Awake() {
		rb = GetComponent<Rigidbody>();

		playerInputActions = new PlayerInputActions();

		// Player action map
		playerInputActions.Player.Enable();
		playerInputActions.Player.Jump.performed += ctx => Jump(ctx);

		// UI action map
		playerInputActions.UI.Submit.performed += ctx => Submit(ctx);

		LoadUserBinds();
		RebindKeyExample();
	}

	private void FixedUpdate() {
		if (playerInputActions.Player.enabled) {
			Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
			float speed = 5f;
			Vector3 movement = new Vector3(inputVector.x, 0, inputVector.y) * speed;
			rb.AddForce(movement, ForceMode.Force);
		}

		// Switch to the UI action map
		if (Keyboard.current.tKey.wasPressedThisFrame) {
			playerInputActions.Player.Disable();
			playerInputActions.UI.Enable();
		}

		// Switch to the player action map
		if (Keyboard.current.yKey.wasPressedThisFrame) {
			playerInputActions.UI.Disable();
			playerInputActions.Player.Enable();
		}
	}

	private void Jump(InputAction.CallbackContext ctx) {
		Debug.Log("Jump! " + ctx.phase);
		rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
	}

	private void Submit(InputAction.CallbackContext ctx) {
		Debug.Log("Submit! " + ctx.phase);
	}

	private void RebindKeyExample() {
		// Disable the map first
		playerInputActions.Player.Disable();

		// Listen for the next input and assign it
		playerInputActions.Player.Jump.PerformInteractiveRebinding()
			.OnMatchWaitForAnother(0.5f) // Wait for another input if the first one is not valid	
			.WithControlsExcluding("Mouse") // Disallow rebinds from the mouse
			.OnComplete(callback => {
				Debug.Log("Rebind complete: " + callback.action.bindings[0].overridePath);
				SaveUserBinds();
				playerInputActions.Player.Enable();
				callback.Dispose();
			})
			.Start();
	}

	// Save and load the users rebinds
	private void SaveUserBinds() {
		string rebinds = playerInputActions.SaveBindingOverridesAsJson();
		PlayerPrefs.SetString("rebinds", rebinds);
	}

	private void LoadUserBinds() {
		string rebinds = PlayerPrefs.GetString("rebinds");
		playerInputActions.LoadBindingOverridesFromJson(rebinds);
	}
}
#endregion

#region Using Player Input Component (Unity Events and C# Events)
/*public class TestingInputSystemComponent : MonoBehaviour {
	private Rigidbody rb;
	private PlayerInput playerInput;
	private void Awake() {
		rb = GetComponent<Rigidbody>();

		// 2. PlayerInput component behavior set to use C# events:
		playerInput = GetComponent<PlayerInput>();
		playerInput.onActionTriggered += OnActionTriggered;
		playerInput.actions["Jump"].performed += JumpWithCSharpEvent;
	}

	// Change between Player and UI action maps:
	private void SwitchToActionMap(string actionMapID) {
		playerInput.SwitchCurrentActionMap(actionMapID);
	}

	// 1. This method is called from the PlayerInput component that is using unity events behavior
	public void Jump(InputAction.CallbackContext ctx) {
		Debug.Log("Jump! " + ctx.phase);
		if (ctx.performed) {
			rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
		}
	}

	// 2. PlayerInput component behavior set to use C# events:
	private void OnActionTriggered(InputAction.CallbackContext ctx) {
		// This event is triggered for all actions on all action maps
		Debug.Log("Action triggered: " + ctx.action.name + " " + ctx.phase);
	}

	private void JumpWithCSharpEvent(InputAction.CallbackContext ctx) {
		Debug.Log("Jump via C# event! " + ctx.phase);
		if (ctx.performed) {
			rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
		}
	}
}*/
#endregion