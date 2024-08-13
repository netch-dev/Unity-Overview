using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestingWebRequests : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI textMesh;
	[SerializeField] private RawImage rawImage;

	private void Start() {
		/*		string url = "https://google.com";
				WebRequestUtil.Get(url, (string error) => {
					Debug.LogError(error);
					textMesh.SetText(error);
				}, (string text) => {
					textMesh.SetText(text);
				});*/

		string url = "https://i.netch.dev/uploads/e22d313b-49a9-4c4a-bcee-d6569d54664d.png";
		WebRequestUtil.GetTexture(url, (string error) => {
			Debug.LogError(error);
			textMesh.SetText(error);
		}, (Texture2D texture) => {
			textMesh.SetText("Success!");
			rawImage.texture = texture;
		});
	}


}
