using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSounds : MonoBehaviour
{
	public float playTime = 60.0f;
	public float maxVolume = 0.7f;

	public AudioClip[] commercials;
	public AudioClip[] tracks;

	public AudioSource trackSource = null;
	public AudioSource commericalSource = null;

	public float smoothness = 5.0f;

	private ElectricalDevice cachedElectricalDevice = null;

	private float currentPlayTime = 0.0f;
	private bool shouldPlayCommercial = false;

	private void PlayRandomMusic()
	{
		int index = Random.Range(0, tracks.Length);

		trackSource.clip = tracks[index];
		trackSource.loop = true;
		trackSource.Play();
	}

	private void PlayRandomCommercial()
	{
		int index = Random.Range(0, commercials.Length);

		commericalSource.clip = commercials[index];
		commericalSource.loop = false;
		commericalSource.Play();
	}

	private void Awake()
	{
		cachedElectricalDevice = GetComponent<ElectricalDevice>();

		trackSource.volume = 0.0f;
		trackSource.loop = true;

		commericalSource.volume = 0.0f;
		commericalSource.loop = false;

		PlayRandomMusic();
	}

	private void Update()
	{
		float targetTrackVolume = (shouldPlayCommercial) ? 0.0f : maxVolume;
		float targetCommercialVolume = (shouldPlayCommercial) ? maxVolume : 0.0f;

		if (!cachedElectricalDevice || !cachedElectricalDevice.Powered)
		{
			targetTrackVolume = 0.0f;
			targetCommercialVolume = 0.0f;
		}

		trackSource.volume = Mathf.Lerp(trackSource.volume, targetTrackVolume, smoothness * Time.deltaTime);
		commericalSource.volume = Mathf.Lerp(commericalSource.volume, targetCommercialVolume, smoothness * Time.deltaTime);

		if (shouldPlayCommercial)
		{
			if (!commericalSource.isPlaying)
			{
				shouldPlayCommercial = false;
				currentPlayTime = 0.0f;

				PlayRandomMusic();
			}
		}
		else
		{
			currentPlayTime += Time.deltaTime;

			if (currentPlayTime > playTime)
			{
				currentPlayTime = 0.0f;
				shouldPlayCommercial = true;

				PlayRandomCommercial();
			}
		}
	}
}
