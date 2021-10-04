using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketSuriken : MonoBehaviour
{
	public float reachDistance = 2.0f;
	public float backForce = 100.0f;
	public float tangentialForce = 1.0f;
	public float initialImpulse = 30.0f;
	public float initialTorque = 10.0f;

	private Rigidbody cachedRigidbody = null;
	private ElectricalDevice cachedElectricalDevice = null;

	private Vector3 lastKnownPosition;
	private Vector3 lastKnownTangentialDirection;

	public void Launch()
	{
		lastKnownPosition = transform.position;
		lastKnownTangentialDirection = transform.up;

		if (!cachedElectricalDevice)
			return;

		if (!cachedElectricalDevice.Powered)
			return;

		cachedRigidbody.AddForce(transform.right * initialImpulse, ForceMode.Impulse);
		cachedRigidbody.AddRelativeTorque(Vector3.forward * initialTorque, ForceMode.Impulse);
	}

	private void Awake()
	{
		cachedRigidbody = GetComponent<Rigidbody>();
		cachedElectricalDevice = GetComponent<ElectricalDevice>();

		lastKnownPosition = transform.position;
		lastKnownTangentialDirection = transform.up;
	}

	private void Update()
	{
		if (!cachedRigidbody)
			return;

		if (!cachedElectricalDevice)
			return;

		if (!cachedElectricalDevice.Powered)
			return;

		Vector3 delta = lastKnownPosition - transform.position;
		if (delta.sqrMagnitude > reachDistance * reachDistance)
		{
			cachedRigidbody.AddForce(delta * backForce);
			cachedRigidbody.AddForce(lastKnownTangentialDirection * tangentialForce);
		}
	}
}
