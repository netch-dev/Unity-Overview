using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.Video;
public class Notes : MonoBehaviour {
	#region Text
	// ---------------------------------
	// 
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
	#endregion

	#region Render Textures

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
	#endregion

	#region Video Player Component

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
	#endregion

	#region Shader Graph
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
	#endregion

	#region Trail Renderer

	// - Setting the material shader to unlit is recommended for trails
	// -- To use colours on a trail, set the material shader to particles/additive

	// - When working with trails in code you can use the Emitting property to enable/disable the trail
	#endregion

	#region Pro Builder
	// - Built in tool for creating 3D models in Unity
	// - Install in the package manager -> Unity Registry -> ProBuilder

	// - Great for prototyping levels

	// - Hold shift with a face selected to loop cut

	// - Use the ProBuilderize button to convert a mesh to a ProBuilder mesh
	#endregion

	#region Assembly Definitions

	// - Organize your scripts into assemblies to improve compile times
	// - Forces you to write better code with fewer dependencies
	// - Small projects don't need to worry about this, but as your project grows it can be useful

	// - To create an assembly:
	// -- Right click -> Create -> Assembly Definition
	// -- Make sure all of the included scripts are in the same folder
	// --- If you need to include scripts from other folders, you can use right click -> 'assembly definition reference' in the secondary folder. Then add the main assembly as the reference

	// - It's a good practice to put the scripts in their own namespace

	// - If you want scripts to access scripts in another assembly, you can add a reference to that assembly in the assembly definition file
	#endregion

	#region Resources
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
	#endregion

	#region Script Execution Order
	// - If you need to run a script before another script, you can set the execution order

	// - Open edit -> project settings -> script execution order
	// -- Scripts that arent set here will run in the default time
	// -- You can drag and drop the scripts to change the order

	// - It's better to use this sparingly, as it can make the project harder to maintain
	// -- It usually can be resolved by using Awake() to initialize objects and Start() to set up references
	#endregion

	#region Unity Logs
	// - If the editor crashes you can check the logs here - \AppData\Local\Unity\Editor\

	// - You can see logs from game builds here - \AppData\LocalLow\CompanyName\GameName\
	#endregion

	#region Player Prefs
	// - A simple way to store player data
	// - Great for prototypes, but not recommended for production
	// - Useful for simple settings, like storing if the game is fullscreen, last resolution, etc
	#endregion

	#region Legacy Input Manager vs Input System
	// - Legacy Input Manager:
	// -- Simple to use, good for prototypes
	public class TestingInputManager : MonoBehaviour {

		private void Update() {
			if (Input.GetMouseButtonDown(0)) {
				Debug.Log("Left mouse button clicked");
			}

			if (Input.GetKeyDown(KeyCode.T)) {
				Debug.Log("T key pressed");
			}
		}
	}

	// - Input System:
	// - Creates a layer of abstraction between the input and the game actions
	// -- This is great because its easy to swap out input types, like keyboard, controller, touch screens, etc

	// - Action Maps
	// -- Organize actions into groups, like player, vehicles, ui while paused, etc
	// -- Contains a list of individual actions 

	// - Individual Actions
	// -- Actions like jump, shoot, move, etc
	// -- Set the bindings for the action, like keyboard, controller, etc

	// - Action properties
	// -- Use Value for continuous inputs, like movement via joystick
	// -- Use Button for discrete inputs, like jump, shoot, etc
	// -- Use Pass Through for reading input from every device at once

	// - To use the input system:
	// - First create a new input action asset
	// -- Then assign the action maps and actions within that asset
	// --- After that generate the C# class by clicking on the asset and clicking on the generate C# class toggle
	// --- Finally you can reference that class in your script:
	public class TestingInputSystem : MonoBehaviour {
		private void Awake() {
			PlayerInputActions playerInputActions = new PlayerInputActions();

			// This will enable all of the action maps
			//playerInputActions.Enable(); 

			// You can also enable individual action maps
			playerInputActions.Player.Enable();
			playerInputActions.Player.Shoot.performed += ctx => Debug.Log("Shoot performed");
		}

		private void Update() {
			// The input system also has basic classes for accessing the input:
			// Useful for testing but not recommended for production
			if (Mouse.current.leftButton.wasPressedThisFrame) {
				Debug.Log("Left mouse button clicked");
			}

			if (Keyboard.current.tKey.wasPressedThisFrame) {
				Debug.Log("T key pressed");
			}
		}
	}

	// --- Or instead of the class reference, you can use the Player Input component to assign the input actions
	#endregion

	#region Animation Component vs Animator Component
	// - Animation Component:
	// -- Simple to use legacy component
	// -- Great for simple state objects that only need one animation

