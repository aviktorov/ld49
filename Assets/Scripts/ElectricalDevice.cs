using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalDevice : MonoBehaviour
{
	public float minWattage = 10.0f;
	public float maxWattage = 20.0f;

	public BatterySocket batterySocket = null;

	private EmbeddedBattery cachedEmbeddedBattery = null;

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

		float wattageUsed = (maxWattage + minWattage) * 0.5f;
		extraWattage = item.Drain(wattageUsed, Time.deltaTime);

		if (cachedEmbeddedBattery)
		{
			extraWattage = Mathf.Max(0.0f, extraWattage - wattageUsed);
			cachedEmbeddedBattery.Charge(extraWattage);
		}
	}

	private void Awake()
	{
		cachedEmbeddedBattery = GetComponent<EmbeddedBattery>();
		currentWattage = 0.0f;
	}

	private void Update()
	{
		float wattageRequired = (maxWattage + minWattage) * 0.5f;
		currentWattage = 0.0f;

		// try to get wattage from dynamo machine
		currentWattage += extraWattage;
		extraWattage = 0.0f;

		if (currentWattage > wattageRequired)
			return;

		float deltaWattage = wattageRequired - currentWattage;

		// then try to get wattage from power socket connection
		float batteryWattage = (batterySocket) ? batterySocket.Drain(deltaWattage, Time.deltaTime) : 0.0f;
		float embeddedBatteryWattage = 0.0f;

		// else fallback to embedded power
		if (batteryWattage == 0.0f)
			embeddedBatteryWattage = (cachedEmbeddedBattery) ? cachedEmbeddedBattery.Drain(deltaWattage, Time.deltaTime) : 0.0f;

		currentWattage += batteryWattage + embeddedBatteryWattage;
	}
}
