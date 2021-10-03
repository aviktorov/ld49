using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITemp : MonoBehaviour
{
	public DynamoMachine device;

	private TMPro.TMP_Text cachedText;

	private void Awake()
	{
		cachedText = GetComponent<TMPro.TMP_Text>();
	}

	private void Update()
	{
		if (!cachedText || !device)
			return;

		cachedText.text = string.Format("{0:000.00} / {1} W", device.Wattage, device.MaxWattage);
	}
}
