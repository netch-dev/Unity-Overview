using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[System.Serializable]
public class AssetReferenceAudioClip : AssetReferenceT<AudioClip> {
	public AssetReferenceAudioClip(string guid) : base(guid) { }
}

public class SpawnObjectAddressables : MonoBehaviour {
	[SerializeField] private AssetReferenceGameObject assetReferenceGameObject;

	[SerializeField] private AssetLabelReference assetLabelReference;

	[SerializeField] private AssetLabelReference specialSpritesLabelReference;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.T)) {
			InstantiateAddressableDirectly();
			LoadAddressablesFolder();
		}
	}

	private void LoadAddressablesFolder() {
		Addressables.LoadAssetsAsync<Sprite>(specialSpritesLabelReference, (sprite) => {
			Debug.Log(sprite);
		});
	}

	private void InstantiateAddressableDirectly() {
		assetReferenceGameObject.InstantiateAsync();

		// This also has a Completed event that can be used to check if the asset was loaded successfully
	}

	private void LoadAddressableViaLabel() {
		Addressables.LoadAssetAsync<GameObject>(assetLabelReference).Completed += (asyncOperationHandle) => {
			if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded) {
				Instantiate(asyncOperationHandle.Result);
			} else {
				Debug.LogError("Failed to load asset");
			}
		};
	}

	private void LoadAddressableViaReference() {
		assetReferenceGameObject.LoadAssetAsync<GameObject>().Completed += (asyncOperationHandle) => {
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
