using UnityEngine;
using System.Collections;

public class S : MonoBehaviour 
{
	public Turret vT;
	public Upgrades vU;
	public Ui vUI;
	public Meshes vM;
	//option stuff
	public Sound vS;
	public Notif vN;
	public CameraToggler vCT;
	
	public void SaveAll()
	{
		PlayerPrefs.SetInt("vExists", 1);
		//
		SaveTurret();
		SaveOptions();
		SaveMeshes();
		SaveUpgrades();
	}
	
	
	public void LoadAll()
	{
		//
		LoadTurret();
		LoadOptions();
		LoadMeshes();
		LoadUpgrades();
		
		vUI.RefreshUp(0);
		vUI.RefreshUp(1);
		vUI.RefreshUp(2); 
		vUI.RefreshRes();
	}
	
	public void SaveTurret()
	{
		PlayerPrefs.SetFloat("damping", vT.damping);
		PlayerPrefs.SetFloat("vRange", vT.vRange);
		
		PlayerPrefs.SetFloat("vCool", vT.vCool);
		PlayerPrefs.SetFloat("vMaxCool", vT.vMaxCool);
		
		PlayerPrefs.SetFloat("vHealth", vT.vHealth);
		PlayerPrefs.SetFloat("vMaxHealth", vT.vMaxHealth);
		
		PlayerPrefs.SetInt("vMoney", vT.vMoney);
		
		PlayerPrefs.SetInt("vLvl", vT.vLvl);
		PlayerPrefs.SetFloat("vCurrExp", vT.vCurrExp);
		
		PlayerPrefs.SetFloat("vBulSpeed", vT.vBulSpeed);
		PlayerPrefs.SetFloat("vBulDmg", vT.vBulDmg);
		
		PlayerPrefs.SetInt("vWaveSize", vT.vWaveSize);
		PlayerPrefs.SetInt("vWaveFreq", vT.vWaveFreq);
		PlayerPrefs.SetInt("eLvl", vT.eLvl);
		PlayerPrefs.SetInt("eDmg", vT.eDmg);
		PlayerPrefs.SetInt("eMoney", vT.eMoney);
		PlayerPrefs.SetInt("eExp", vT.eExp);
		
		PlayerPrefs.SetFloat("eSpeed", vT.eSpeed);
		PlayerPrefs.SetFloat("eHealth", vT.eHealth);
	}
	public void LoadTurret()
	{
		vT.damping=PlayerPrefs.GetFloat("damping");
		vT.vRange=PlayerPrefs.GetFloat("vRange");
		
		vT.vCool=PlayerPrefs.GetFloat("vCool");
		vT.vMaxCool=PlayerPrefs.GetFloat("vMaxCool");
		
		vT.vHealth=PlayerPrefs.GetFloat("vHealth");
		vT.vMaxHealth=PlayerPrefs.GetFloat("vMaxHealth");
		
		vT.vMoney=PlayerPrefs.GetInt("vMoney");
		
		vT.vLvl=PlayerPrefs.GetInt("vLvl");
		vT.vCurrExp=PlayerPrefs.GetFloat("vCurrExp");
		
		vT.vBulSpeed=PlayerPrefs.GetFloat("vBulSpeed");
		vT.vBulDmg=PlayerPrefs.GetFloat("vBulDmg");
		
		vT.vWaveSize=PlayerPrefs.GetInt("vWaveSize");
		vT.vWaveFreq=PlayerPrefs.GetInt("vWaveFreq");
		vT.eLvl=PlayerPrefs.GetInt("eLvl");
		vT.eDmg=PlayerPrefs.GetInt("eDmg");
		vT.eMoney=PlayerPrefs.GetInt("eMoney");
		vT.eExp=PlayerPrefs.GetInt("eExp");
		
		vT.eSpeed=PlayerPrefs.GetFloat("eSpeed");
		vT.eHealth=PlayerPrefs.GetFloat("eHealth");
	}
	
	public void SaveOptions()
	{
		PlayerPrefs.SetFloat("vMaxCoolOPT", vN.vMaxCool);
		PlayerPrefs.SetFloat("vMuted", vS.vMuted);
		PlayerPrefs.SetFloat("vAmountZOOM", vCT.vAmount);
		PlayerPrefs.SetInt("vParticles", vT.vParticles);
	}
	public void LoadOptions()
	{
		vN.vMaxCool = PlayerPrefs.GetFloat("vMaxCoolOPT");
		vS.vMuted= PlayerPrefs.GetFloat("vMuted");
		vCT.vAmount= PlayerPrefs.GetFloat("vAmountZOOM");
		vT.vParticles=PlayerPrefs.GetInt("vParticles");
	}
	
