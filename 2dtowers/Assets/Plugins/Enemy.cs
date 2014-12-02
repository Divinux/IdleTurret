using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public float vSpeed = 1;
	public int vDmg = 1;
	public float vHealth = 1;
	public int vMoney = 1;
	
	public Turret vT;
	
	
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
			vT.EARN(vDmg, vMoney);
			
			Die();
		}
	}
	
	void Die()
	{
	vT.vEnemies.Remove(gameObject);
	Destroy(gameObject);
	}
}
