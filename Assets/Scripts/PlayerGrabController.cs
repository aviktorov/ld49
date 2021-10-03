using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabController : MonoBehaviour
{
	public DynamoMachine device = null;

	private PlayerInput cachedPlayerInput;

	private void Awake()
	{
		cachedPlayerInput = GetComponent<PlayerInput>();
	}

	private void Update()
	{
		if (!device || !cachedPlayerInput)
			return;

		if (cachedPlayerInput.usePulse)
			device.Use();
	}
}
