using UnityEngine;
using System.Collections;

public class Waves : MonoBehaviour 
{
public Turret vT;

public int vF;
public bool vStarted = false;
public GameObject vParticles1;
	void Awake () 
	{
	
	//Spawn();
	}
	
	void Spawn () 
	{
	vT.Spawn();
	vF = vT.vWaveFreq;
	}
	
	public void Clicka(int c)
	{
		vF -= c;
		if(vT.vParticles == 1){
		vParticles1.particleSystem.Play();
		
	}
	}
	
	void FixedUpdate()
	{
	if(vStarted){
		vF--;
		if(vF <= 0)
		{
			Spawn();
			vF = vT.vWaveFreq;
		}
	}
	}
}
