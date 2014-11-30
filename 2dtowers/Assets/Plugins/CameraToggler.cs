using UnityEngine;
using System.Collections;

public class CameraToggler : MonoBehaviour 
{
	public GameObject v1;
	public GameObject v2;
	
	void Awake () 
	{
		transform.parent = v1.transform;
		transform.position = v1.transform.position;
		transform.rotation = v1.transform.rotation;
	}
	
	void Update () 
	{
		if(Input.GetKeyDown("c"))
		{
			if(transform.parent == v1.transform)
			{
				transform.parent = v2.transform;
		transform.position = v2.transform.position;
		transform.rotation = v2.transform.rotation;
			}
			else
			{
			transform.parent = v1.transform;
		transform.position = v1.transform.position;
		transform.rotation = v1.transform.rotation;
			}
		}
	}
}
