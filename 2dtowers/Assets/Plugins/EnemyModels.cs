using UnityEngine;
using System.Collections;

public class EnemyModels : MonoBehaviour 
{
	public GameObject[] vMeshes;
	public Enemy vE;
	public int vCurrent; 
	
	void Awake () 
	{
		vE = gameObject.GetComponent<Enemy>();
	}
	void Start()
	{
		Choose();
	}
	void DisableAll()
	{
		foreach(GameObject a in vMeshes)
		{
			a.SetActive(false);
		}
	}
	void Enable(int i)
	{
		DisableAll();
		vMeshes[i].SetActive(true);
	}
	void Choose()
	{
		int t = vE.vDmg;
		if(t < 5)
		{
			Enable(0);
			vCurrent = 0;
		}
		else
		{
			if(t/5 < vMeshes.Length)
			{
				Enable(t/5);
				vCurrent = t/5;
			}
			else
			{
				Enable(vMeshes.Length-1);
				vCurrent = vMeshes.Length-1;
			}
		}
	}

}