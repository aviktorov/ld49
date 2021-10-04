using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketStar : MonoBehaviour
{
	private Rigidbody cachedRigidbody = null;

	public void Detach()
	{
		cachedRigidbody.isKinematic = false;
	}

	public void Attach(Collision collision)
	{
		if (collision.contactCount == 0)
			return;

		ContactPoint contact = collision.GetContact(0);

		cachedRigidbody.isKinematic = true;

		transform.position = contact.point;
		transform.rotation = Quaternion.LookRotation(transform.forward, contact.normal);
	}

	private void Awake()
	{
		cachedRigidbody = GetComponent<Rigidbody>();
	}
}
