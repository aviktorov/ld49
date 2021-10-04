using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderStationButton : MonoBehaviour
{
	public int digit = -1;

	public float hitOffset = 0.005f;
	public float smoothness = 5.0f;

	private OrderStation cachedOrderStation;
	private Vector3 initialPosition;
	private Vector3 targetPosition;

	public void Press()
	{
		if (digit == -1)
			cachedOrderStation.Clear();
		else
			cachedOrderStation.PushDigit(digit);

		transform.position = initialPosition - transform.forward * hitOffset;
	}

	private void Awake()
	{
		cachedOrderStation = GetComponentInParent<OrderStation>();
		initialPosition = transform.position;
	}

	private void Update()
	{
		transform.position = Vector3.Lerp(transform.position, initialPosition, smoothness * Time.deltaTime);
	}
}
