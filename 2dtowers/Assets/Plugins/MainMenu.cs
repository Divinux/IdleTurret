using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public Texture2D vBg;
	public Texture2D vLoad;
	public GUISkin skin;
	public int vLoading = 0;
	public Sound vSou;

	public S vSave;
	
	public string vVersion = "Version 0.1";
public int vExists = 0;

	void Awake () 
	{
		vSou = gameObject.GetComponent<Sound>();
		vExists = PlayerPrefs.GetInt("vExists");
	}
	
	void OnGUI () 
	{
		GUI.skin = skin;
		GUI.DrawTexture(new Rect(0, 0, 900, 600), vBg, ScaleMode.StretchToFill);
		GUI.Label(new Rect(21,561,300,200),"<size=12><color=black>"+vVersion+"</color></size>");
		GUI.Label(new Rect(20,560,300,200),"<size=12><color=white>"+vVersion+"</color></size>");
		if(GUI.Button(new Rect(100,100,200,30), "<color=black>New Game</color>"))
		{
			vSou.PlayClickY();
			vLoading = 1;
			vSave.ResetAll();
			Application.LoadLevel(1);
			
		}
		if(vExists == 1){
		if(GUI.Button(new Rect(100,140,200,30), "<color=black>Continue Game</color>"))
		{
			vSou.PlayClickY();
			vLoading = 1;
			Application.LoadLevel(1);
		}}
		if(vLoading == 1)
		{
		GUI.DrawTexture(new Rect(0, 0, 900, 600), vLoad, ScaleMode.StretchToFill);
		GUI.Label(new Rect(400,300,300,200),"<size=24><color=black>Loading...</color></size>");
		}
	}
}
