using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRadio : MonoBehaviour
{
	private Radio cachedRadio;
	private ElectricalDevice cachedElectricalDevice;
	private TMPro.TMP_Text cachedText;

	private void Awake()
	{
		cachedText = GetComponent<TMPro.TMP_Text>();
		cachedRadio = GetComponentInParent<Radio>();
		cachedElectricalDevice = GetComponentInParent<ElectricalDevice>();
	}

	private void Update()
	{
		if (!cachedText || !cachedRadio)
			return;

		if (!cachedElectricalDevice.Powered)
			cachedText.text = "";
		else
			cachedText.text = string.Format("{0:000.00} MHz", cachedRadio.Frequency);
	}
}
