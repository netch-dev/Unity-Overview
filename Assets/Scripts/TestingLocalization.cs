#if UNITY_EDITOR
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class TestLocalization : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI helloPlayerNameTextMesh;
	[SerializeField] private LocalizedString playerNameLocalizedString;

	private bool isTestingLanguages;
	private float testingLanguagesTimer;

	public enum Language {
		English,
		French,
		Spanish,
	}
	private void Start() {
		UpdateText();

		LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_OnSelectedLocaleChanged;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.T)) {
			isTestingLanguages = true;
		}

		if (Input.GetKeyDown(KeyCode.Y)) {
			isTestingLanguages = false;
		}

		if (isTestingLanguages) {
			testingLanguagesTimer -= Time.unscaledDeltaTime;
			if (testingLanguagesTimer <= 0) {
				testingLanguagesTimer = 0.5f;
				Language currentLanguage = (Language)LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
				Language nextLanguage = (Language)(((int)currentLanguage + 1) % System.Enum.GetValues(typeof(Language)).Length);
				SetLanguage(nextLanguage);
			}
		}
	}

	private void LocalizationSettings_OnSelectedLocaleChanged(Locale obj) {
		UpdateText();
	}

	private void UpdateText() {
		helloPlayerNameTextMesh.text = playerNameLocalizedString.GetLocalizedString("Netch Dev");
	}

	private void SetLanguage(Language language) {
		LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)language];
	}
}
#endif