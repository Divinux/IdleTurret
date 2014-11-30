using UnityEngine;
using System.Collections;

public class Waves : MonoBehaviour 
{
public Turret vT;
public int vF;

	void Awake () 
	{
	Spawn();
	}
	
	void Spawn () 
	{
	vT.Spawn();
	vF = vT.vWaveFreq;
	}
	
	void FixedUpdate()
	{
		vF--;
		if(vF <= 0)
		{
			Spawn();
			vF = vT.vWaveFreq;
		}
	}
}