	// - Animator Component:
	// -- More complex, but more powerful
	#endregion

	#region Animation Window
	// - Usually when you make an animation you only want to move the child objects

	// - Animation properties are saved with the exact gameobject name

	// - Imported animations that are read only can be edited by duplicating the animation with CTRL-D

	// - Animation events can be used to time actions with animations
	// -- The script that has the action must be attached to the same object that has the animator
	#endregion

	#region Animator
	// - Transitions:
	// -- Has exit time: Automatically trigger the transition when the previous animation is complete or near complete
	// -- Exit time: When to transition to the next state, in percentage

	// - Instead of using a string to set parameters, use the id. Less brittle a generates less garbage
	// Replace animator.SetBool("vertical", true);
	// with:
	// int verticalId = Animator.StringToHash("vertical");
	// animator.SetBool(verticalId, true);

	// - Use the 'animation controller override' object to create variations of that logic. For example your enemy logic can have two controllers, one for the normal enemy and one for a large enemy
	#endregion

	#region Animation Blend Trees
	// - Used to blend multiple animations based on parameters
	// -- Great for handling separate animations per direction

	// - Helps blend animations like Idle -> Run || Walk Left -> Walk Front

	// - Blend Types:
	// -- 1D: One parameter
	// -- 2D Simple Directional: One animation per direction
	// -- 2D Freeform Directional: Multiple animations per direction
	// -- 2D Freeform Cartesian: Used when the animations are not directional
	#endregion

	#region Animation Avatar
	// - Useful for retargeting animations to different characters
	// -- All you need to do is assign the avatar for that specific mesh to the animator

	// - The muscles settings can be used to create movement constraints
	// -- For example, you can limit the movement on a heavily armoured character so the mesh doesn't clip

	// - In the animator you can use multiple layers and apply an avatar mask
	// -- The avatar mask will only apply the animation to the selected bones
	// --- For example a sword swing animation will only affect the arm/upper body bones
	// ---- The transform section within the avatar mask only works on non-humanoid rigs, or any extra bones that are not part of the humanoid rig
	#endregion

	#region Animation Rigging
	// - A package for adding dynamic modifications on top of static animations
	// -- For example having a standard animation and making the head look at something in the scene

	// - To setup a character for animation rigging:
	// - Selected the character and go click the 'Animation Rigging' menu
	// -- Then click 'Rig Setup'
	// --- Optionally you can click the 'Bone Renderer Setup' for a visual representation of the bones

	// - The order the rigs are applied is the order in which they are in the hierarchy
	// -- For the weapon aiming rig, the hand rig should be after the body rig so it overrides the body rig
	#endregion

	#region RawImage Vs Image, Sprite Vs Texture
	// - RawImage:
	// -- Used for displaying textures

	// - Image:
	// -- Used for displaying sprites

	public class TestingSpriteTextures : MonoBehaviour {
		[SerializeField] private Texture2D texture2D;
		[SerializeField] private Sprite sprite;

		private void Awake() {
			// Sprites
			Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
			GetComponent<Image>().sprite = sprite;

			// Texture
			GetComponent<RawImage>().texture = texture2D;
		}
	}
	#endregion

	#region UnityWebRequests - Contact External Servers
	// - Useful for fetching data that might change after the game has been built
	// -- Patch notes, leaderboards, workshop items, etc

	// - The UnityWebRequest implements IDisposable, so you should use it in a using statement

	// - UnityWebRequestTexture can be used to download images
	// -- returns a Texture2D
	#endregion

	#region Ragdolls
	// - To create a ragdoll:
	// -- Selected the character and click 3D object -> Ragdoll

	// - They should be spawned on death, not always in the scene for performance reasons
	// -- Use this recursive function to match the transforms of the ragdoll to the original character:
	/*
	private void MatchAllChildTransforms(Transform root, Transform clone) {
		foreach (Transform child in root) {
			Transform cloneChild = clone.Find(child.name);
			if (cloneChild) {
				cloneChild.position = child.position;
				cloneChild.rotation = child.rotation;

				MatchAllChildTransforms(child, cloneChild);
			}
		}
	}
	*/
	#endregion

	#region Addressables
	// - A package for managing assets in the project
	// -- Useful for loading assets during runtime
	// --- Keeps the loading times and memory footprint low by loading assets when needed
	// ---- Without using addressables, all referenced assets in the scene are loaded into memory

	// - Addressables are entirely asynchronous

	// - Scene changes release all of the assets in the previous scene

	// - They can also be unloaded manually:
	// -- Addressables.ReleaseInstance(gameObject);
	// -- Addressables.Release(assetReference);

