using UnityEngine;
using System.Collections;

public class Ui : MonoBehaviour 
{
	public GUISkin skin;
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
	
	void Awake () 
	{
		WindowUpgrades = new Rect(70, 10, 300, 400);
		WindowResearch = new Rect(70, 10, 300, 400);
		WindowStatus = new Rect(70, 10, 300, 400);
		WindowOptions = new Rect(70, 10, 300, 400);
	}
	
	void OnGUI () 
	{
		GUI.skin = skin;
		if(GUI.Button(new Rect(10,10,50,50), vStatTex))
		{
			bStat = !bStat;
		}
		if(GUI.Button(new Rect(10,130,50,50), vResTex))
		{
			bRes = !bRes;
		}
		if(GUI.Button(new Rect(10,70,50,50), vUpgradesTex))
		{
			bUpgrades = !bUpgrades;
		}
		if(GUI.Button(new Rect(10,190,50,50), vOptTex))
		{
			bOpt = !bOpt;
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
	}
	//////
	void DrawStat()
	{
		WindowStatus = GUI.Window(2, WindowStatus, fWindowStat, "");
	}
	void fWindowStat(int w3)
	{
		GUI.Label(new Rect(20,10,200,50),"<size=24><color=white>Status</color></size>");
		if(GUI.Button(new Rect(250,10,42,42), vCloseTex, "label"))
		{
			bStat = !bStat;
		}
		GUI.DragWindow();
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
			bUpgrades = !bUpgrades;
		}
		GUI.DragWindow();
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
			bRes = !bRes;
		}
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
			bOpt = !bOpt;
		}
		GUI.DragWindow();
	}
}
