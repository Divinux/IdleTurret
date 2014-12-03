using UnityEngine;
using System.Collections;

public class Meshes : MonoBehaviour 
{
	public GameObject[] vBases;
	public GameObject[] vGuns;
	
	public Texture2D[] vTextures;
	
	public Material vMaterial;
	
	public int vCurrBase = 0;
	public int vCurrGun = 0;
	public int vTexture = 0;
	
	
	void DisableBases () 
	{
		foreach (GameObject b in vBases)
		{
			b.SetActive(false);
		}
	}
	
	void DisableGuns () 
	{
		foreach (GameObject g in vGuns)
		{
			g.SetActive(false);
		}
	}
	[ContextMenu ("Do Something")]
	public void asd()
	{
	EnableTex(1);
	}
	public void EnableBase(int i)
	{
		DisableBases();
		vBases[i].SetActive(true);
		vCurrBase = i;
	}
	public void EnableGun(int i2)
	{
		DisableGuns();
		vGuns[i2].SetActive(true);
		vCurrGun = i2;
	}
	public void EnableTex(int i3)
	{
		vMaterial.mainTexture = vTextures[i3];
		vTexture = i3;
	}
}
