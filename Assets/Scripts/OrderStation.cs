using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct OrderDescription
{
	public int code;
	public GameObject trinket;
	public float deliveryTime;
}

public class OrderStation : MonoBehaviour
{
	public List<OrderDescription> availableOrders = new List<OrderDescription>();

	private ElectricalDevice cachedElectricalDevice;

	private int currentCode = 0;
	private GameObject orderedTrinket = null;
	private float deliveryTime = 0.0f;
	private float currentDeliveryTime = 0.0f;

	public GameObject OrderedTrinket => orderedTrinket;
	public float DeliveryTime => deliveryTime;
	public float CurrentDeliveryTime => currentDeliveryTime;
	public float ProgressNormalized => Mathf.Clamp01(1.0f - currentDeliveryTime / deliveryTime);
	public int Code => currentCode;

	public void PushDigit(int digit)
	{
		if (!cachedElectricalDevice || !cachedElectricalDevice.Powered)
			return;

		currentCode *= 10;
		currentCode += digit;
		currentCode %= 10000;
	}

	public void Clear()
	{
		currentCode = 0;
	}

	public bool Order()
	{
		if (!cachedElectricalDevice || !cachedElectricalDevice.Powered)
			return false;

		foreach (OrderDescription description in availableOrders)
		{
			if (description.code == currentCode)
			{
				orderedTrinket = description.trinket;
				deliveryTime = description.deliveryTime;
				currentDeliveryTime = description.deliveryTime;
				break;
			}
		}

		currentCode = 0;
		return orderedTrinket != null;
	}

	private void Awake()
	{
		cachedElectricalDevice = GetComponent<ElectricalDevice>();
	}
	
	private void Update()
	{
		if (!cachedElectricalDevice)
			return;

		if (!cachedElectricalDevice.Powered)
			return;

		currentDeliveryTime -= Time.deltaTime;
		if (currentDeliveryTime > 0.0f)
			return;

		if (!orderedTrinket)
			return;

		// TODO: notify delivery center and spawn trinket
		orderedTrinket = null;
	}
}
