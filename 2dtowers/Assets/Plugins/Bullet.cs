using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public GameObject vTarget;
	public float vSpeed;
	public int vDmg;
	
	public float vTriggerDist = 0.4F;
	
	
	void Awake () 
	{
	
	}
	
	void Update () 
	{
		if(vTarget != null)
		{
			transform.LookAt(vTarget.transform);
			  transform.Translate(Vector3.forward * Time.deltaTime * vSpeed);
			  if((vTarget.transform.position - transform.position).sqrMagnitude <= vTriggerDist)
			  {
				vTarget.SendMessage("DMG", vDmg);
				Destroy(gameObject);
			  }
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	public void vShoot(GameObject t, float s, int d)
	{
		
		vSpeed = s;
		vDmg = d;
		vTarget = t;
	}
}
