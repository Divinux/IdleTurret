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
	public CameraToggler vC;
	public S vSave;
	
	public Texture2D vStatTex;
	public Texture2D vUpgradesTex;
	public Texture2D vResTex;
	public Texture2D vOptTex;
	public Texture2D vCamTex;
	
	public Texture2D vCloseTex;
	public Texture2D vBUP;
	public Texture2D vBDN;
	
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
	
	//menu status
	public int vButtonStatus = 0;
	//progress bar
	public Texture2D vProgressFrame;
	public Texture2D vProgressBar;
	//status page
	public int vStatSel = 0;
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
	
	
	//option page stuffs
	public int vStatOpt = 0;
	public string vMuteButtonText = "";
	public string vParticleButtonText = "";

	
	void Awake () 
	{
		WindowUpgrades = new Rect(70, 10, 300, 400);
		WindowResearch = new Rect(70, 10, 300, 400);
		WindowStatus = new Rect(70, 10, 200, 360);
		WindowOptions = new Rect(70, 10, 300, 220);

		vT = vTurret.GetComponent<Turret>();
		vW = vTurret.GetComponent<Waves>();
		vU = vTurret.GetComponent<Upgrades>();
		vS = gameObject.GetComponent<Sound>();
		vN = gameObject.GetComponent<Notif>();
		vC = gameObject.GetComponent<CameraToggler>();
		vSave = gameObject.GetComponent<S>();

	}
	void Update()
	{
		if(Input.GetKeyDown("s"))
		{
			vS.PlayClickY();
			bStat = !bStat;
		}
		if(Input.GetKeyDown("u"))
		{
			vS.PlayClickY();
			bUpgrades = !bUpgrades;
		}
		if(Input.GetKeyDown("r"))
		{
			vS.PlayClickY();
			bRes = !bRes;
		}
		if(Input.GetKeyDown("o"))
		{
			vS.PlayClickY();
			bOpt = !bOpt;
		}
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
		if(vButtonStatus == 1)
		{
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
		if(GUI.Button(new Rect(10,250,50,50), vOptTex))
		{
			bOpt = !bOpt;
			vS.PlayClickY();
		}
		if(GUI.Button(new Rect(10,190,50,50), vCamTex))
		{
			vC.Switch();
			vS.PlayClickY();
		}
		if(GUI.Button(new Rect(10,310,50,30), vBUP))
		{
			vButtonStatus = 0;
			vS.PlayClickY();
		}
		}
		else
		{
		if(GUI.Button(new Rect(10,10,50,30), vBDN))
		{
			vButtonStatus = 1;
			vS.PlayClickY();
		}
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
		if(GUI.Button(new Rect(10,54,90,20), "<color=black>Main</color>"))
		{
			vS.PlayClickY();
			vStatSel = 0;
		}
		if(GUI.Button(new Rect(100,54,90,20), "<color=black>Enemies</color>"))
		{
			vS.PlayClickY();
			vStatSel = 1;
		}
		if(vStatSel == 0){
			//money
			GUI.Label(new Rect(20,80,200,30),"<size=12><color=black>Money: " + vT.vMoney + "u"+"</color></size>");
			//lvl
			GUI.Label(new Rect(20,100,200,30),"<size=12><color=black>Level: " + vT.vLvl + "</color></size>");
			//exp
			GUI.Label(new Rect(20,120,200,30),"<size=12><color=black>Exp: " + vT.vCurrExp.ToString("F1") + "/" + vT.vExpLeft.ToString("F1") + "</color></size>");
			GUI.DrawTexture(new Rect(10, 140, 180, 15), vProgressFrame, ScaleMode.StretchToFill, true, 10.0F);
			//progress bar
			float w4 = fWidth(180F, vT.vCurrExp , vT.vExpLeft, false);
			GUI.DrawTexture(new Rect(11,141,w4,13),vProgressBar, ScaleMode.StretchToFill, true, 10.0F);
			//health
			GUI.Label(new Rect(20,160,200,30),"<size=12><color=black>Health: " + vT.vHealth.ToString("F1") + "/" + vT.vMaxHealth.ToString("F1") + "</color></size>");
			GUI.DrawTexture(new Rect(10, 180, 180, 15), vProgressFrame, ScaleMode.StretchToFill, true, 10.0F);
			//progress bar
			float w = fWidth(180F, vT.vHealth, vT.vMaxHealth, false);
			GUI.DrawTexture(new Rect(11,181,w,13),vProgressBar, ScaleMode.StretchToFill, true, 10.0F);
			
			//cooldown
			GUI.Label(new Rect(20,200,200,30),"<size=12><color=black>Cooldown: "+ vT.vMaxCool.ToString("F1") +"ms</color></size>");
			GUI.DrawTexture(new Rect(10, 220, 180, 15), vProgressFrame, ScaleMode.StretchToFill, true, 10.0F);
			//progress bad
			float w2 = fWidth(180F, vT.vCool, vT.vMaxCool, true);
			GUI.DrawTexture(new Rect(11,221,w2,13),vProgressBar, ScaleMode.StretchToFill, true, 10.0F);
			//wave
			GUI.Label(new Rect(20,240,200,30),"<size=12><color=black>Next Wave: "+vT.vWaveFreq.ToString("F1")+"ms</color></size>");
			GUI.DrawTexture(new Rect(10, 260, 180, 15), vProgressFrame, ScaleMode.StretchToFill, true, 10.0F);
			//progress bad
			float ww3 = fWidth(180F, vW.vF, vT.vWaveFreq, true);
			GUI.DrawTexture(new Rect(11,261,ww3,13),vProgressBar, ScaleMode.StretchToFill, true, 10.0F);
			//dmg
			GUI.Label(new Rect(20,280,200,30),"<size=12><color=black>Bullet Damage: " + vT.vBulDmg.ToString("F1") + ""+"</color></size>");
			//lvl
			GUI.Label(new Rect(20,300,200,30),"<size=12><color=black>Bullet Speed: " + vT.vBulSpeed.ToString("F1") + "</color></size>");
			//lvl
			GUI.Label(new Rect(20,320,200,30),"<size=12><color=black>Turret Speed: " + vT.damping.ToString("F1") + "</color></size>");
		}
		else
		{
			//lvl
			GUI.Label(new Rect(20,80,200,30),"<size=12><color=black>Level: " + vT.eLvl + "</color></size>");
			//lvl
			GUI.Label(new Rect(20,100,200,30),"<size=12><color=black>Health: " + vT.eHealth.ToString("F1") + "</color></size>");
			//lvl
			GUI.Label(new Rect(20,120,200,30),"<size=12><color=black>Damage: " + vT.eDmg + "</color></size>");
			//lvl
			GUI.Label(new Rect(20,140,200,30),"<size=12><color=black>Speed: " + vT.eSpeed.ToString("F2") + "</color></size>");
			//lvl
			GUI.Label(new Rect(20,160,200,30),"<size=12><color=black>EXP: " + vT.eExp + "</color></size>");
			//lvl
			GUI.Label(new Rect(20,180,200,30),"<size=12><color=black>Money: " + vT.eMoney + "</color></size>");
			//lvl
			GUI.Label(new Rect(20,200,200,30),"<size=12><color=black>Enemy Count: " + vT.vWaveSize + "</color></size>");
		}
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
		if(la.Count >= 1)
		{
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
		}
		else
		{
			GUI.Label(new Rect(10,0,200,30),"<size=12><color=black>No Available Upgrades</color></size>");
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
		
		
		scrollPosition2 = GUI.BeginScrollView(new Rect(10, 130, 280, 220), scrollPosition2, new Rect(0, 0, 260, scrollHeight2));
		if(la2.Count >= 1)
		{
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
		}
		else
		{
			GUI.Label(new Rect(10,0,200,30),"<size=12><color=black>No Available Research</color></size>");
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
		if(GUI.Button(new Rect(10,54,90,20), "<color=black>Sound</color>"))
		{
			vS.PlayClickY();
			vStatOpt = 0;
		}
		if(GUI.Button(new Rect(105,54,90,20), "<color=black>Misc</color>"))
		{
			vS.PlayClickY();
			vStatOpt = 1;
		}
		if(GUI.Button(new Rect(200,54,90,20), "<color=black>Save</color>"))
		{
			vS.PlayClickY();
			vStatOpt = 2;
		}
		// 
		if(vStatOpt == 0)
		{
			if(vS.vMuted == 1.0f)
			{
			vMuteButtonText = "<color=black>Mute Sound</color>";
			}
			else
			{
			vMuteButtonText = "<color=black>Unmute Sound</color>";
			}
			if(GUI.Button(new Rect(10,90,90,20), vMuteButtonText))
			{
				
				Muting();
			}
			
		}
		else if(vStatOpt == 1)
		{
		//lvl
			GUI.Label(new Rect(20,90,200,30),"<size=12><color=black>Notification Lifetime: "+vN.vMaxCool +"ms</color></size>");
			vN.vMaxCool = GUI.HorizontalSlider(new Rect(20, 110, 200, 30), vN.vMaxCool, 1f, 400f);
			GUI.Label(new Rect(20,130,200,30),"<size=12><color=black>Zoom Speed: "+vC.vAmount +"</color></size>");
		vC.vAmount = GUI.HorizontalSlider(new Rect(20, 150, 200, 30), vC.vAmount, 0.1F, 1.0F);
		
		if(vT.vParticles == 1)
			{
			vParticleButtonText = "<color=black>Disable Particles</color>";
			}
			else
			{
			vParticleButtonText = "<color=black>Enable Particles</color>";
			}
			if(GUI.Button(new Rect(20,180,100,20), vParticleButtonText))
			{
				
				Particling();
			}
		
		}
		else if(vStatOpt == 2)
		{
		if(GUI.Button(new Rect(10,90,90,20), "<color=black>Save</color>"))
			{
			vS.PlayClickY();
				vSave.SaveAll();
				vN.AddNotif("Game Saved");
			}
			if(GUI.Button(new Rect(105,90,90,20), "<color=black>Load</color>"))
			{
			vS.PlayClickY();
				vSave.LoadAll();
				vN.AddNotif("Game Loaded");
			}
		}
		GUI.DragWindow();
	}
	void Muting()
	{
		if(vS.vMuted == 1.0f)
		{
			vS.PlayClickY();
			vS.vMuted = 0f;
		}
		else
		{
			vS.vMuted = 1.0f;
			vS.PlayClickY();
		}
	}
	void Particling()
	{
		if(vT.vParticles == 1)
		{
			vS.PlayClickY();
			vT.vParticles = 0;
		}
		else
		{
			vT.vParticles = 1;
			vS.PlayClickY();
		}
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
			int len = vNotificationText.Split('\n').Length;
			int hei = len * 20;
			hei +=10;
			GUI.Box(new Rect(10,Screen.height-hei-10,200,hei), vNotificationText);
			
			
		}
	}
	//called by notification manager
	public void PlayNotif(string inp)
	{
		vNotificationText = inp;

	}
}
