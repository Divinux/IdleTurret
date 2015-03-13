using UnityEngine;
using System.Collections;

public class CamRotate : MonoBehaviour 
{

	void Awake () 
	{
	
	}
	
	void FixedUpdate () 
	{
	transform.Rotate(Vector3.up * Time.deltaTime);
	}
}
