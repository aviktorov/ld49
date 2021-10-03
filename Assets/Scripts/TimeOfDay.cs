using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TimeOfDay : MonoBehaviour
{
	[Range(0, 24)]
	public float timeOfDay = 0.0f;
	public Light sun = null;
	public Light moon = null;

	public float realSecondsInGameHour = 1.0f;

	private void Update()
	{
		timeOfDay +=  Time.deltaTime / Mathf.Max(Mathf.Epsilon, realSecondsInGameHour);
		timeOfDay %= 24.0f;

		float k = timeOfDay / 24.0f;
		float sunRotation = Mathf.Lerp(-90.0f, 270.0f, k);
		float moonRotation = sunRotation + 180.0f;

		sun.transform.rotation = Quaternion.Euler(sunRotation, 150.0f, 0.0f);
		moon.transform.rotation = Quaternion.Euler(moonRotation, 150.0f, 0.0f);

		if (k > 0.25f && k < 0.75f)
		{
			sun.shadows = LightShadows.Soft;
			moon.shadows = LightShadows.None;
		}
		else
		{
			sun.shadows = LightShadows.None;
			moon.shadows = LightShadows.Soft;
		}
	}
}
