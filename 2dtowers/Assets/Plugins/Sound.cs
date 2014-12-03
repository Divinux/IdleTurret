using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour 
{
	public AudioClip vClickY;
	public AudioClip vClickN;
	public AudioClip vCash;
	public AudioClip vNotif;
	public AudioClip vMove;
	public AudioClip vShoot;
	public AudioClip vPop;
	
	public GameObject SoundSource;
	
	
	
	public float vMuted = 1.0f;
	
	void Update()
	{
		if(Input.GetKeyDown("m"))
		{
			
			if(vMuted == 1.0f)
			{
			vMuted = 0f;
			}
			else
			{
			vMuted = 1.0f;
			}
		}
	}
	
	
	public void PlayCash() 
	{
		GameObject a = Instantiate(SoundSource, transform.position, transform.rotation) as GameObject;
		a.audio.clip = vCash;
		a.audio.volume = 1.0f * 0.4f* vMuted;
		a.audio.Play();
	}
	public void PlayPop() 
	{
		GameObject a = Instantiate(SoundSource, transform.position, transform.rotation) as GameObject;
		a.audio.volume = 1.0f * vMuted;
		a.audio.clip = vPop;
		a.audio.Play();
	}
	public void PlayClickY() 
	{
		GameObject a = Instantiate(SoundSource, transform.position, transform.rotation) as GameObject;
		a.audio.clip = vClickY;
		a.audio.volume = 1.0f * 0.3f * vMuted;
		a.audio.Play();
	}
	public void PlayClickN() 
	{
		GameObject a = Instantiate(SoundSource, transform.position, transform.rotation) as GameObject;
		a.audio.clip = vClickN;
		a.audio.volume = 1.0f * 0.3f * vMuted;
		a.audio.Play();
	}
	public void PlayNotif() 
	{
		GameObject a = Instantiate(SoundSource, transform.position, transform.rotation) as GameObject;
		a.audio.clip = vNotif;
		a.audio.volume = 1.0f * 0.5f*vMuted;
		a.audio.Play();
	}
	public void PlayMove() 
	{
		GameObject a = Instantiate(SoundSource, transform.position, transform.rotation) as GameObject;
		a.audio.clip = vMove;
		a.audio.volume = 1.0f * vMuted;
		a.audio.Play();
	}
	public void PlayShoot() 
	{
		GameObject a = Instantiate(SoundSource, transform.position, transform.rotation) as GameObject;
		a.audio.clip = vShoot;
		a.audio.volume = 1.0f *0.5f* vMuted;
		a.audio.Play();
	}

}
