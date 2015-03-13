using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Turret : MonoBehaviour 
{
	//rotating gun object
	public GameObject vGun;
	//rotating gun object
	public GameObject vGunC;
	//bullet prefab
	public GameObject vBullet;
	//enemy prefab
	public GameObject vEnemy;
	//bullet spawn spot
	public GameObject vBulletSpawn;
	//shooting particles
	public GameObject vParticles1;
	public GameObject vParticles2;
	//lvl up particles
	public GameObject vParticles3;
	//
	public int vParticles = 1;
	
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
	public float vExpBase = 100;
	public float vExpLeft;
	public float vMod = 1.25f;
	//////////////
	//bullet speed
	public float vBulSpeed = 1f;
	//bullet dgm
	public float vBulDmg = 10f;
	//////////////
	//wave size
	public int vWaveSize = 1;
	public int vWaveFreq = 2000;
	public int eLvl = 1;
	public float eSpeed = 1f;
	public int eDmg = 1;
	public float eHealth = 1f;
	public int eMoney = 1;
	public int eExp = 2;
	
	
	
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
	public Meshes vM;
	
	public GameObject vScPos1;
	public GameObject vScPos2;
	
	
	//recoid vel
	private Vector3 velocityRec = Vector3.zero;
	private Vector3 targetPosition;
	//fwd rot
	public Quaternion fwd;
	//bwd rot 
	public Quaternion lft;
	//bwd rot 
	public Quaternion rght;
	//scan bool
	public bool vScanBool = true;
	//ray cd
	public int cd = 0;
	//direction change
	public int vLastDir = -1;
	public int vCurrDir = 0;
	void Awake () 
	{
		fwd = vGun.transform.rotation;
		lft = vScPos1.transform.rotation;
		rght = vScPos2.transform.rotation;
		targetPosition = vGunC.transform.localPosition;
		FindNearest();
		vExpLeft = vExpBase;
		
		vCam =  GameObject.FindWithTag("MainCamera");
		vN = vCam.GetComponent<Notif>();
		vS = vCam.GetComponent<Sound>();
		vM = gameObject.GetComponent<Meshes>();
	}
	
	void FixedUpdate () 
	{
		if(vNearest != null)
		{
			if((vNearest.transform.position - transform.position).sqrMagnitude <= vRange)
			{
				SmoothLook();
				vCurrDir = 0;
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
			else
			{
				
				SmoothScan();
				vCurrDir = 1;
			}
		}
		else
		{
			FindNearest();
		}
		
		if(vNearest == null)
		{
			
			SmoothScan();
			vCurrDir = 2;
		}
		
		if(vCool > 0)
		{
			vCool--;
			if(vCool < 0)
			{
				vCool = 0;
			}
		}
		if(vLastDir != vCurrDir)
		{
			vS.PlayMove();
			vLastDir = vCurrDir;
		}
	}
	//shoot
	void Shoot()
	{
		vS.PlayShoot();
		//shoot
		GameObject b = Instantiate(vBullet, vBulletSpawn.transform.position, vBulletSpawn.transform.rotation) as GameObject;
		Bullet bs = b.GetComponent<Bullet>();
		bs.vT = this;
		bs.vSpeed = vBulSpeed;
		bs.vDmg = (int)vBulDmg;
		bs.vTarget = vNearest;
		
		vCool = vMaxCool;
		Recoil();
		if(vParticles==1){
		 Particles();}
	}
	void Particles()
	{
	vParticles1.particleSystem.Play();
	vParticles2.particleSystem.Play();
	}
	void Recoil()
	{
		vGunC.transform.Translate(-Vector3.right * 0.5f);
		StartCoroutine("RecoilCo");
	}
	IEnumerator RecoilCo()
	{
		while(vGunC.transform.localPosition != targetPosition){
			vGunC.transform.localPosition = Vector3.SmoothDamp(vGunC.transform.localPosition, targetPosition, ref velocityRec, 0.2f);
			yield return new WaitForSeconds(0f);
		}
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
		vGun.transform.rotation = Quaternion.Slerp(vGun.transform.rotation, fwd, Time.deltaTime * 0.3f);
		//Debug.Log(Quaternion.Dot(fwd, vGun.transform.rotation));
		
	}
	void SmoothScan()
	{
		if(vScanBool)
		{
			vGun.transform.rotation = Quaternion.Slerp(vGun.transform.rotation, rght, Time.deltaTime * 0.2f);
			//Debug.Log(Quaternion.Angle(rght, vGun.transform.rotation));
			if(Quaternion.Angle(rght, vGun.transform.rotation) <= 20f)
			{
				
				vS.PlayMove();
				vScanBool = false;
			}
		}
		else
		{
			vGun.transform.rotation = Quaternion.Slerp(vGun.transform.rotation, lft, Time.deltaTime * 0.2f);
			//Debug.Log(Quaternion.Dot(lft, vGun.transform.rotation));
			if(Quaternion.Angle(lft, vGun.transform.rotation) <= 20f)
			{
				
				vS.PlayMove();
				vScanBool = true;
			}
		}
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
			e.vExp = eExp;
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
	if(vParticles==1){vParticles3.particleSystem.Play();}
		vCurrExp -= vExpLeft;
		//lvlup
		vLvl++;
		vMoney += vLvl*50;
		LvlStats();
		vN.AddNotif("Level Up!\nLevel: " + vLvl +"\n+" +vLvl*50 + "u\nGun Cooldown: " +vMaxCool.ToString("F0")+ "\nDamage: " + vBulDmg.ToString("F0") + "\nRotationspeed: " + damping.ToString("F0"));
		//calculate next level
		float t = Mathf.Pow(vMod, vLvl);
		vExpLeft = vExpBase * t;
	}
	//raise stats onlvlup
	void LvlStats()
	{
		float mis = vMaxHealth-vHealth;
		vMaxHealth *= 1.1f;
		vHealth = vMaxHealth;
		vHealth -= mis;
		
		vMaxCool *= 0.98f;
		if(vMaxCool < 1)
		{
			vMaxCool = 1;
		}
		vBulDmg *= 1.03f;
		vBulDmg++;
		vBulSpeed *= 1.03f;
		damping *= 1.1f;
	}
	//take damage
	public void DMG(int dm)
	{
		vHealth -= dm;
		if(vHealth <= 0)
		{
			Application.LoadLevel(0);
		}
	}
	//earn money and exp
	public void EARN(int exp, int mon)
	{
		vMoney += mon;
		GainExp(exp);
		
	}
}
