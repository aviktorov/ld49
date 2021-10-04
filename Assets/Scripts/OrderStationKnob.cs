using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderStationKnob : MonoBehaviour
{
	public float smoothness = 5.0f;
	public float degreeInterval = 1.0f;
	public float failAttemptDegrees = 15.0f;

	private OrderStation cachedOrderStation;
	private float currentAngle = 0.0f;
	private Quaternion initialRotation = Quaternion.identity;

	public void Turn()
	{
		if (cachedOrderStation.OrderedTrinket)
		{
			currentAngle += failAttemptDegrees * degreeInterval;
			return;
		}

		bool success = cachedOrderStation.Order();
		if (!success)
			currentAngle = failAttemptDegrees * degreeInterval;
	}

	private void Awake()
	{
		cachedOrderStation = GetComponentInParent<OrderStation>();
		initialRotation = transform.rotation;
	}

	private void Update()
	{
		if (!cachedOrderStation)
			return;

		float targetAngle = (cachedOrderStation.OrderedTrinket) ? cachedOrderStation.DeliveryTime * degreeInterval : 0.0f;

		currentAngle = Mathf.Lerp(currentAngle, targetAngle, smoothness * Time.deltaTime);
		transform.rotation = initialRotation * Quaternion.AngleAxis(currentAngle, Vector3.right);
	}
}
