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
	}
	public void LoadOptions()
	{
	vN.vMaxCool = PlayerPrefs.GetFloat("vMaxCoolOPT");
	vS.vMuted= PlayerPrefs.GetFloat("vMuted");
	vCT.vAmount= PlayerPrefs.GetFloat("vAmountZOOM");
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
		PlayerPrefs.SetInt("vToUnlock", vU.vToUnlock);
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
			PlayerPrefs.SetInt(vU.lTech.IndexOf(t) + "vPriceT", t.vPrice);
			PlayerPrefs.SetInt(vU.lTech.IndexOf(t) + "vCoolT", t.vCool);
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
	
}