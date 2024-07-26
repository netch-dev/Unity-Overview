using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RenderPipelines : MonoBehaviour {
	// ---------------------------------
	// Render Pipelines
	// ---------------------------------

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
	// Render Pipeline Objects
	// ---------------------------------

	// - Select the render pipeline asset -> Add renderer feature -> Render Objects

	// - Lets you override how some objects are rendered
	// -- Objects on the selected layer mask will be rendered with the new settings

	// - For example:
	// 1. Add a building ghost feature to make certain buildings transparent
	// -- Add the objects to the ghost layer
	// --- Create a transparent material and override it in the render objects feature
	// --- Set the layer mask on the render objects feature to the ghost layer
	// --- Remove the ghost layer from the opaque layer mask

	// 2. Add the ability to see through walls
	// -- Put the character on a separate layer
	// --- Create a material and override it in the render objects feature
	// --- Set the layer mask to the character layer
	// --- Set the depth test to greater and disable write depth
	// --- Remove the character layer from the opaque layer mask
	// ---- Add another renderer feature to render the character without an override
	// ---- On the depth do the opposite of the first feature, so Less
	// ----- If you're running SSAO and the characters appear transparent, enable the 'After Opaque' option in the SSAO settings

	// - The project includes both of these examples

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
