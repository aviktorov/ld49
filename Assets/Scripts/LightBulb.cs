using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : MonoBehaviour
{
	public int materialIndex = 0;
	public AnimationCurve intensityFalloff = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);

	private float cachedLightIntensity = 0.0f;
	private float cachedEmissiveIntensity = 0.0f;
	private ElectricalDevice cachedElectricalDevice;
	private Light cachedLight;
	private Renderer cachedRenderer;
	private Color cachedEmissiveColorLDR;

	private void Awake()
	{
		cachedElectricalDevice = GetComponentInParent<ElectricalDevice>();
		cachedLight = GetComponentInParent<Light>();
		if (cachedLight)
			cachedLightIntensity = cachedLight.intensity;

		cachedRenderer = GetComponent<Renderer>();
		cachedEmissiveColorLDR = cachedRenderer.sharedMaterials[materialIndex].GetColor("_EmissiveColorLDR");
		cachedEmissiveIntensity = cachedRenderer.sharedMaterials[materialIndex].GetFloat("_EmissiveIntensity");
	}

	private void Update()
	{
		if (!cachedElectricalDevice)
			return;

		float wattageLerp = Mathf.Clamp01(cachedElectricalDevice.CurrentWattageNormalized);
		float intensityLerp = intensityFalloff.Evaluate(wattageLerp);

		if (cachedLight)
			cachedLight.intensity = cachedLightIntensity * intensityLerp;

		cachedRenderer.materials[materialIndex].SetColor("_EmissiveColor", cachedEmissiveColorLDR.linear * cachedEmissiveIntensity * intensityLerp);
	}
}
