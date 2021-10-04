using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMultiMaterial : MonoBehaviour
{
	public Material[] defaultMaterials = null;
	public Material[] fpsMaterials = null;

	private MeshRenderer[] cachedMeshRenderers = null;

	public void Decorate(bool firstPerson)
	{
		if (cachedMeshRenderers == null)
			cachedMeshRenderers = GetComponents<MeshRenderer>();

		foreach (MeshRenderer renderer in cachedMeshRenderers)
			renderer.sharedMaterials = firstPerson ? fpsMaterials : defaultMaterials;
	}
}
