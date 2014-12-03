using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public float vSpeed = 1;
	public int vDmg = 1;
	public float vHealth = 1;
	public int vMoney = 1;
	public int vExp = 1;
	
	public Turret vT;
	public GameObject vCam;
	public Sound vS;
	
	void Awake()
	{
		vCam = GameObject.FindWithTag("MainCamera");
		vS = vCam.GetComponent<Sound>();
	}
	void FixedUpdate () 
	{
	if(vT != null)
	{
		transform.LookAt(vT.transform);
		transform.Translate(Vector3.forward * Time.deltaTime * vSpeed);
		
		if((vT.transform.position - transform.position).sqrMagnitude <= 0.4)
		{
			vT.DMG(vDmg);
			Die();
		}
		
	}
	}
	
	public void DMG(int d)
	{
		vHealth -= d;
		if(vHealth <= 0)
		{
			//die
			vT.EARN(vExp, vMoney);
			
			Die();
		}
	}
	
	void Die()
	{
	vS.PlayPop();
	vT.vEnemies.Remove(gameObject);
	Destroy(gameObject);
	}
}
