using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamoMachineHandleAnimation : MonoBehaviour
{
	public AnimationCurve rotationFalloff;
	public Transform handle = null;
	public float rotationSpeed = 30.0f;

	private DynamoMachine cachedDynamoMachine = null;

	private void Awake()
	{
		cachedDynamoMachine = GetComponent<DynamoMachine>();
	}

	private void Update()
	{
		if (!handle || !cachedDynamoMachine)
			return;

		float velocity = Mathf.Clamp01(rotationFalloff.Evaluate(cachedDynamoMachine.Percentage)) * rotationSpeed;

		handle.rotation *= Quaternion.AngleAxis(velocity * Time.deltaTime, Vector3.back);
	}
}
