using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class TestingWebRequests : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI text;

	private void Start() {
		Debug.Log("TestingWebRequests.Start()");
		string url = "https://google.com";
		StartCoroutine(Get(url));
	}

	private IEnumerator Get(string url) {
		using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url)) {
			yield return unityWebRequest.SendWebRequest();

			if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError) {
				Debug.LogError(unityWebRequest.error);
				text.SetText(unityWebRequest.error);
				yield break;
			}

			Debug.Log(unityWebRequest.downloadHandler.text);
			text.SetText(unityWebRequest.downloadHandler.text);
		}
	}
}
