using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class WebRequestUtil {
	// StartCoroutine requires a MonoBehaviour to run
	private class WebRequestsMonoBehaviour : MonoBehaviour { }
	private static WebRequestsMonoBehaviour Instance;

	private static void Init() {
		if (Instance != null) return;
		GameObject gameObject = new GameObject("WebRequestUtil");
		Instance = gameObject.AddComponent<WebRequestsMonoBehaviour>();

	}

	public static void Get(string url, Action<string> onError, Action<string> onSuccess) {
		Init();
		Instance.StartCoroutine(GetCoroutine(url, onError, onSuccess));
	}

	public static void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess) {
		Init();
		Instance.StartCoroutine(GetTextureCoroutine(url, onError, onSuccess));
	}

	private static IEnumerator GetCoroutine(string url, Action<string> onError, Action<string> onSuccess) {
		using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url)) {
			yield return unityWebRequest.SendWebRequest();

			if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError) {
				onError(unityWebRequest.error);
				yield break;
			}

			onSuccess(unityWebRequest.downloadHandler.text);
		}
	}

	private static IEnumerator GetTextureCoroutine(string url, Action<string> onError, Action<Texture2D> onSuccess) {
		using (UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url)) {
			yield return unityWebRequest.SendWebRequest();

			if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError) {
				onError(unityWebRequest.error);
				yield break;
			}

			DownloadHandlerTexture downloadHandlerTexture = unityWebRequest.downloadHandler as DownloadHandlerTexture;
			onSuccess(downloadHandlerTexture.texture);
		}
	}
}
