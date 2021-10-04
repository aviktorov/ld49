using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCenter : MonoBehaviour
{
	public Transform spawnSocket = null;

	public GameObject SpawnTrinket(GameObject prefab)
	{
		if (!prefab)
			return null;

		if (!spawnSocket)
			return null;

		GameObject trinket = GameObject.Instantiate(prefab, spawnSocket.position, spawnSocket.rotation);
		return trinket;
	}
}
