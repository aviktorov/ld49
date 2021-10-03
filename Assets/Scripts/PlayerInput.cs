using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	[HideInInspector]
	public Vector2 moveDirection = Vector2.zero;

	[HideInInspector]
	public Vector2 mouseDelta = Vector2.zero;

	[HideInInspector]
	public bool jumpPulse = false;

	[HideInInspector]
	public bool usePulse = false;

	[HideInInspector]
	public bool grabDropPulse = false;

	[HideInInspector]
	public bool throwPulse = false;

	[HideInInspector]
	public bool charging = false;

	[HideInInspector]
	public bool sprinting = false;

	// Update is called once per frame
	void Update()
	{
		mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		jumpPulse = Input.GetButtonDown("Jump");
		usePulse = Input.GetButtonDown("Fire1");
		grabDropPulse = Input.GetButtonDown("Fire2");
		throwPulse = Input.GetButtonDown("Fire3");
		charging = Input.GetButton("Charge");
		sprinting = Input.GetButton("Sprint");
	}
}
