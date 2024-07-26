using TMPro;
using Unity.VisualScripting;
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

	// ---------------------------------
	// Pro Builder
	// ---------------------------------

	// - Built in tool for creating 3D models in Unity
	// - Install in the package manager -> Unity Registry -> ProBuilder

	// - Great for prototyping levels

	// - Hold shift with a face selected to loop cut

	// - Use the ProBuilderize button to convert a mesh to a ProBuilder mesh

	// ---------------------------------
	// Assembly Definitions
	// ---------------------------------

	// - Organize your scripts into assemblies to improve compile times
	// - Forces you to write better code with fewer dependencies
	// - Small projects don't need to worry about this, but as your project grows it can be useful

	// - To create an assembly:
	// -- Right click -> Create -> Assembly Definition
	// -- Make sure all of the included scripts are in the same folder
	// --- If you need to include scripts from other folders, you can use right click -> 'assembly definition reference' in the secondary folder. Then add the main assembly as the reference

	// - It's a good practice to put the scripts in their own namespace

	// - If you want scripts to access scripts in another assembly, you can add a reference to that assembly in the assembly definition file

	// ---------------------------------
	// Resources
	// ---------------------------------

	// - Simple way to load assets during runtime
	// - Unity loads all referenced objects in the scene, but sometimes you might not want to
	// -- For example if you have 1000 hats in the game, you don't want to load all of them at the start for faster loading times

	// - In order to load a resource, you need to put it in a folder named Resources
	// -- You can have subfolders within the Resources folder but you'll need to include the path when loading the resource

	// - Resources.Load is synchronous and will block the main thread
	// -- If you're loading a huge file consider using the async version
	// --- Resources.LoadAsync

	// - Every single asset that is in the Resources folder will be included in the build whether you use it or not
	// -- Use it only for the assets that you'll actually need

	public class ResourcesExample : MonoBehaviour {
		private void Start() {
			LoadSingleResourceExample();
			LoadMultipleExample();
		}

		private void LoadSingleResourceExample() {
			Transform examplePrefab = Resources.Load<Transform>("examplePrefab");
			Instantiate(examplePrefab);

			// To unload the asset from memory use:
			Resources.UnloadAsset(examplePrefab);

			// You can also use this which will automatically unload unused assets:
			Resources.UnloadUnusedAssets();
		}

		private void LoadMultipleExample() {
			// Put all of the resources in a folder within the Resources folder

			// Here the prefabs are in the examplePrefabs folder
			Transform[] examplePrefabs = Resources.LoadAll<Transform>("examplePrefabs");
			foreach (Transform t in examplePrefabs) {
				// Do something with the prefabs
				Instantiate(t);
			}
		}

		private void LoadAsyncExample() {
			// This will run in the background, and will let you know when the asset is loaded

			Resources.LoadAsync<Transform>("examplePrefab").completed += operation => {
				ResourceRequest resourceRequest = (ResourceRequest)operation;
				Instantiate(resourceRequest.asset);
			};
		}
	}
}
