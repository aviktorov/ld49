using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOrderStation : MonoBehaviour
{
	private OrderStation cachedOrderStation;
	private ElectricalDevice cachedElectricalDevice;
	private TMPro.TMP_Text cachedText;

	private void Awake()
	{
		cachedText = GetComponent<TMPro.TMP_Text>();
		cachedOrderStation = GetComponentInParent<OrderStation>();
		cachedElectricalDevice = GetComponentInParent<ElectricalDevice>();
	}

	private void Update()
	{
		if (!cachedText || !cachedOrderStation)
			return;

		if (!cachedElectricalDevice.Powered)
			cachedText.text = "";
		else if (cachedOrderStation.OrderedTrinket)
			cachedText.text = string.Format("{0:000}%", cachedOrderStation.ProgressNormalized * 100.0f);
		else
			cachedText.text = string.Format("{0:0000}", cachedOrderStation.Code);
	}
}
