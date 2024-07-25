using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RenderPipelines : MonoBehaviour {
	// - 1. Built-in
	// -- Legacy rendering system

	// - 2. Universal Render Pipeline (URP)
	// -- Maximum compatibility
	// -- Runs on every platform that Unity supports

	// - 3. High Definition Render Pipeline (HDRP)
	// -- Maximum fidelity
	// -- Intended for console and high-end PCs


	// - If you import an old asset that is using the built-in pipeline, you can update it by going to Edit -> Render Pipeline -> Universal Render Pipeline -> Upgrade Project Materials to UniversalRP Materials
	// -- This only works on the default shaders so if you have any custom shaders you will have to update them manually


	// ---------------------------------
	// Post Processing
	// ---------------------------------

	// - To add post processing simply create a new gameobject and add the Volume component
	// -- Might need to install the Post Processing package from the package manager under Unity Registry

	// - If you don't see the effects in the game view, check the cameras rendering settings and enable post processing
	// -- Also might have to enable HDR on the render pipeline asset

	// - There are also effects directly on the pipeline render asset 
	// -- Under renderer features you can add effects

	private void PostProcessingTest() {
		Volume volume = gameObject.GetComponent<Volume>();
		if (volume.profile.TryGet<Bloom>(out Bloom bloom)) {
			bloom.intensity.value = 10f;
		}
	}

}
