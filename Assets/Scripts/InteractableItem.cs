using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
	public int defaultLayer = 0;
	public int firstPersonLayer = 6;

	public void Use()
	{
		DynamoMachine dynamoMachine = GetComponent<DynamoMachine>();
		if (dynamoMachine)
			dynamoMachine.Use();
	}

	public void Attach(Transform parent)
	{
		DecorateGameObject(gameObject, true);

		gameObject.transform.position = parent.position;
		gameObject.transform.rotation = parent.rotation;
		gameObject.transform.parent = parent;
	}

	public void Throw(Vector3 impulse)
	{
		DecorateGameObject(gameObject, false);
		gameObject.transform.parent = null;

		Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
		if (rigidbody)
			rigidbody.AddForce(impulse, ForceMode.Impulse);
	}

	[ContextMenu("Switch to first person")]
	private void SwitchToFirstPerson()
	{
		DecorateGameObject(gameObject, true);
	}

	[ContextMenu("Switch to third person")]
	private void SwitchToThirdPerson()
	{
		DecorateGameObject(gameObject, false);
	}

	private void DecorateGameObject(GameObject gameObject, bool firstPerson)
	{
		FPSMaterial[] materials = gameObject.GetComponents<FPSMaterial>();
		Collider[] colliders = gameObject.GetComponents<Collider>();
		Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

		if (rigidbody)
			rigidbody.isKinematic = firstPerson;

		foreach (Collider collider in colliders)
			collider.enabled = !firstPerson;

		foreach (FPSMaterial material in materials)
			material.Decorate(firstPerson);

		gameObject.layer = firstPerson ? firstPersonLayer : defaultLayer;

		foreach (Transform child in gameObject.transform)
			DecorateGameObject(child.gameObject, firstPerson);
	}
}
