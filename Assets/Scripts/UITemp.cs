using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITemp : MonoBehaviour
{
	private DynamoMachine cachedDynamoMachine;
	private TMPro.TMP_Text cachedText;

	private void Awake()
	{
		cachedText = GetComponent<TMPro.TMP_Text>();
		cachedDynamoMachine = GetComponentInParent<DynamoMachine>();
	}

	private void Update()
	{
		if (!cachedText || !cachedDynamoMachine)
			return;

		cachedText.text = string.Format("{0:000.00} / {1} W", cachedDynamoMachine.Wattage, cachedDynamoMachine.MaxWattage);
	}
}
