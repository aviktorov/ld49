using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalDevice : MonoBehaviour
{
	public float minWattage = 10.0f;
	public float maxWattage = 20.0f;

	public BatterySocket batterySocket = null;

	private float currentWattage = 0.0f;
	private float extraWattage = 0.0f;

	public bool Powered => currentWattage > minWattage;
	public bool Overloaded => currentWattage > maxWattage;

	public float CurrentWattage => currentWattage;
	public float CurrentWattageNormalized => (currentWattage - minWattage) / (maxWattage - minWattage);

	public void Charge(InteractableItem item)
	{
		if (!item)
			return;

		extraWattage = item.Drain(maxWattage, Time.deltaTime);
	}

	private void Awake()
	{
		currentWattage = 0.0f;
	}

	private void Update()
	{
		float batteryWattage = (batterySocket) ? batterySocket.Drain(maxWattage, Time.deltaTime) : 0.0f;

		currentWattage = batteryWattage + extraWattage;
		extraWattage = 0.0f;
	}
}