	// - To use Addressables:
	// -- Open the window -> Window -> Asset Management -> Addressables
	// -- Create a new group
	// -- Drag and drop the assets or folders into the group
	// -- Create an asset reference in your script - [SerializeField] private AssetReference assetReference;
	// -- Spawn the asset using the LoadAssetAsync method:
	/*
	 private void LoadAddressableViaReference() {
		assetReference.LoadAssetAsync<GameObject>().Completed += (asyncOperationHandle) => {
			if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded) {
				Instantiate(asyncOperationHandle.Result);
			} else {
				Debug.LogError("Failed to load asset");
			}
		};
	 }
	 */

	// - Using AssetReference can be error prone since it can be of many types
	// -- To enforce a specific type you can use one of the included types like AssetReferenceGameObject, AssetReferenceSprite
	// --- You can also create your own types by inheriting from AssetReferenceT
	[System.Serializable]
	public class AssetReferenceAudioClip : AssetReferenceT<AudioClip> {
		public AssetReferenceAudioClip(string guid) : base(guid) { }
	}

	// - Entire folders can be marked as addressable
	// -- If you make a folder addressable and drag another object into that folder, that new object will also be addressable
	// --- To load them add a label to the folder and use the AssetLabelReference
	/*
	[SerializeField] private AssetLabelReference specialSpritesLabelReference;

	private void LoadAddressablesFolder() {
		Addressables.LoadAssetsAsync<Sprite>(specialSpritesLabelReference, (sprite) => {
			Debug.Log(sprite);
		});
	}
	*/
	#endregion

	#region Addressables Remote
	// - Setup addressables to load assets from a remote location (cloud, cdn, or your own server)
	// -- Deliver updated assets to players without needing to update the game

	// - To setup addressables remote:
	// -- Open the window -> Window -> Asset Management -> Addressables -> Settings
	// --- Under Content Update enable Build Remote Catalog and set 'Build & Load Paths' to Remote
	// ---- In the 'Addressables Groups' window, select the group and set the 'Build & Load Paths' to Remote
	// ----- In the 'Addressables Groups' window, build the asset bundles and upload them to the server
	// ------ To update the asset bundles, in the same window as building, click the 'Update a Previous Build' button, and upload them to the server

	// - To use the remote assets within the editor you can change the 'Play Mode Script' option in the Addressables Groups window
	#endregion

	#region Unity Cloud Content Delivery
	// - A service that delivers assets to players from a cloud server

	// - Can be easily integrated with addressables
	// -- This is a general cdn so you can use any assets with it, not just addressables
	#endregion

	#region Dynamic Resolution
	// - Lower the resolution when the game is running on a lower end device, or if the frame rate is too low
	// -- This can be done by changing the resolution scale

	// - Only has an effect on GPU

	// - There's 2 methods:

	// 1. Dynamic resolution - https://docs.unity3d.com/Manual/DynamicResolution.html
	// - Lower the resolution of certain cameras
	// - Has some limitations - Must be using Metal for IOS, Vulkan for Android, and DX12 for Windows
	// - To enable it, set 'allow dynamic resolution' to true in the camera settings

	// 2. Render scale on the render asset (URP or HDRP)

	public class TestDynamicResolution : MonoBehaviour {
		private void Update() {
			DynamicResolutionExample();

			RenderScaleExample();
		}

		private void DynamicResolutionExample() {
			// This requires the editor to be running in DX12
			if (Input.GetKeyDown(KeyCode.T)) {
				ScalableBufferManager.ResizeBuffers(0.1f, 0.1f);
			}

			if (Input.GetKeyDown(KeyCode.Y)) {
				ScalableBufferManager.ResizeBuffers(1f, 1f);
			}
		}

		private void RenderScaleExample() {
			// This requires the render asset to be URP or HDRP
			if (Input.GetKeyDown(KeyCode.U)) {
				((UniversalRenderPipelineAsset)GraphicsSettings.defaultRenderPipeline).renderScale = 0.1f;
			}

			if (Input.GetKeyDown(KeyCode.I)) {
				((UniversalRenderPipelineAsset)GraphicsSettings.defaultRenderPipeline).renderScale = 1f;
			}
		}
	}
	#endregion

	#region Build Size Report
	// - Check which assets are taking up the most space in the build

	// - The build size report is in the editor logs
	// -- To open the editor logs go to the top right corner of the console and click the three dots -> Open Editor Log

	// - Search for 'Build Report' in the editor log
	// -- This will show you the size of each asset in the build

	// - To reduce the size of the build:
	// - Compress textures, reduce their size, or use a texture atlas
	// - Compress meshes
	// - Compress animations
	// - Compress sounds
	// - Don't include assets, and download them from the cloud
	// -- Mobile games do this to keep the initial download size small
	#endregion
}
