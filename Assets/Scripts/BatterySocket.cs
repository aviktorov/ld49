using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySocket : MonoBehaviour
{
	public Transform attachSocket = null;

	[HideInInspector]
	public Battery battery = null;

	public float Drain(float wattage, float deltaTime)
	{
		return (battery) ? battery.Drain(wattage, deltaTime) : 0.0f;
	}
}
