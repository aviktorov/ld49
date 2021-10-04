using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketQB : MonoBehaviour
{
	public float drag = 10.0f;

	private Rigidbody cachedRigidbody = null;
	private ElectricalDevice cachedElectricalDevice = null;

	private void Awake()
	{
		cachedRigidbody = GetComponent<Rigidbody>();
		cachedElectricalDevice = GetComponent<ElectricalDevice>();
	}

	private void Update()
	{
		if (!cachedRigidbody)
			return;

		if (!cachedElectricalDevice)
			return;

		cachedRigidbody.useGravity = !cachedElectricalDevice.Powered;
		cachedRigidbody.drag = cachedElectricalDevice.Powered ? drag : 0.0f;
	}
}
