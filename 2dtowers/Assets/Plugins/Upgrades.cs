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
	public Meshes vM; 
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
	}
	[System.Serializable]
	public class Tech
	{
		public string vName;
		public int vPrice;
		
		public bool vUnlocked;
		public bool vBought;
		public string vSpecial = "";
		
		public int vCool;
		public int vMaxCool;
	}
	void Awake () 
	{
		vCam = GameObject.FindWithTag("MainCamera");
		vT = gameObject.GetComponent<Turret>();
		vU = vCam.GetComponent<Ui>();
		vN = vCam.GetComponent<Notif>();
		vM = gameObject.GetComponent<Meshes>();
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
		/*if(lTech[tounl].vUpgrUnlock >= 0)
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
			
		}*/
		CheckSpecialTech(tounl);
		vU.RefreshUp();
		vU.RefreshRes();
	}
	//buys an upgrade
	public void BuyUpgrade(int num, Ui a)
	{
		lUpgrades[num].vUnlocked = true;
		lUpgrades[num].vBought = true;
		/*if(lUpgrades[num].vUpgrUnlock >= 0)
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
			
		}*/
		
		CheckSpecial(num);
		a.RefreshUp();
		a.RefreshRes();
	}
	//unlocks an upgrade
	void UnlockUp(int ii)
	{
			/*if(lUpgrades[ii].vUnlocked == false)
			{
			vN.AddNotif("Upgrade Unlocked!\n " + lUpgrades[ii].vName);
			}*/
			lUpgrades[ii].vUnlocked = true;
	}
	//unlocks an tech
	void UnlockTech(int iii)
	{
			/*if(lTech[iii].vUnlocked == false)
			{
			vN.AddNotif("Research Unlocked!\n " + lTech[iii].vName);
			}*/
			lTech[iii].vUnlocked = true;
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
				vT.vBulDmg++;
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
				case "PRIMUNL":
				UnlockTech(0);
				vN.AddNotif("Research Unlocked!\n " + lTech[0].vName);
				break;
				case "STRUCTUNL":
				UnlockTech(1);
				vN.AddNotif("Research Unlocked!\n " + lTech[1].vName);
				break;
				case "PROJUNL":
				UnlockTech(2);
				vN.AddNotif("Research Unlocked!\n " + lTech[2].vName);
				break;
				case "ENEMUNL":
				UnlockTech(3);
				vN.AddNotif("Research Unlocked!\n " + lTech[3].vName);
				break;
				case "EFREQ":
				IncreasePrice(12);
				vT.vWaveFreq-=10;
				if(vT.vWaveFreq <= 0)
				{
				vT.vWaveFreq = 1;
				}
				break;
				case "ECOUNT":
				IncreasePrice(11);
				vT.vWaveSize++;
				break;
				case "ELVL":
				IncreasePrice(10);
				vT.eLvl++;
				vT.eSpeed *= 1.01f;
				vT.eDmg = vT.eLvl;
				vT.eHealth =1 + vT.eLvl;
				vT.eMoney += vT.eLvl;
				break;
				case "AR":
				UnlockTech(4);
				vN.AddNotif("Research Unlocked!\n " + lTech[4].vName);
				break;
				case "VISUP":
				UnlockTech(5);
				vN.AddNotif("Research Unlocked!\n " + lTech[5].vName);
				break;
				case "BASEI":
				vM.vCurrBase++;
				vM.EnableBase(vM.vCurrBase);
				vN.AddNotif("Base Mesh\n Upgrade!");
				break;
				case "GUNI":
				vM.vCurrGun++;
				vM.EnableGun(vM.vCurrGun);
				vN.AddNotif("Gun Mesh\n Upgrade!");
				break;
				case "SURFI":
				vM.vTexture++;
				vM.EnableTex(vM.vTexture);
				vN.AddNotif("Base Mesh\n Upgrade!");
				break;
				case "ARII":
				UnlockTech(7);
				vN.AddNotif("Research Unlocked!\n " + lTech[7].vName);
				break;
				case "EANATOMYII":
				UnlockTech(6);
				vN.AddNotif("Research Unlocked!\n " + lTech[6].vName);
				break;
				case "EDECHP":
				vT.eHealth *= 0.95f;
				IncreasePrice(20);
				//vN.AddNotif("Research Unlocked!\n " + lTech[6].vName);
				break;
				case "EDECDMG":
				vT.eDmg -= vT.eLvl;
				if(vT.eDmg <= 0)
				{
				vT.eDmg = 1;
				}
				IncreasePrice(21);
				//vN.AddNotif("Research Unlocked!\n " + lTech[6].vName);
				break;
				case "EDECSP":
				vT.eSpeed *= 0.95f;
				IncreasePrice(22);
				//vN.AddNotif("Research Unlocked!\n " + lTech[6].vName);
				break;
				case "VISUPII":
				UnlockTech(8);
				vN.AddNotif("Research Unlocked!\n " + lTech[8].vName);
				break;
			}
		}
		if(lUpgrades[nn].vReusable)
		{
			lUpgrades[nn].vBought = false;
		}
	}
	//checks for special function of research
	//also functions as a library for effects
	public void CheckSpecialTech(int nnn)
	{
		if(lTech[nnn].vSpecial != "")
		{
			switch (lTech[nnn].vSpecial)
			{
			case "PR":
			UnlockUp(7);
			UnlockUp(8);
			UnlockUp(9);
			UnlockUp(1);
			UnlockUp(2);
			UnlockUp(13);
			vN.AddNotif("Upgrades Unlocked!\n " + lUpgrades[7].vName +"\n"+lUpgrades[8].vName+"\n"+lUpgrades[9].vName+"\n"+lUpgrades[1].vName+"\n"+lUpgrades[2].vName+"\n"+lUpgrades[13].vName);
			break;
			case "SR":
			UnlockUp(3);
			UnlockUp(6);
			vN.AddNotif("Upgrades Unlocked!\n " + lUpgrades[3].vName +"\n"+lUpgrades[6].vName);
			break;
			case "PJR":
			UnlockUp(4);
			UnlockUp(5);
			vN.AddNotif("Upgrades Unlocked!\n " + lUpgrades[4].vName +"\n"+lUpgrades[5].vName);
			break;
			case "EA":
			UnlockUp(10);
			UnlockUp(11);
			UnlockUp(12);
			vN.AddNotif("Upgrades Unlocked!\n " + lUpgrades[10].vName +"\n"+lUpgrades[11].vName+"\n"+lUpgrades[12].vName);
			break;
			case "AR":
			UnlockUp(14);
			UnlockUp(18);
			UnlockUp(19);
			vN.AddNotif("Upgrades Unlocked!\n " + lUpgrades[14].vName+"\n"+lUpgrades[18].vName+"\n"+lUpgrades[19].vName);
			break;
			case "VR":
			UnlockUp(15);
			UnlockUp(16);
			UnlockUp(17);
			vN.AddNotif("Upgrades Unlocked!\n " + lUpgrades[15].vName+"\n"+lUpgrades[16].vName+"\n"+lUpgrades[17].vName);
			break;
			case "EAII":
			UnlockUp(20);
			UnlockUp(21);
			UnlockUp(22);
			vN.AddNotif("Upgrades Unlocked!\n " + lUpgrades[20].vName+"\n"+lUpgrades[21].vName+"\n"+lUpgrades[22].vName);
			break;
			case "ARII":
			////////////////
			UnlockUp(23);
			vN.AddNotif("Upgrades Unlocked!\n " + lUpgrades[23].vName);
			break;
			case "VRII":
			////////////////
			//UnlockUp(24);
			UnlockUp(24);
			UnlockUp(25);
			UnlockUp(26);
			vN.AddNotif("Upgrades Unlocked!\n " + lUpgrades[24].vName+"\n"+lUpgrades[25].vName+"\n"+lUpgrades[26].vName);
			break;
			}
		}
	}
	//increase price
	void IncreasePrice(int pti)
	{
		int ipti = Mathf.CeilToInt(lUpgrades[pti].vPrice * 1.1f);
		lUpgrades[pti].vPrice = ipti;
	}
	
	//library of all upgrades
	
	void PopulateUpgrades() 
	{
		//0
		lUpgrades.Add(new Upgrade() 
		{vName = "Primitive Research", vUnlocked = true, vBought = false, vPage = 2, vPrice = 10, vSpecial = "PRIMUNL"});
		//1
		lUpgrades.Add(new Upgrade() 
		{vName = "Restore 10 HP", vUnlocked = false, vBought = false, vPage = 0, vPrice = 10, vReusable = true, vSpecial = "10HP"});
		//2
		lUpgrades.Add(new Upgrade() 
		{vName = "Restore Max HP", vUnlocked = false, vBought = false, vPage = 0, vPrice = 100, vReusable = true, vSpecial = "FULLHP"});
		//3
		lUpgrades.Add(new Upgrade() 
		{vName = "Increase Max HP", vUnlocked = false, vBought = false, vPage = 0, vPrice = 100, vReusable = true, vSpecial = "HPPLUS"});
		//4
		lUpgrades.Add(new Upgrade() 
		{vName = "Increase Power", vUnlocked = false, vBought = false, vPage = 0, vPrice = 100, vReusable = true, vSpecial = "DMGPLUS"});
		//5
		lUpgrades.Add(new Upgrade() 
		{vName = "Projectile Speed", vUnlocked = false, vBought = false, vPage = 0, vPrice = 100, vReusable = true, vSpecial = "BULSPEED"});
		//6
		lUpgrades.Add(new Upgrade() 
		{vName = "Rotation Speed", vUnlocked = false, vBought = false, vPage = 0, vPrice = 100, vReusable = true, vSpecial = "ROTSPEED"});
		//7
		lUpgrades.Add(new Upgrade() 
		{vName = "Structural Research I", vUnlocked = false, vBought = false, vPage = 2, vPrice = 10, vSpecial = "STRUCTUNL"});
		//8
		lUpgrades.Add(new Upgrade() 
		{vName = "Enemy Anatomy I", vUnlocked = false, vBought = false, vPage = 2, vPrice = 10, vSpecial = "ENEMUNL"});
		//9
		lUpgrades.Add(new Upgrade() 
		{vName = "Projectile Research I", vUnlocked = false, vBought = false, vPage = 2, vPrice = 10, vSpecial = "PROJUNL"});
		//10
		lUpgrades.Add(new Upgrade() 
		{vName = "Increase Enemy Level", vUnlocked = false, vBought = false, vPage = 1, vPrice = 10, vReusable = true, vSpecial = "ELVL"});
		//11
		lUpgrades.Add(new Upgrade() 
		{vName = "Increase Enemy Count", vUnlocked = false, vBought = false, vPage = 1, vPrice = 10, vReusable = true, vSpecial = "ECOUNT"});
		//12
		lUpgrades.Add(new Upgrade() 
		{vName = "Increase Wave Frequency", vUnlocked = false, vBought = false, vPage = 1, vPrice = 10, vReusable = true, vSpecial = "EFREQ"});
		//13////////////////////////////
		lUpgrades.Add(new Upgrade() 
		{vName = "Advanced Research I", vUnlocked = false, vBought = false, vPage = 2, vPrice = 10, vReusable = false, vSpecial = "AR"});
		//14
		lUpgrades.Add(new Upgrade() 
		{vName = "Visual Research I", vUnlocked = false, vBought = false, vPage = 2, vPrice = 10, vReusable = false, vSpecial = "VISUP"});
		//15---
		lUpgrades.Add(new Upgrade() 
		{vName = "Base Visual I", vUnlocked = false, vBought = false, vPage = 2, vPrice = 10, vReusable = false, vSpecial = "BASEI"});
		//16
		lUpgrades.Add(new Upgrade() 
		{vName = "Gun Visual I", vUnlocked = false, vBought = false, vPage = 2, vPrice = 10, vReusable = false, vSpecial = "GUNI"});
		//17
		lUpgrades.Add(new Upgrade() 
		{vName = "Surface Visual I", vUnlocked = false, vBought = false, vPage = 2, vPrice = 10, vReusable = false, vSpecial = "SURFI"});
		//18
		lUpgrades.Add(new Upgrade() 
		{vName = "Advanced Research II", vUnlocked = false, vBought = false, vPage = 2, vPrice = 10, vReusable = false, vSpecial = "ARII"});
		//19
		lUpgrades.Add(new Upgrade() 
		{vName = "Enemy Anatomy II", vUnlocked = false, vBought = false, vPage = 2, vPrice = 10, vReusable = false, vSpecial = "EANATOMYII"});
		//20
		lUpgrades.Add(new Upgrade() 
		{vName = "Decrease Health", vUnlocked = false, vBought = false, vPage = 1, vPrice = 10, vReusable = true, vSpecial = "EDECHP"});
		//21
		lUpgrades.Add(new Upgrade() 
		{vName = "Decrease Damage", vUnlocked = false, vBought = false, vPage = 1, vPrice = 10, vReusable = true, vSpecial = "EDECDMG"});
		//22
		lUpgrades.Add(new Upgrade() 
		{vName = "Decrease Speed", vUnlocked = false, vBought = false, vPage = 1, vPrice = 10, vReusable = true, vSpecial = "EDECSP"});
		//23///////////////////////////
		lUpgrades.Add(new Upgrade() 
		{vName = "Visual Research II", vUnlocked = false, vBought = false, vPage = 2, vPrice = 10, vReusable = false, vSpecial = "VISUPII"});
		//24---
		lUpgrades.Add(new Upgrade() 
		{vName = "Base Visual II", vUnlocked = false, vBought = false, vPage = 2, vPrice = 10, vReusable = false, vSpecial = "BASEI"});
		//25
		lUpgrades.Add(new Upgrade() 
		{vName = "Gun Visual II", vUnlocked = false, vBought = false, vPage = 2, vPrice = 10, vReusable = false, vSpecial = "GUNI"});
		//26
		lUpgrades.Add(new Upgrade() 
		{vName = "Surface Visual II", vUnlocked = false, vBought = false, vPage = 2, vPrice = 10, vReusable = false, vSpecial = "SURFI"});
		
	}
	//library of all tech
	void PopulateTech() 
	{
		//0
		lTech.Add(new Tech() {vName = "Primitive Research", vUnlocked = false, vBought = false, vCool = 10, vMaxCool = 1000, vPrice = 10, vSpecial ="PR"});
		//1
		lTech.Add(new Tech() {vName = "Structural Research I", vUnlocked = false, vBought = false, vCool = 10, vMaxCool = 1000, vPrice = 10, vSpecial ="SR"});
		//2
		lTech.Add(new Tech() {vName = "Projectile Research I", vUnlocked = false, vBought = false, vCool = 10, vMaxCool = 1000, vPrice = 10, vSpecial ="PJR"});
		//3
		lTech.Add(new Tech() {vName = "Enemy Anatomy I", vUnlocked = false, vBought = false, vCool = 10, vMaxCool = 1000, vPrice = 10, vSpecial ="EA"});
		//4
		lTech.Add(new Tech() {vName = "Advanced Research I", vUnlocked = false, vBought = false, vCool = 10, vMaxCool = 1000, vPrice = 10, vSpecial ="AR"});
		//5
		lTech.Add(new Tech() {vName = "Visual Research I", vUnlocked = false, vBought = false, vCool = 10, vMaxCool = 1000, vPrice = 10, vSpecial ="VR"});
		//6
		lTech.Add(new Tech() {vName = "Enemy Anatomy II", vUnlocked = false, vBought = false, vCool = 10, vMaxCool = 1000, vPrice = 10, vSpecial ="EAII"});
		//7
		lTech.Add(new Tech() {vName = "Advanced Research II", vUnlocked = false, vBought = false, vCool = 10, vMaxCool = 1000, vPrice = 10, vSpecial ="ARII"});
		//8
		lTech.Add(new Tech() {vName = "Visual Research II", vUnlocked = false, vBought = false, vCool = 10, vMaxCool = 1000, vPrice = 10, vSpecial ="VRII"});
	}
}
