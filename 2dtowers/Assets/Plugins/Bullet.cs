using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public GameObject vTarget;
	public Turret vT;
	public float vSpeed;
	public int vDmg;
	
	public float vTriggerDist = 0.4F;
	public int vCounter = 1000;
	
	public GameObject vNearest;
	//distace to nearest
	public float vNearDist;
	void Awake () 
	{
	
	}
	//find nearest enemy
	void FindNearest()
	{
		foreach(GameObject a in vT.vEnemies)
		{
			if(vNearest == null)
			{
				vNearest = a;
				vNearDist = (a.transform.position - transform.position).sqrMagnitude;
			}
			else
			{
				if(vNearDist > (a.transform.position - transform.position).sqrMagnitude)
				{
					vNearest = a;
					vNearDist = (a.transform.position - transform.position).sqrMagnitude;
				}
			}
		}
		if(vNearDist >= 10)
		{
		vNearest = null;
		}
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
		FindNearest();
		if(vNearest != null)
		{
		vTarget = vNearest;
		}
		else{
			transform.Translate(Vector3.forward * Time.deltaTime * vSpeed);
			vCounter--;
			if(vCounter <= 0)
			{
				Destroy(gameObject);
			}
			
			}
		}
	}
	
	public void vShoot(GameObject t, float s, int d)
	{
		
		vSpeed = s;
		vDmg = d;
		vTarget = t;
	}
}
