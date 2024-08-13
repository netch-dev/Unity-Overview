using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnObjectAddressables : MonoBehaviour {
	private void Update() {
		if (Input.GetKeyDown(KeyCode.T)) {
			AsyncOperationHandle<GameObject> asyncOperationHandle =
				Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Addressables Test/Environment.prefab");

			asyncOperationHandle.Completed += AsyncOperationHandle_Completed;
		}
	}

	private void AsyncOperationHandle_Completed(AsyncOperationHandle<GameObject> obj) {
		if (obj.Status == AsyncOperationStatus.Succeeded) {
			Instantiate(obj.Result);
		} else {
			Debug.LogError("Failed to load asset");
		}
	}
}
