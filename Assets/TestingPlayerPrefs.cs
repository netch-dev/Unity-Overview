using UnityEngine;
public class TestingPlayerPrefs : MonoBehaviour {

	private void Awake() {
		PlayerPrefs.SetInt("PlayerLevel", -1);

		Debug.Log("Player Level: " + PlayerPrefs.GetInt("PlayerLevel")); // -1

		Debug.Log($"playerLevel: {PlayerPrefs.HasKey("playerLevel")}"); // false
		Debug.Log($"PlayerLevel: {PlayerPrefs.HasKey("PlayerLevel")}"); // true

		// You can force save the player prefs incase the game crashes, or if you don't want to read a cached value
		PlayerPrefs.Save();

		// If you need more complexity, you can convert a class to a json string, and save the string
		SaveObject saveObject = new SaveObject {
			playerName = "Netch",
			playerLevel = 1337,
			playerPosition = new Vector3(1, 2, 3)
		};
		PlayerPrefs.SetString("SaveObject", JsonUtility.ToJson(saveObject));

		// You can then convert the json string back to a class
		SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(PlayerPrefs.GetString("SaveObject"));
	}

	// Since playerprefs doesnt have a bool, we can store it as an int
	private void SetBool(string key, bool value) {
		PlayerPrefs.SetInt(key, value ? 1 : 0);
	}

	private bool GetBool(string key) {
		return PlayerPrefs.GetInt(key) == 1;
	}

	[System.Serializable]
	public class SaveObject {
		public string playerName;
		public int playerLevel;
		public Vector3 playerPosition;
	}
}
