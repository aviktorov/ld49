using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
	public LayerMask interactionMask = ~0;
	public float interactionDistance = 1.0f;
	public Transform attachSocket = null;

	private InteractableItem currentItem = null;

	private PlayerInput cachedPlayerInput;
	private Camera cachedCamera;

	private void Awake()
	{
		cachedPlayerInput = GetComponent<PlayerInput>();
		cachedCamera = GetComponentInChildren<Camera>();
	}

	private void Update()
	{
		if (!cachedPlayerInput)
			return;

		RaycastHit hit;
		bool hasHit = Physics.Raycast(cachedCamera.transform.position, cachedCamera.transform.forward, out hit, interactionDistance, interactionMask);

		ElectricalDevice electricalDevice = null;
		InteractableItem interactableItem = null;
		Battery battery = null;
		BatterySocket batterySocket = null;
		
		if (hasHit)
		{
			electricalDevice = hit.collider.GetComponent<ElectricalDevice>();
			interactableItem = hit.collider.GetComponent<InteractableItem>();
			battery = hit.collider.GetComponent<Battery>();
			batterySocket = hit.collider.GetComponent<BatterySocket>();
		}

		if (currentItem)
		{
			if (cachedPlayerInput.usePulse)
				currentItem.Use();

			if (cachedPlayerInput.charging && electricalDevice)
				electricalDevice.Charge(currentItem);

			if (cachedPlayerInput.charging && battery)
				battery.Charge(currentItem);

			if (cachedPlayerInput.grabDropPulse)
			{
				// TODO: check better drop position if there's a hit nearby
				currentItem.Throw(cachedCamera.transform.forward);

				if (batterySocket)
					currentItem.Attach(batterySocket);

				currentItem = null;
			}
			else if (cachedPlayerInput.throwPulse)
			{
				currentItem.Throw(cachedCamera.transform.forward * 10.0f);
				currentItem = null;
			}
		}
		else if (interactableItem)
		{
			if (cachedPlayerInput.usePulse)
				interactableItem.Use();

			if (cachedPlayerInput.grabDropPulse)
			{
				interactableItem.AttachToPlayer(attachSocket);
				currentItem = interactableItem;
			}
		}
	}
}
