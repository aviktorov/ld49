using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : MonoBehaviour
{
	public AnimationCurve intensityFalloff;

	private float cachedLightIntensity = 0.0f;
	private float cachedEmissiveIntensity = 0.0f;
	private ElectricalDevice cachedElectricalDevice;
	private Light cachedLight;
	private Renderer cachedRenderer;
	private Material cachedMaterial;

	private void Awake()
	{
		cachedElectricalDevice = GetComponent<ElectricalDevice>();
		cachedLight = GetComponent<Light>();
		cachedLightIntensity = cachedLight.intensity;

		cachedRenderer = GetComponent<Renderer>();
		cachedMaterial = cachedRenderer.material;
		cachedEmissiveIntensity = cachedMaterial.GetFloat("_EmissiveIntensity");
	}

	private void Update()
	{
		if (!cachedElectricalDevice)
			return;

		float wattageLerp = Mathf.Clamp01(cachedElectricalDevice.CurrentWattageNormalized);
		float intensityLerp = intensityFalloff.Evaluate(wattageLerp);

		cachedLight.intensity = cachedLightIntensity * intensityLerp;

		Color emissiveColorLDR = cachedMaterial.GetColor("_EmissiveColorLDR");
		cachedMaterial.SetColor("_EmissiveColor", emissiveColorLDR.linear * cachedEmissiveIntensity * intensityLerp);
	}
}
