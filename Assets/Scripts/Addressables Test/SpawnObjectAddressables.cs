using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnObjectAddressables : MonoBehaviour {
	private void Update() {
		if (Input.GetKeyDown(KeyCode.T)) {
			Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Addressables Test/Environment.prefab").Completed +=
				(asyncOperationHandle) => {
					if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded) {
						Instantiate(asyncOperationHandle.Result);
					} else {
						Debug.LogError("Failed to load asset");
					}
				};
		}
	}

}