	public void SaveMeshes()
	{
		PlayerPrefs.SetInt("vCurrBase", vM.vCurrBase);
		PlayerPrefs.SetInt("vCurrGun", vM.vCurrGun);
		PlayerPrefs.SetInt("vTexture", vM.vTexture);
	}
	public void LoadMeshes()
	{
		vM.vCurrBase=PlayerPrefs.GetInt("vCurrBase");
		vM.vCurrGun=PlayerPrefs.GetInt("vCurrGun");
		vM.vTexture=PlayerPrefs.GetInt("vTexture");
		vM.EnableBase(vM.vCurrBase);
		vM.EnableGun(vM.vCurrGun);
		vM.EnableTex(vM.vTexture);
	}
	public void SaveUpgrades()
	{
		if(vU != null)
		PlayerPrefs.SetInt("vToUnlock", vU.vToUnlock);
		if(vUI != null)
		PlayerPrefs.SetInt("vCurrRes", vUI.vCurrRes);
		//upgrades
		foreach(Upgrades.Upgrade u in vU.lUpgrades)
		{
			PlayerPrefs.SetInt(vU.lUpgrades.IndexOf(u) + "vPrice", u.vPrice);
			PlayerPrefs.SetInt(vU.lUpgrades.IndexOf(u) + "vUnlocked", ReturnInt(u.vUnlocked));
			PlayerPrefs.SetInt(vU.lUpgrades.IndexOf(u) + "vBought", ReturnInt(u.vBought));
		}
		
		//tech
		foreach(Upgrades.Tech t in vU.lTech)
		{
			PlayerPrefs.SetInt(vU.lTech.IndexOf(t) + "vPriceT", t.vPrice);
			PlayerPrefs.SetInt(vU.lTech.IndexOf(t) + "vCoolT", t.vCool);
			PlayerPrefs.SetInt(vU.lTech.IndexOf(t) + "vMaxCoolT", t.vMaxCool);
			PlayerPrefs.SetInt(vU.lTech.IndexOf(t) + "vUnlockedT", ReturnInt(t.vUnlocked));
			PlayerPrefs.SetInt(vU.lTech.IndexOf(t) + "vBoughtT", ReturnInt(t.vBought));
		}
	}
	public void LoadUpgrades()
	{
		vU.vToUnlock=PlayerPrefs.GetInt("vToUnlock");
		vUI.vCurrRes = PlayerPrefs.GetInt("vCurrRes");
		//upgrades
		foreach(Upgrades.Upgrade u in vU.lUpgrades)
		{
			u.vPrice = PlayerPrefs.GetInt(vU.lUpgrades.IndexOf(u) + "vPrice");
			u.vUnlocked = ReturnBool(PlayerPrefs.GetInt(vU.lUpgrades.IndexOf(u) + "vUnlocked"));
			u.vBought = ReturnBool(PlayerPrefs.GetInt(vU.lUpgrades.IndexOf(u) + "vBought"));
		}
		//tech
		foreach(Upgrades.Tech t in vU.lTech)
		{
			t.vPrice = PlayerPrefs.GetInt(vU.lTech.IndexOf(t) + "vPriceT");
			t.vCool = PlayerPrefs.GetInt(vU.lTech.IndexOf(t) + "vCoolT");
			t.vMaxCool = PlayerPrefs.GetInt(vU.lTech.IndexOf(t) + "vMaxCoolT");
			t.vUnlocked = ReturnBool(PlayerPrefs.GetInt(vU.lTech.IndexOf(t) + "vUnlockedT"));
			t.vBought = ReturnBool(PlayerPrefs.GetInt(vU.lTech.IndexOf(t) + "vBoughtT"));
			
		}
		
	}
	//converts a bool into an int
	public int ReturnInt(bool bo)
	{
		if(bo){return 1;}else{return 0;}
	}
	//converts an int into a bool
	public bool ReturnBool(int inn)
	{
		if(inn == 1){return true;}else{return false;}
	}
	[ContextMenu ("ResetAll")]
	public void ResetAll()
	{
		PlayerPrefs.SetInt("vExists", 0);
		PlayerPrefs.SetFloat("damping", 1f);
		PlayerPrefs.SetFloat("vRange", 100);
		
		PlayerPrefs.SetFloat("vCool", 200);
		PlayerPrefs.SetFloat("vMaxCool", 200);
		
		PlayerPrefs.SetFloat("vHealth", 100f);
		PlayerPrefs.SetFloat("vMaxHealth", 100f);
		
		PlayerPrefs.SetInt("vMoney", 0);
		
		PlayerPrefs.SetInt("vLvl", 1);
		PlayerPrefs.SetFloat("vCurrExp", 0f);
		
		PlayerPrefs.SetFloat("vBulSpeed", 1f);
		PlayerPrefs.SetFloat("vBulDmg", 2f);
		
		PlayerPrefs.SetInt("vParticles",1);
		
		PlayerPrefs.SetInt("vWaveSize", 1);
		PlayerPrefs.SetInt("vWaveFreq", 5000);
		PlayerPrefs.SetInt("eLvl", 1);
		PlayerPrefs.SetInt("eDmg", 2);
		PlayerPrefs.SetInt("eMoney", 2);
		PlayerPrefs.SetInt("eExp", 2);
		
		PlayerPrefs.SetFloat("eSpeed", 1f);
		PlayerPrefs.SetFloat("eHealth", 2f);
		////////////////
		PlayerPrefs.SetFloat("vMaxCoolOPT", 200);
		PlayerPrefs.SetFloat("vMuted", 1f);
		PlayerPrefs.SetFloat("vAmountZOOM", 0.5f);
		/////////////////////
		PlayerPrefs.SetInt("vCurrBase", 0);
		PlayerPrefs.SetInt("vCurrGun", 0);
		PlayerPrefs.SetInt("vTexture", 0);
		//////////
		PlayerPrefs.SetInt("vToUnlock", -1);
		PlayerPrefs.SetInt("vCurrRes", -1);
		//////////////////////////
		//upgrades
		foreach(Upgrades.Upgrade u in vU.lUpgrades)
		{
			
			PlayerPrefs.SetInt(vU.lUpgrades.IndexOf(u) + "vUnlocked", 0);
			PlayerPrefs.SetInt(vU.lUpgrades.IndexOf(u) + "vBought", 0);
		}
		PlayerPrefs.SetInt("0vUnlocked", 1);
		PlayerPrefs.SetInt("0vPrice", 10);
		PlayerPrefs.SetInt("1vPrice", 10);
		PlayerPrefs.SetInt("2vPrice", 100);
		PlayerPrefs.SetInt("3vPrice", 1000);
		PlayerPrefs.SetInt("4vPrice", 100);
		PlayerPrefs.SetInt("5vPrice", 50);
		PlayerPrefs.SetInt("6vPrice", 10);
		PlayerPrefs.SetInt("7vPrice", 50);
		PlayerPrefs.SetInt("8vPrice", 100);
		PlayerPrefs.SetInt("9vPrice", 50);
		PlayerPrefs.SetInt("10vPrice", 100);
		PlayerPrefs.SetInt("11vPrice", 100);
		PlayerPrefs.SetInt("12vPrice", 10);
		PlayerPrefs.SetInt("13vPrice", 1000);
		PlayerPrefs.SetInt("14vPrice", 100);
		PlayerPrefs.SetInt("15vPrice", 100);
		PlayerPrefs.SetInt("16vPrice", 100);
		PlayerPrefs.SetInt("17vPrice", 100);
		PlayerPrefs.SetInt("18vPrice", 5000);
		PlayerPrefs.SetInt("19vPrice", 500);
		PlayerPrefs.SetInt("20vPrice", 500);
		PlayerPrefs.SetInt("21vPrice", 500);
		PlayerPrefs.SetInt("22vPrice", 500);
		PlayerPrefs.SetInt("23vPrice", 1000);
		PlayerPrefs.SetInt("24vPrice", 1000);
		PlayerPrefs.SetInt("25vPrice", 1000);
		PlayerPrefs.SetInt("26vPrice", 1000);
		///fukkk remember to set all of em
		//tech
		foreach(Upgrades.Tech t in vU.lTech)
		{
			
			PlayerPrefs.SetInt(vU.lTech.IndexOf(t) + "vUnlockedT", 0);
			PlayerPrefs.SetInt(vU.lTech.IndexOf(t) + "vBoughtT", 0);
		}
		PlayerPrefs.SetInt("0vPriceT", 10);
		PlayerPrefs.SetInt("1vPriceT", 10);
		PlayerPrefs.SetInt("2vPriceT", 10);
		PlayerPrefs.SetInt("3vPriceT", 10);
		PlayerPrefs.SetInt("4vPriceT", 1000);
		PlayerPrefs.SetInt("5vPriceT", 100);
		PlayerPrefs.SetInt("6vPriceT", 1000);
		PlayerPrefs.SetInt("7vPriceT", 5000);
		PlayerPrefs.SetInt("8vPriceT", 1000);
		
		
		
		PlayerPrefs.SetInt("0vCoolT", 100);
		PlayerPrefs.SetInt("1vCoolT", 1000);
		PlayerPrefs.SetInt("2vCoolT", 1000);
		PlayerPrefs.SetInt("3vCoolT", 1000);
		PlayerPrefs.SetInt("4vCoolT", 3000);
		PlayerPrefs.SetInt("5vCoolT", 1000);
		PlayerPrefs.SetInt("6vCoolT", 3000);
		PlayerPrefs.SetInt("7vCoolT", 5000);
		PlayerPrefs.SetInt("8vCoolT", 2000);
		
			
		PlayerPrefs.SetInt("0vMaxCoolT", 100);
		PlayerPrefs.SetInt("1vMaxCoolT", 1000);
		PlayerPrefs.SetInt("2vMaxCoolT", 1000);
		PlayerPrefs.SetInt("3vMaxCoolT", 1000);
		PlayerPrefs.SetInt("4vMaxCoolT", 3000);
		PlayerPrefs.SetInt("5vMaxCoolT", 1000);
		PlayerPrefs.SetInt("6vMaxCoolT", 3000);
		PlayerPrefs.SetInt("7vMaxCoolT", 5000);
		PlayerPrefs.SetInt("8vMaxCoolT", 2000);
		///fukkk remember to set all of em
		//SaveUpgrades();
		
		
	}
	
	public void CallLoadAll()
	{
		LoadAll();
	}
}
