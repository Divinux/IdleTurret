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
	public EnemyModels vEM;
	public float vRemapped;
	   public Color lerpedColor = Color.white;
	   public Color blue =  new Color(0.493F, 0F, 0F, 1F);
	   public Color red = new Color(0F, 0.154F, 0.336F, 1F);
	   
	   public GameObject vParticles;
	   public GameObject vDMGParticles;
	void Awake()
	{
	
		vCam = GameObject.FindWithTag("MainCamera");
		vS = vCam.GetComponent<Sound>();
		vEM = gameObject.GetComponent<EnemyModels>();
	}
	
	void FixedUpdate () 
	{
	if(vT != null)
	{
		transform.LookAt(vT.transform);
		transform.Translate(Vector3.forward * Time.deltaTime * vSpeed);
		
		if((vT.transform.position - transform.position).sqrMagnitude <= 0.2)
		{
			vT.DMG(vDmg);
			if(vT.vParticles == 1)
			{
			Debug.Log("yee");
			vDMGParticles.transform.parent = null;
			DieOnCall a1 = vDMGParticles.GetComponent<DieOnCall>();
			a1.Die();
				vDMGParticles.particleSystem.Play();
			}
			Die();
		}
		vRemapped = Mathfx.Remap((vT.transform.position - transform.position).sqrMagnitude, 1f,400f,1f,0f);
		lerpedColor = Color.Lerp(red, blue, vRemapped);
		vEM.vMeshes[vEM.vCurrent].renderer.material.color = lerpedColor;
		if(Input.GetKeyDown("z"))
		{
			Debug.Log(vEM.vMeshes[vEM.vCurrent].renderer.material.color);
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
			if(vT.vParticles == 1)
			{
			DieOnCall a2 = vDMGParticles.GetComponent<DieOnCall>();
			a2.Die();
			vParticles.transform.parent = null;
			Debug.Log("yee die fgt");
				vParticles.particleSystem.Play();
			}
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
