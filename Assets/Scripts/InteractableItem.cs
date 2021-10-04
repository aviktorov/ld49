using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
	public bool movable = true;
	public int defaultLayer = 0;
	public int firstPersonLayer = 6;

	public float Drain(float wattage, float deltaTime)
	{
		DynamoMachine dynamoMachine = GetComponent<DynamoMachine>();
		if (dynamoMachine)
			return dynamoMachine.Wattage;

		return 0.0f;
	}

	public void Use()
	{
		DynamoMachine dynamoMachine = GetComponent<DynamoMachine>();
		if (dynamoMachine)
			dynamoMachine.Use();

		OrderStationButton button = GetComponent<OrderStationButton>();
		if (button)
			button.Press();

		OrderStationKnob knob = GetComponent<OrderStationKnob>();
		if (knob)
			knob.Turn();
	}

	public void AttachToPlayer(Transform socket)
	{
		if (!movable)
			return;

		Battery battery = GetComponent<Battery>();
		if (battery)
			battery.Detach();

		DecorateGameObject(gameObject, true);

		gameObject.transform.position = socket.position;
		gameObject.transform.rotation = socket.rotation;
		gameObject.transform.parent = socket;
	}

	public void Attach(BatterySocket socket)
	{
		if (!movable)
			return;

		if (!socket)
			return;

		Battery battery = GetComponent<Battery>();
		if (!battery)
			return;

		DecorateGameObject(gameObject, false);
		gameObject.transform.parent = null;
		
		battery.Attach(socket);
	}

	public void Throw(Vector3 impulse)
	{
		if (!movable)
			return;

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

	private void OnTriggerEnter(Collider collider)
	{
		if (!movable)
			return;

		BatterySocket socket = collider.GetComponent<BatterySocket>();
		Attach(socket);
	}
}
