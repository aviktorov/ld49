using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: jump? run?

public class PlayerFPSController : MonoBehaviour
{
	public float turnSpeed = 360.0f;
	public float walkSpeed = 5.0f;
	public float runSpeed = 10.0f;
	public float airSpeed = 5.0f;
	public float maxSpeed = 10.0f;
	public float airDrag = 5.0f;
	public float jumpSpeed = 100.0f;
	public float fovSmoothness = 5.0f;
	public float runFovMultiplier = 1.2f;
	public const float ANGLE_LIMIT = 89.0f;

	private bool onGround = false;

	private Camera cachedCamera;
	private Transform cachedCameraTransform;
	private Rigidbody cachedRigidbody;

	private Vector2 angles;
	private Quaternion initialOrientation;
	private float initialFov;

	private void Awake()
	{
		cachedCamera = GetComponentInChildren<Camera>();
		cachedCameraTransform = cachedCamera.transform;
		cachedRigidbody = GetComponent<Rigidbody>();

		initialOrientation = cachedCameraTransform.localRotation;
		initialFov = cachedCamera.fieldOfView;
		angles = Vector2.zero;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		bool isSprinting = Input.GetButton("Sprint");

		// aim
		Vector2 mouseDelta = new Vector2(
			Input.GetAxis("Mouse X"),
			Input.GetAxis("Mouse Y")
		);

		angles += mouseDelta * turnSpeed * Time.deltaTime;
		angles.y = Mathf.Clamp(angles.y, -ANGLE_LIMIT, ANGLE_LIMIT);

		cachedCameraTransform.localRotation = initialOrientation * Quaternion.Euler(-angles.y, angles.x, 0.0f);

		// movement
		Vector3 movement = new Vector3(
			Input.GetAxis("Horizontal"),
			Input.GetAxis("Vertical"),
			0.0f
		);

		movement = cachedCameraTransform.forward * movement.y + cachedCameraTransform.right * movement.x;

		if (movement.sqrMagnitude > 1)
			movement.Normalize();

		// jump
		float verticalVelocity = cachedRigidbody.velocity.y;

		if (onGround && Input.GetButtonDown("Jump"))
		{
			verticalVelocity = jumpSpeed;
			onGround = false;
		}

		// apply
		if (onGround)
			movement *= isSprinting ? runSpeed : walkSpeed;
		else
			movement = Vector3.Lerp(cachedRigidbody.velocity, movement * airSpeed, airDrag * Time.deltaTime);

		movement.y = 0.0f;

		float currentMagnitude = movement.magnitude;
		movement = movement.normalized * Mathf.Min(maxSpeed, currentMagnitude);

		cachedRigidbody.velocity = new Vector3(movement.x, verticalVelocity, movement.z);

		// camera fov
		float targetFov = initialFov;

		if (onGround && isSprinting)
			targetFov = initialFov * runFovMultiplier;

		cachedCamera.fieldOfView = Mathf.Lerp(cachedCamera.fieldOfView, targetFov, fovSmoothness * Time.deltaTime);
	}

	private void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			if (contact.normal.y > 0.8f)
			{
				onGround = true;
				return;
			}
		}
	}
}
