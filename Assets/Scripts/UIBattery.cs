using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBattery : MonoBehaviour
{
	private Battery cachedBattery;
	private TMPro.TMP_Text cachedText;

	private void Awake()
	{
		cachedText = GetComponent<TMPro.TMP_Text>();
		cachedBattery = GetComponentInParent<Battery>();
	}

	private void Update()
	{
		if (!cachedText || !cachedBattery)
			return;

		cachedText.text = string.Format("{0:000.00} / 100%", cachedBattery.ChargePercentage);
	}
}
