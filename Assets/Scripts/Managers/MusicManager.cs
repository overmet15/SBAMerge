using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public static MusicManager instance;

	[SerializeField]
	private List<MusicThingy> music;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		StartCoroutine(SelectMusic());
		StartCoroutine(SlowlyAddPitch());
	}

	private IEnumerator SelectMusic()
	{
		MusicThingy i = music[Random.Range(0, music.Count)];
		AudioSource a = GetComponent<AudioSource>();
		if (i.start != null)
		{
			a.clip = i.start;
			a.Play();
			yield return new WaitForSecondsRealtime(i.start.length);
			a.clip = i.loop;
			a.Play();
		}
		else
		{
			a.clip = i.loop;
			a.Play();
		}
	}

	public IEnumerator SlowlyAddPitch(float pitch = 1f)
	{
		AudioSource a = GetComponent<AudioSource>();
		float elapsedTime = a.pitch;
		while (elapsedTime < pitch)
		{
			elapsedTime = a.pitch = elapsedTime + 0.65f * Time.deltaTime;
			yield return null;
		}
		a.pitch = pitch;
	}

	public IEnumerator SlowlyDeAddPitch(float pitch = 0f)
	{
		AudioSource a = GetComponent<AudioSource>();
		float elapsedTime = a.pitch;
		while (elapsedTime > pitch)
		{
			elapsedTime = (a.pitch = elapsedTime - 0.65f * Time.deltaTime);
			yield return null;
		}
		a.pitch = pitch;
	}
}

[System.Serializable]
public class MusicThingy
{
	public AudioClip start;
	public AudioClip loop;
}
