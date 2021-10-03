using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMaterial : MonoBehaviour
{
	public Material defaultMaterial = null;
	public Material fpsMaterial = null;

	private MeshRenderer[] cachedMeshRenderers = null;

	public void Decorate(bool firstPerson)
	{
		if (cachedMeshRenderers == null)
			cachedMeshRenderers = GetComponents<MeshRenderer>();

		foreach (MeshRenderer renderer in cachedMeshRenderers)
			renderer.sharedMaterial = firstPerson ? fpsMaterial : defaultMaterial;
	}
}
