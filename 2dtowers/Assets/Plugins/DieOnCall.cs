using UnityEngine;
using System.Collections;

public class DieOnCall : MonoBehaviour 
{

	void DieInvoked () 
	{
	Destroy(gameObject);
	}
	
	public void Die () 
	{
	Invoke("DieInvoked", 1f);
	}
}
