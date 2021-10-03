using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamoMachine : MonoBehaviour
{
	public AnimationCurve wattageOutput;
	public float maxWattage = 200.0f;
	public float decaySpeed = 1.0f;
	public float wattagePercentIncreasePerUse = 0.2f;

	private float currentOutput = 0.0f;

	public float Percentage => currentOutput;
	public float Wattage => Mathf.Max(0.0f, wattageOutput.Evaluate(currentOutput) * maxWattage);
	public float MaxWattage => maxWattage;

	public void Use()
	{
		currentOutput += wattagePercentIncreasePerUse;
	}

	private void Update()
	{
		currentOutput -= decaySpeed * Time.deltaTime;
		currentOutput = Mathf.Clamp01(currentOutput);
	}
}
