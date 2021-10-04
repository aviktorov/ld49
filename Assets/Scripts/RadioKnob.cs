using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioKnob : MonoBehaviour
{
	public float smoothness = 5.0f;
	public float degreeInterval = 1.0f;
	public float failAttemptDegrees = 15.0f;

	private Radio cachedRadio;
	private float currentAngle = 0.0f;
	private Quaternion initialRotation = Quaternion.identity;

	public void TurnUp()
	{
		bool success = cachedRadio.TurnUp();
		if (!success)
			currentAngle += failAttemptDegrees;
	}

	public void TurnDown()
	{
		bool success = cachedRadio.TurnDown();
		if (!success)
			currentAngle -= failAttemptDegrees;
	}

	private void Awake()
	{
		cachedRadio = GetComponentInParent<Radio>();
		initialRotation = transform.rotation;

		currentAngle = (cachedRadio) ? cachedRadio.Frequency * degreeInterval : 0.0f;
	}

	private void Update()
	{
		float targetAngle = (cachedRadio) ? cachedRadio.Frequency * degreeInterval : 0.0f;

		currentAngle = Mathf.Lerp(currentAngle, targetAngle, smoothness * Time.deltaTime);
		transform.rotation = initialRotation * Quaternion.AngleAxis(currentAngle, Vector3.left);
	}
}
