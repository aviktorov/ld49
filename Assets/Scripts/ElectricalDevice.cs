using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalDevice : MonoBehaviour
{
	public AnimationCurve wattageOutput;
	public float maxWattage = 200.0f;
	public float decaySpeed = 1.0f;
	public float wattageIncreasePerUse = 0.2f;

	private float currentOutput = 0.0f;
	private PlayerInput cachedPlayerInput;

	public float currentWattage => Mathf.Max(0.0f, wattageOutput.Evaluate(currentOutput) * maxWattage);

	private void Awake()
	{
		cachedPlayerInput = GetComponent<PlayerInput>();
	}

	private void Update()
	{
		if (cachedPlayerInput.usePulse)
			currentOutput += wattageIncreasePerUse;
		else
			currentOutput -= decaySpeed * Time.deltaTime;

		currentOutput = Mathf.Clamp01(currentOutput);
	}
}
