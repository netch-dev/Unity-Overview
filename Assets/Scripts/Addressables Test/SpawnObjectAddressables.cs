using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnObjectAddressables : MonoBehaviour {
	[SerializeField] private AssetReference assetReference;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.T)) {
			LoadAddressableViaReference();
		}
	}

	private void LoadAddressableViaReference() {
		assetReference.LoadAssetAsync<GameObject>().Completed += (asyncOperationHandle) => {
			if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded) {
				Instantiate(asyncOperationHandle.Result);
			} else {
				Debug.LogError("Failed to load asset");
			}
		};
	}

	// Shouldn't be used in production but it's possible to load an asset via a string path
	private void LoadAddressableViaString() {
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
