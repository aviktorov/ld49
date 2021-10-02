using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : MonoBehaviour
{
	public DynamoMachine device;

	public AnimationCurve intensityFalloff;
	public float minWattage = 5.0f;
	public float maxWattage = 10.0f;

	private float cachedLightIntensity = 0.0f;
	private float cachedEmissiveIntensity = 0.0f;
	private Light cachedLight;
	private Renderer cachedRenderer;
	private Material cachedMaterial;

	private void Awake()
	{
		cachedLight = GetComponent<Light>();
		cachedLightIntensity = cachedLight.intensity;

		cachedRenderer = GetComponent<Renderer>();
		cachedMaterial = cachedRenderer.material;
		cachedEmissiveIntensity = cachedMaterial.GetFloat("_EmissiveIntensity");
	}

	private void Update()
	{
		if (!device)
			return;

		float wattageLerp = Mathf.Clamp01((device.currentWattage - minWattage) / (maxWattage - minWattage));
		float intensityLerp = intensityFalloff.Evaluate(wattageLerp);

		cachedLight.intensity = cachedLightIntensity * intensityLerp;

		Color emissiveColorLDR = cachedMaterial.GetColor("_EmissiveColorLDR");
		cachedMaterial.SetColor("_EmissiveColor", emissiveColorLDR.linear * cachedEmissiveIntensity * intensityLerp);
	}
}
