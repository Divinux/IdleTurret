using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Upgrades : MonoBehaviour 
{

	public List<Upgrade> lUpgrades = new List<Upgrade>();
	public List<Tech> lTech = new List<Tech>();
	
	public GameObject vCam;
	
	public Turret vT;
	public Ui vU;
	public Notif vN;
	//finished project to be unlocked by button
	public int vToUnlock = -1;

	[System.Serializable]
	public class Upgrade
	{
		//name of the upgrade, page its on and price
		public string vName;
		public int vPage;
		public int vPrice;
		//whether its reusable, and whethe rit has 
		//a special functionality other than unlocking further stuff
		public bool vReusable = false;
		public string vSpecial = "";
		//unlocked and bought status
		public bool vUnlocked;
		public bool vBought;
		//what if anything it unlocks
		public int vUpgrUnlock;
		public int vTechUnlock;
	}
	[System.Serializable]
	public class Tech
	{
		public string vName;
		public int vPrice;
		
		public bool vUnlocked;
		public bool vBought;
		
		public int vUpgrUnlock;
		public int vTechUnlock;
		
		public int vCool;
		public int vMaxCool;
	}
	void Awake () 
	{
		vCam = GameObject.FindWithTag("MainCamera");
		vT = gameObject.GetComponent<Turret>();
		vU = vCam.GetComponent<Ui>();
		vN = vCam.GetComponent<Notif>();
		PopulateUpgrades();
		PopulateTech();
	}
	
	//returns a list of all available upgrades
	public List<int> GiveUpgrades(int ff)
	{
		List<int> la = new List<int>();
		
		foreach(Upgrade u in lUpgrades)
		{
			if(u.vUnlocked && u.vPage == ff && !u.vBought)
			{
				la.Add(lUpgrades.IndexOf(u));
			}
		}
		
		return la;
	}
	//returns a list of all available tech research projects
	public List<int> GiveRes()
	{
		
		List<int> la2 = new List<int>();
		
		foreach(Tech u2 in lTech)
		{
			if(u2.vUnlocked && !u2.vBought)
			{
				la2.Add(lTech.IndexOf(u2));
				//Debug.Log("adding to list");
			}
		}
		
		return la2;
	}
	//buys a tech
	public void BuyTech(int num2, Ui a2)
	{
		lTech[num2].vUnlocked = true;
		lTech[num2].vBought = true;
		a2.StartRes(num2);
		a2.RefreshUp();
		a2.RefreshRes();
	}
	//unlocks further techs
	public void vUnlock(int tou)
	{
	vN.AddNotif("Research Finished!\nVisit Research\nTo Complete");
		vToUnlock = tou;
		//vFunishUnlock(tou);
	}
	public void vFunishUnlock(int tounl)
	{
		vU.vCurrRes = -1;
		vToUnlock = -1;
		if(lTech[tounl].vUpgrUnlock >= 0)
		{
			if(lUpgrades[lTech[tounl].vUpgrUnlock].vUnlocked == false)
			{
			vN.AddNotif("Upgrade Unlocked!\n " + lUpgrades[lTech[tounl].vUpgrUnlock].vName);
			}
			lUpgrades[lTech[tounl].vUpgrUnlock].vUnlocked = true;
			
		}
		if(lTech[tounl].vTechUnlock >= 0)
		{
			if(lTech[lTech[tounl].vTechUnlock].vUnlocked == false)
			{
			vN.AddNotif("Research Unlocked!\n " + lTech[lTech[tounl].vTechUnlock].vName);
			}
			lTech[lTech[tounl].vTechUnlock].vUnlocked = true;
			
		}
		vU.RefreshUp();
		vU.RefreshRes();
	}
	//buys an upgrade
	public void BuyUpgrade(int num, Ui a)
	{
		lUpgrades[num].vUnlocked = true;
		lUpgrades[num].vBought = true;
		if(lUpgrades[num].vUpgrUnlock >= 0)
		{
			if(lUpgrades[lUpgrades[num].vUpgrUnlock].vUnlocked == false)
			{
			vN.AddNotif("Upgrade Unlocked!\n " + lUpgrades[lUpgrades[num].vUpgrUnlock].vName);
			}
			lUpgrades[lUpgrades[num].vUpgrUnlock].vUnlocked = true;
			
		}
		if(lUpgrades[num].vTechUnlock >= 0)
		{
			if(lTech[lUpgrades[num].vTechUnlock].vUnlocked == false)
			{
			vN.AddNotif("Research Unlocked!\n " + lTech[lUpgrades[num].vTechUnlock].vName);
			}
			lTech[lUpgrades[num].vTechUnlock].vUnlocked = true;
			
		}
		
		CheckSpecial(num);
		a.RefreshUp();
		a.RefreshRes();
	}
	//checks for special function
	//also functions as a library for effects
	public void CheckSpecial(int nn)
	{
		if(lUpgrades[nn].vSpecial != "")
		{
			switch (lUpgrades[nn].vSpecial)
			{
			case "10HP":
				if(vT.vHealth == vT.vMaxHealth)
				{
					vT.vMoney += 10;
				}
				vT.vHealth += 10;
				if(vT.vHealth > vT.vMaxHealth)
				{
					vT.vHealth = vT.vMaxHealth;
				}
				break;
			case "FULLHP":
				if(vT.vHealth == vT.vMaxHealth)
				{
					vT.vMoney += 10;
				}
				vT.vHealth = vT.vMaxHealth;
				break;
			case "HPPLUS":
				vT.vMaxHealth *= 1.1f;
				vT.vHealth *= 1.1f;
				IncreasePrice(3);
				break;
			case "DMGPLUS":
				vT.vBulDmg *= 1.1f;
				IncreasePrice(4);
				break;
			case "BULSPEED":
				vT.vBulSpeed *= 1.1f;
				IncreasePrice(5);
				break;
			case "ROTSPEED":
				vT.damping *= 1.1f;
				IncreasePrice(6);
				break;
			}
		}
		if(lUpgrades[nn].vReusable)
		{
			lUpgrades[nn].vBought = false;
		}
	}
	//increase price
	void IncreasePrice(int pti)
	{
		int ipti = Mathf.CeilToInt(lUpgrades[pti].vPrice * 1.5f);
		lUpgrades[pti].vPrice = ipti;
	}
	
	//library of all upgrades
	void PopulateUpgrades() 
	{
		//0
		lUpgrades.Add(new Upgrade() 
		{vName = "Primitive Research", vUnlocked = true, vBought = false, vPage = 2, vPrice = 10, vUpgrUnlock = -1, vTechUnlock = 0});
		//1
		lUpgrades.Add(new Upgrade() 
		{vName = "Restore 10 HP", vUnlocked = false, vBought = false, vPage = 0, vPrice = 10, vUpgrUnlock = 2, vTechUnlock = -1, vReusable = true, vSpecial = "10HP"});
		//2
		lUpgrades.Add(new Upgrade() 
		{vName = "Restore Max HP", vUnlocked = false, vBought = false, vPage = 0, vPrice = 100, vUpgrUnlock = -1, vTechUnlock = -1, vReusable = true, vSpecial = "FULLHP"});
		//3
		lUpgrades.Add(new Upgrade() 
		{vName = "Increase Max HP", vUnlocked = false, vBought = false, vPage = 0, vPrice = 100, vUpgrUnlock = -1, vTechUnlock = -1, vReusable = true, vSpecial = "HPPLUS"});
		//4
		lUpgrades.Add(new Upgrade() 
		{vName = "Increase Power", vUnlocked = false, vBought = false, vPage = 0, vPrice = 100, vUpgrUnlock = -1, vTechUnlock = 3, vReusable = true, vSpecial = "DMGPLUS"});
		//5
		lUpgrades.Add(new Upgrade() 
		{vName = "Projectile Speed", vUnlocked = false, vBought = false, vPage = 0, vPrice = 100, vUpgrUnlock = -1, vTechUnlock = -1, vReusable = true, vSpecial = "BULSPEED"});
		//6
		lUpgrades.Add(new Upgrade() 
		{vName = "Rotation Speed", vUnlocked = false, vBought = false, vPage = 0, vPrice = 100, vUpgrUnlock = -1, vTechUnlock = -1, vReusable = true, vSpecial = "ROTSPEED"});
	}
	//library of all tech
	void PopulateTech() 
	{
		//0
		lTech.Add(new Tech() {vName = "Repairs", vUnlocked = false, vBought = false, vCool = 1000, vMaxCool = 1000, vPrice = 10, vUpgrUnlock = 1, vTechUnlock = 1});
		//1
		lTech.Add(new Tech() {vName = "Health", vUnlocked = false, vBought = false, vCool = 1000, vMaxCool = 1000, vPrice = 10, vUpgrUnlock = 3, vTechUnlock = 2});
		//2
		lTech.Add(new Tech() {vName = "Power", vUnlocked = false, vBought = false, vCool = 1000, vMaxCool = 1000, vPrice = 10, vUpgrUnlock = 4, vTechUnlock = -1});
		//3
		lTech.Add(new Tech() {vName = "Agility", vUnlocked = false, vBought = false, vCool = 1000, vMaxCool = 1000, vPrice = 10, vUpgrUnlock = 5, vTechUnlock = -1});
	}
}
