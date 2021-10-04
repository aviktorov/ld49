using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbeddedBattery : MonoBehaviour
{
	public float maxCapacity = 1.0f * 3600.0f;
	public float maxChargeWattage = 10.0f;

	private float currentCapacity = 0.0f;

	public float ChargePercentage => Mathf.Clamp(currentCapacity / maxCapacity * 100.0f, 0.0f, 100.0f);

	public void Charge(float wattage)
	{
		float extraWattage = Mathf.Min(maxChargeWattage, wattage);
		currentCapacity = Mathf.Min(maxCapacity, currentCapacity + extraWattage * Time.deltaTime);
	}

	public float Drain(float wattage, float deltaTime)
	{
		currentCapacity = Mathf.Max(0.0f, currentCapacity - wattage * deltaTime);
		return Mathf.Min(currentCapacity, wattage);
	}
}
