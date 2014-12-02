using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour 
{
	public AudioClip vClickY;
	public AudioClip vClickN;
	public AudioClip vCash;
	public AudioClip vNotif;
	
	public GameObject SoundSource;
	
	public void PlayCash() 
	{
		GameObject a = Instantiate(SoundSource, transform.position, transform.rotation) as GameObject;
		a.audio.clip = vCash;
		a.audio.Play();
	}
	public void PlayClickY() 
	{
		GameObject a = Instantiate(SoundSource, transform.position, transform.rotation) as GameObject;
		a.audio.clip = vClickY;
		a.audio.Play();
	}
	public void PlayClickN() 
	{
		GameObject a = Instantiate(SoundSource, transform.position, transform.rotation) as GameObject;
		a.audio.clip = vClickN;
		a.audio.Play();
	}
	public void PlayNotif() 
	{
		GameObject a = Instantiate(SoundSource, transform.position, transform.rotation) as GameObject;
		a.audio.clip = vNotif;
		a.audio.Play();
	}

}
