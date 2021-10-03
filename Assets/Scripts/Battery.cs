using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
	public float maxCapacity = 100.0f * 3600.0f;
	public float maxChargeWattage = 100.0f;

	private Rigidbody cachedRigidbody = null;
	private BatterySocket cachedSocket = null;
	private float currentCapacity = 0.0f;

	public float ChargePercentage => Mathf.Clamp(currentCapacity / maxCapacity * 100.0f, 0.0f, 100.0f);

	public void Detach()
	{
		if (!cachedSocket)
			return;

		cachedSocket.battery = null;
		cachedSocket = null;
		cachedRigidbody.isKinematic = false;
	}

	public void Attach(BatterySocket socket)
	{
		if (!socket)
			return;

		cachedSocket = socket;
		cachedSocket.battery = this;
		cachedRigidbody.isKinematic = true;

		transform.position = cachedSocket.attachSocket.position;
		transform.rotation = cachedSocket.attachSocket.rotation;
	}

	public void Charge(InteractableItem item)
	{
		if (!item)
			return;

		float extraWattage = item.Drain(maxChargeWattage, Time.deltaTime);
		currentCapacity = Mathf.Min(maxCapacity, currentCapacity + extraWattage * Time.deltaTime);
	}

	public float Drain(float wattage, float deltaTime)
	{
		currentCapacity = Mathf.Max(0.0f, currentCapacity - wattage * deltaTime);
		return Mathf.Min(currentCapacity, wattage);
	}

	private void Awake()
	{
		cachedRigidbody = GetComponent<Rigidbody>();
	}
}
