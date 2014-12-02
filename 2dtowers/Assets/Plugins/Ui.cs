using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Ui : MonoBehaviour 
{
	public GUISkin skin;
	public GameObject vTurret;
	public Turret vT;
	public Waves vW;
	public Upgrades vU;
	public Sound vS;
	public Notif vN;
	
	public Texture2D vStatTex;
	public Texture2D vUpgradesTex;
	public Texture2D vResTex;
	public Texture2D vOptTex;
	
	public Texture2D vCloseTex;
	
	//bools for the states
	public bool bUpgrades = false;
	public bool bRes = false;
	public bool bStat = false;
	public bool bOpt = false;
	
	//window magic
	private Rect WindowStatus;
	private Rect WindowUpgrades;
	private Rect WindowResearch;
	private Rect WindowOptions;
	
	//progress bar
	public Texture2D vProgressFrame;
	public Texture2D vProgressBar;
	
	//upgrade page selection
	//0Turret,1Enemies,2Tech
	public int vUpSel = 0;
	public List<int> la = new List<int>();
	public List<int> la2 = new List<int>();
	//scrolly stuffs
	public Vector2 scrollPosition = Vector2.zero;
	public int scrollHeight = 1;
	public Vector2 scrollPosition2 = Vector2.zero;
	public int scrollHeight2 = 1;
	
	//research page stuffs
	public int vCurrRes = -1;
	//notification stuffs
	public string vNotificationText = "";

	
	void Awake () 
	{
		WindowUpgrades = new Rect(70, 10, 300, 400);
		WindowResearch = new Rect(70, 10, 300, 400);
		WindowStatus = new Rect(70, 10, 200, 360);
		WindowOptions = new Rect(70, 10, 300, 400);
		
		vT = vTurret.GetComponent<Turret>();
		vW = vTurret.GetComponent<Waves>();
		vU = vTurret.GetComponent<Upgrades>();
		vS = gameObject.GetComponent<Sound>();
		vN = gameObject.GetComponent<Notif>();

	}
	
	void FixedUpdate()
	{
		if(vCurrRes != -1)
		{
			
			if(vU.lTech[vCurrRes].vCool <= 0)
			{
			if(vU.vToUnlock == -1){
				FinishRes(vCurrRes);}
			}
			else
			{
				vU.lTech[vCurrRes].vCool--;
			}
		}
		
	}
	
	void OnGUI () 
	{
		GUI.skin = skin;
		if(GUI.Button(new Rect(10,10,50,50), vStatTex))
		{
			bStat = !bStat;
			vS.PlayClickY();
		}
		if(GUI.Button(new Rect(10,130,50,50), vResTex))
		{
			bRes = !bRes;
			RefreshRes();
			vS.PlayClickY();
		}
		if(GUI.Button(new Rect(10,70,50,50), vUpgradesTex))
		{
			bUpgrades = !bUpgrades;
			vS.PlayClickY();
		}
		if(GUI.Button(new Rect(10,190,50,50), vOptTex))
		{
			bOpt = !bOpt;
			vS.PlayClickY();
		}
		///////////////////
		if(bUpgrades)
		{
			DrawUpgrades();
		}
		if(bRes)
		{
			DrawRes();
		}
		if(bStat)
		{
			DrawStat();
		}
		if(bOpt)
		{
			DrawOpt();
		}
		
		Notif();
		
	}
	//////
	void DrawStat()
	{
		WindowStatus = GUI.Window(2, WindowStatus, fWindowStat, "");
	}
	void fWindowStat(int w3)
	{
		GUI.Label(new Rect(20,10,200,50),"<size=24><color=white>Status</color></size>");
		if(GUI.Button(new Rect(150,10,42,42), vCloseTex, "label"))
		{
			bStat = !bStat;
			vS.PlayClickN();
		}
		
		//money
		GUI.Label(new Rect(20,60,200,30),"<size=12><color=black>Money: " + vT.vMoney + "u"+"</color></size>");
		//lvl
		GUI.Label(new Rect(20,80,200,30),"<size=12><color=black>Level: " + vT.vLvl + "</color></size>");
		//exp
		GUI.Label(new Rect(20,100,200,30),"<size=12><color=black>Exp: " + vT.vCurrExp.ToString("F1") + "/" + vT.vExpLeft.ToString("F1") + "</color></size>");
		GUI.DrawTexture(new Rect(10, 120, 180, 15), vProgressFrame, ScaleMode.StretchToFill, true, 10.0F);
		//progress bar
		float w4 = fWidth(180F, vT.vCurrExp , vT.vExpLeft, false);
		GUI.DrawTexture(new Rect(11,121,w4,13),vProgressBar, ScaleMode.StretchToFill, true, 10.0F);
		//health
		GUI.Label(new Rect(20,140,200,30),"<size=12><color=black>Health: " + vT.vHealth.ToString("F1") + "/" + vT.vMaxHealth.ToString("F1") + "</color></size>");
		GUI.DrawTexture(new Rect(10, 160, 180, 15), vProgressFrame, ScaleMode.StretchToFill, true, 10.0F);
		//progress bar
		float w = fWidth(180F, vT.vHealth, vT.vMaxHealth, false);
		GUI.DrawTexture(new Rect(11,161,w,13),vProgressBar, ScaleMode.StretchToFill, true, 10.0F);
		
		//cooldown
		GUI.Label(new Rect(20,180,200,30),"<size=12><color=black>Cooldown</color></size>");
		GUI.DrawTexture(new Rect(10, 200, 180, 15), vProgressFrame, ScaleMode.StretchToFill, true, 10.0F);
		//progress bad
		float w2 = fWidth(180F, vT.vCool, vT.vMaxCool, true);
		GUI.DrawTexture(new Rect(11,201,w2,13),vProgressBar, ScaleMode.StretchToFill, true, 10.0F);
		//wave
		GUI.Label(new Rect(20,220,200,30),"<size=12><color=black>Next Wave</color></size>");
		GUI.DrawTexture(new Rect(10, 240, 180, 15), vProgressFrame, ScaleMode.StretchToFill, true, 10.0F);
		//progress bad
		float ww3 = fWidth(180F, vW.vF, vT.vWaveFreq, true);
		GUI.DrawTexture(new Rect(11,241,ww3,13),vProgressBar, ScaleMode.StretchToFill, true, 10.0F);
		//dmg
		GUI.Label(new Rect(20,260,200,30),"<size=12><color=black>Bullet Damage: " + vT.vBulDmg + ""+"</color></size>");
		//lvl
		GUI.Label(new Rect(20,280,200,30),"<size=12><color=black>Bullet Speed: " + vT.vBulSpeed + "</color></size>");
		//lvl
		GUI.Label(new Rect(20,300,200,30),"<size=12><color=black>Turret Speed: " + vT.damping + "</color></size>");
		GUI.DragWindow();
	}
	float fWidth(float max, float vc, float vmax, bool reverse)
	{
		//progress bar
		float maxx = max;
		float a = vc/vmax;
		a *= maxx;
		if(reverse){
			a = maxx-a;
		}
		a--;
		return a;
	}
	//////
	void DrawUpgrades()
	{
		WindowUpgrades = GUI.Window(0, WindowUpgrades, fWindowUpgrades, "");
	}
	void fWindowUpgrades(int w1)
	{
		GUI.Label(new Rect(20,10,200,50),"<size=24><color=white>Upgrades</color></size>");
		if(GUI.Button(new Rect(250,10,42,42), vCloseTex, "label"))
		{
		vS.PlayClickN();
			bUpgrades = !bUpgrades;
		}
		if(GUI.Button(new Rect(10,54,90,20), "<color=black>Turret</color>"))
		{
			vUpSel = 0;
			RefreshUp(vUpSel);
			vS.PlayClickY();
		}
		if(GUI.Button(new Rect(105,54,90,20), "<color=black>Enemies</color>"))
		{
			vUpSel = 1;
			RefreshUp(vUpSel);
			vS.PlayClickY();
		}
		if(GUI.Button(new Rect(200,54,90,20), "<color=black>Tech</color>"))
		{
			vUpSel = 2;
			RefreshUp(vUpSel);
			vS.PlayClickY();
		}
		DrawUpgradePage();
		
		GUI.DragWindow();
	}
	void DrawUpgradePage()
	{
		scrollPosition = GUI.BeginScrollView(new Rect(10, 80, 280, 300), scrollPosition, new Rect(0, 0, 260, scrollHeight));
		foreach(int ind in la)
		{
			if(GUI.Button(new Rect(20,la.IndexOf(ind)*20,240,20),"<color=black>" + vU.lUpgrades[ind].vName + ": " + vU.lUpgrades[ind].vPrice + "u</color>"))
			{
				if(vT.vMoney >= vU.lUpgrades[ind].vPrice)
				{
					vT.vMoney -= vU.lUpgrades[ind].vPrice;
					vU.BuyUpgrade(ind, this);
					vS.PlayCash();
				}
				else
				{
				vS.PlayClickN();
				}
			}
		}
		GUI.EndScrollView();
	}
	//////
	void DrawRes()
	{
		
		WindowResearch = GUI.Window(1, WindowResearch, fWindowRes, "");
	}
	void fWindowRes(int w2)
	{
		GUI.Label(new Rect(20,10,200,50),"<size=24><color=white>Research</color></size>");
		if(GUI.Button(new Rect(250,10,42,42), vCloseTex, "label"))
		{
		vS.PlayClickN();
			bRes = !bRes;
		}
		//current project status
		GUI.DrawTexture(new Rect(10, 80, 280, 15), vProgressFrame, ScaleMode.StretchToFill, true, 10.0F);
		
		if(vCurrRes == -1)
		{
			GUI.Label(new Rect(20,60,200,30),"<size=12><color=black>No Active Research</color></size>");
		}
		else
		{
			GUI.Label(new Rect(20,60,200,30),"<size=12><color=black>"+ vU.lTech[vCurrRes].vName +"</color></size>");
			
			float www = fWidth(280F, vU.lTech[vCurrRes].vCool, vU.lTech[vCurrRes].vMaxCool, true);
			GUI.DrawTexture(new Rect(11,81,www,13),vProgressBar, ScaleMode.StretchToFill, true, 10.0F);
			
			if(vU.vToUnlock != -1)
			{
				if(GUI.Button(new Rect(10,100,280,20), "<color=black>Claim Results</color>"))
				{
					vU.vFunishUnlock(vU.vToUnlock);
					vS.PlayCash();
				}
			}
			
			
		}
		
		//progress bad
		
		
		scrollPosition2 = GUI.BeginScrollView(new Rect(10, 160, 280, 220), scrollPosition2, new Rect(0, 0, 260, scrollHeight2));
		
		foreach(int ind2 in la2)
		{
			if(GUI.Button(new Rect(20,la2.IndexOf(ind2)*20,240,20), "<color=black>" + vU.lTech[ind2].vName + ": " + vU.lTech[ind2].vPrice + "u</color>"))
			{
				if(vT.vMoney >= vU.lTech[ind2].vPrice && vCurrRes == -1)
				{
					vT.vMoney -= vU.lTech[ind2].vPrice;
					vU.BuyTech(ind2, this);
					vS.PlayCash();
				}
				else
				{
				vS.PlayClickN();
				}
			}
		}
		
		GUI.EndScrollView();
		GUI.DragWindow();
	}
	//////
	void DrawOpt()
	{
		WindowOptions = GUI.Window(3, WindowOptions, fWindowOpt, "");
	}
	void fWindowOpt(int w4)
	{
		GUI.Label(new Rect(20,10,200,50),"<size=24><color=white>Options</color></size>");
		if(GUI.Button(new Rect(250,10,42,42), vCloseTex, "label"))
		{
		vS.PlayClickN();
			bOpt = !bOpt;
		}
		GUI.DragWindow();
	}
	
	//refresh upgrade list for a given page
	public void RefreshUp(int pg)
	{
		
		la = vU.GiveUpgrades(pg);
		scrollHeight = 20*la.Count+1;
	}
	public void RefreshUp()
	{
		
		la = vU.GiveUpgrades(vUpSel);
		scrollHeight = 20*la.Count+1;
	}
	//
	public void RefreshRes()
	{
		la2 = new List<int>();
		la2 = vU.GiveRes();
		scrollHeight2 = 20*la2.Count+1;
	}
	public void StartRes(int res)
	{
		vCurrRes = res;
	}
	public void FinishRes(int resf)
	{
		
		vU.vUnlock(resf);
	}
	//internal ui function
	void Notif()
	{
		if(vNotificationText != "")
		{
			GUI.Box(new Rect(10,490,200,100), vNotificationText);
		}
	}
	//called by notification manager
	public void PlayNotif(string inp)
	{
		vNotificationText = inp;

	}
}
