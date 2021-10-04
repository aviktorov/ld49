using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerIndicator : MonoBehaviour
{
	public MeshRenderer[] renderers = null;

	private EmbeddedBattery cachedEmbeddedBattery = null;

	private void Awake()
	{
		cachedEmbeddedBattery = GetComponent<EmbeddedBattery>();
	}

	private void Update()
	{
		if (!cachedEmbeddedBattery)
			return;

		if (renderers == null || renderers.Length == 0)
			return;

		int numRenderers = renderers.Length;
		float batteryCharge = cachedEmbeddedBattery.ChargePercentage;
		float batteryChargeStep = 100.0f / numRenderers;

		for (int i = 0; i < numRenderers; ++i)
			renderers[i].enabled = (batteryCharge > batteryChargeStep * i);
	}
}
