using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioFrequencyPointer : MonoBehaviour
{
	public Transform startPoint = null;
	public Transform endPoint = null;

	public float smoothness = 5.0f;

	private Radio cachedRadio = null;

	private void Awake()
	{
		cachedRadio = GetComponentInParent<Radio>();
	}

	private void Update()
	{
		if (!cachedRadio)
			return;

		float targetLerp = Mathf.Clamp01(cachedRadio.FrequencyNormalized);
		Vector3 targetPosition = Vector3.Lerp(startPoint.position, endPoint.position, targetLerp);

		transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
	}
}
