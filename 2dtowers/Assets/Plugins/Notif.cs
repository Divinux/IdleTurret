using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Notif : MonoBehaviour 
{
	public Ui vU;
	public Sound vS;
	
	public List<string> lQueue = new List<string>();
	
	public float vCool;
	public float vMaxCool = 200;
	
	
	
	void Awake () 
	{
		vU = gameObject.GetComponent<Ui>();
		vS = gameObject.GetComponent<Sound>();
	}
	
	//sends a notification to the ui
	public void SendNotif(string inp)
	{
		vU.PlayNotif(inp);
		
	}
	
	//receives messages
	public void AddNotif(string toadd)
	{
		//check if busy
		if(vCool > 0)
		{
			//add to list
			lQueue.Add(toadd);
		}
		else
		{
			//display
			vU.vNotificationText = toadd;
			//set cooldown
			vCool = vMaxCool;
			vS.PlayNotif();
			
		}
	}
	
	void Update()
	{
		if(vCool > 0)
		{
			vCool--;
			if(vCool <= 0)
			{
				Disable();
			}
		}
		
	}
	
	void Disable()
	{
		vU.vNotificationText = "";
		if(lQueue.Count != 0)
		{
			Invoke("Followup", 1f);
		}
		
	}
	
	void Followup()
	{
		AddNotif(lQueue[0]);
		lQueue.RemoveAt(0);
	}
	
}
