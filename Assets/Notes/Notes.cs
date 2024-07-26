using TMPro;
using UnityEngine;
using UnityEngine.Video;
public class Notes : MonoBehaviour {
	// ---------------------------------
	// Text
	// ---------------------------------

	// - 1. 3D Text / UI Text
	// -- Old legacy component
	// -- Limited options - not recommended for new projects

	// - 2. TextMeshPro
	// -- New text component
	// -- Much more options
	// -- Ideally you should use TextMeshPro for all text
	public TextMeshPro textMeshPro; // World text
	public TextMeshProUGUI textMeshProUGUI; // UI text

	// ---------------------------------
	// Render Textures
	// ---------------------------------

	// - Render textures can render a camera view into a texture
	// -- Useful for minimaps, ingame tvs, security cameras, etc

	// - For minimaps use a raw image and set the texture to the render texture
	// -- Set the minimap camera to orthographic to make it 2D
	// --- Use the layer culling mask option on the camera to only render the minimap objects
	// ---- Then you can add sprites to the minimap layer to show the player, enemies, etc

	public void RenderTextureTest() {
		RenderTexture renderTexture = new RenderTexture(256, 256, 8);

		// Assigning a render texture to a camera:
		Camera.main.targetTexture = renderTexture;

		// to remove it:
		Camera.main.targetTexture = null;
	}

	// ---------------------------------
	// Video Player Component
	// ---------------------------------

	// - Useful for cutscenes, custom tutorials, showing combos, etc

	// - Supports all kinds of formats - mp4, webm, ogg, etc
	// -- It also supports URL links to videos

	// - The main option for imported videos is the Transcode option, enabling it will make Unity re-encode the video to a format that is supported by all platforms

	// - To render in fullscreen, set the video player's render mode to Camera Near Plane and set the camera to the main camera
	// -- Use the Direct audio output mode

	public void VideoPlayerTest() {
		VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
		videoPlayer.Play();

		int videoFramesPerSecond = 60;
		videoPlayer.frame = 10 * videoFramesPerSecond; // Go to 10 seconds
	}

	// ---------------------------------
	// Shader Graph
	// ---------------------------------

	// - The name for the primary texture is usually called MainTex and _MainTex for the reference

	public class ChangeShaderColourExample : MonoBehaviour {
		private Material material;

		private void Start() {
			material = GetComponent<Renderer>().material;
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.T)) {
				material.SetColor("_Color", Color.red);
			}
		}
	}

	// ---------------------------------
	// Trail Renderer
	// ---------------------------------

	// - Setting the material shader to unlit is recommended for trails
	// -- To use colours on a trail, set the material shader to particles/additive

	// - When working with trails in code you can use the Emitting property to enable/disable the trail
}
