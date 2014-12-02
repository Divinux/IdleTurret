using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Turret : MonoBehaviour 
{
	//rotating gun object
	public GameObject vGun;
	//bullet prefab
	public GameObject vBullet;
	//enemy prefab
	public GameObject vEnemy;
	
	//////////////
	//to control the rotation 
	public float damping = 1.0f;	
	//range
	public float vRange = 10f;
	//cooldown
	public float vCool = 200f;
	public float vMaxCool = 200f;
	//hp
	public float vHealth = 100f;
	public float vMaxHealth = 100f;
	//money
	public int vMoney = 1;
	//level
	public int vLvl = 1;
	public float vCurrExp = 0;
	public float vExpBase = 10;
	public float vExpLeft;
	public float vMod = 1.15f;
	//////////////
	//bullet speed
	public float vBulSpeed = 1f;
	//bullet dgm
	public float vBulDmg = 10f;
	//////////////
	//wave size
	public int vWaveSize = 1;
	public int vWaveFreq = 2000;
	public float eSpeed = 1f;
	public int eDmg = 1;
	public float eHealth = 1f;
	public int eMoney = 1;
	
	
	
	//list of enemies
	public List<GameObject> vEnemies = new List<GameObject>();
	//nearest enemy
	public GameObject vNearest;
	//distace to nearest
	public float vNearDist;
	
	//list of spawners
	public List<GameObject> vSpawners = new List<GameObject>();
	
	//script objects
	public GameObject vCam;
	public Notif vN;
	public Sound vS;
	
	
	
	
	
	//fwd rot
	public Quaternion fwd;
	//ray cd
	public int cd = 0;
	
	void Awake () 
	{
		fwd = vGun.transform.rotation;
		FindNearest();
		vExpLeft = vExpBase;
		
		vCam =  GameObject.FindWithTag("MainCamera");
		vN = vCam.GetComponent<Notif>();
		vS = vCam.GetComponent<Sound>();
	}
	
	void FixedUpdate () 
	{
		if(vNearest != null)
		{
			if((vNearest.transform.position - transform.position).sqrMagnitude <= vRange)
			{
				SmoothLook();
				if(vCool <= 1 && cd <= 0)
				{
					RaycastHit hit;
					if (Physics.Raycast (vGun.transform.position, vGun.transform.TransformDirection(Vector3.forward),out hit, vRange * 1F)) 
					{
						//Debug.Log(hit.transform.gameObject);
						//Debug.DrawLine(hit.point, vGun.transform.position, Color.green, 1, false);
						//Debug.Log(vNearest);
						if(hit.transform.gameObject == vNearest)
						{
							Shoot();
						}
						cd = 5;
					}
				}
				cd--;
			}
		}
		else
		{
			FindNearest();
		}
		
		if(vNearest == null)
		{
			SmoothFwd();
		}
		
		if(vCool > 0)
		{
			vCool--;
		}
	}
	//shoot
	void Shoot()
	{
		
		//shoot
		GameObject b = Instantiate(vBullet, vGun.transform.position, vGun.transform.rotation) as GameObject;
		Bullet bs = b.GetComponent<Bullet>();
		bs.vSpeed = vBulSpeed;
		bs.vDmg = (int)vBulDmg;
		bs.vTarget = vNearest;
		
		vCool = vMaxCool;
		
	}
	//find nearest enemy
	void FindNearest()
	{
		foreach(GameObject a in vEnemies)
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
	}
	//rotation functions
	void SmoothLook()
	{
		Quaternion vrotation = Quaternion.LookRotation(vNearest.transform.position - vGun.transform.position);
		vGun.transform.rotation = Quaternion.Slerp(vGun.transform.rotation, vrotation, Time.deltaTime * damping);
	}
	void SmoothFwd()
	{
		vGun.transform.rotation = Quaternion.Slerp(vGun.transform.rotation, fwd, Time.deltaTime * damping);
	}
	//spawn next wave
	public void Spawn()
	{
		StartCoroutine("SpawnWave");
	}
	IEnumerator SpawnWave()
	{
		for(int i = 0; i< vWaveSize; i++)
		{
			int r = Random.Range(0,vSpawners.Count);
			GameObject ins = Instantiate(vEnemy, vSpawners[r].transform.position, vSpawners[r].transform.rotation) as GameObject;
			Enemy e = ins.GetComponent<Enemy>();
			e.vSpeed = eSpeed;
			e.vDmg = eDmg;
			e.vHealth = eHealth;
			e.vMoney = eMoney;
			vEnemies.Add(ins);
			e.vT = this;
			yield return new WaitForSeconds(1);
			
		}
	}
	//gain exp called internally
	void GainExp(int ex)
	{
		vCurrExp += ex;
		if(vCurrExp >= vExpLeft)
		{
			LvlUp();
			
		}
	}
	//lvl up
	public void LvlUp()
	{
		vCurrExp -= vExpLeft;
			//lvlup
			vLvl++;
			LvlStats();
			vN.AddNotif("Level Up!\nLevel: " + vLvl);
			//calculate next level
			float t = Mathf.Pow(vMod, vLvl);
			vExpLeft = vExpBase * t;
	}
	//raise stats onlvlup
	void LvlStats()
	{
		vMaxHealth *= 1.1f;
		//vHealth = vMaxHealth;
		
		vMaxCool *= 0.9f;
		if(vMaxCool < 1)
		{
			vMaxCool = 1;
		}
	}
	//take damage
	public void DMG(int dm)
	{
		vHealth -= dm;
		if(vHealth <= 0)
		{
			Debug.Log("Game Lost");
		}
	}
	//earn money and exp
	public void EARN(int exp, int mon)
	{
	vMoney += mon;
	GainExp(exp);
	
	}
}
