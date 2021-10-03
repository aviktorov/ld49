using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabController : MonoBehaviour
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

		if (currentItem)
		{
			if (cachedPlayerInput.usePulse)
			{
				currentItem.Use();
			}
			else if (cachedPlayerInput.grabDropPulse)
			{
				currentItem.Throw(cachedCamera.transform.forward);
				currentItem = null;
			}
			else if (cachedPlayerInput.throwPulse)
			{
				currentItem.Throw(cachedCamera.transform.forward * 10.0f);
				currentItem = null;
			}

			return;
		}

		RaycastHit hit;
		if (!Physics.Raycast(cachedCamera.transform.position, cachedCamera.transform.forward, out hit, interactionDistance, interactionMask))
			return;

		InteractableItem item = hit.collider.gameObject.GetComponent<InteractableItem>();

		if (!item)
			return;

		if (cachedPlayerInput.usePulse)
			item.Use();

		if (cachedPlayerInput.grabDropPulse)
		{
			item.Attach(attachSocket);
			currentItem = item;
		}
	}
}
