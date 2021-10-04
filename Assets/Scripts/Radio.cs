using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
	public float minFrequency = 88.0f;
	public float maxFrequency = 108.0f;
	public float smoothness = 5.0f;
	public float frequencyIncreasePerTurn = 0.9f;

	private float currentFrequency = 89.1f;
	private float targetFrequency = 89.1f;

	public float Frequency => currentFrequency;
	public float FrequencyNormalized => (currentFrequency - minFrequency) / (maxFrequency - minFrequency);

	public bool TurnUp()
	{
		targetFrequency = Mathf.Min(targetFrequency + frequencyIncreasePerTurn, maxFrequency);
		return (targetFrequency < maxFrequency);
	}

	public bool TurnDown()
	{
		targetFrequency = Mathf.Max(targetFrequency - frequencyIncreasePerTurn, minFrequency);
		return (targetFrequency > minFrequency);
	}

	private void Update()
	{
		currentFrequency = Mathf.Lerp(currentFrequency, targetFrequency, smoothness * Time.deltaTime);
	}
}
