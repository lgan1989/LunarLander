using UnityEngine;
using System.Collections;

	public class Bullet : Collidable
	{
	
		void Update () 
		{
			transform.RotateAroundLocal(transform.position,Time.deltaTime * 5);
		}
	
		void Awake() 
		{
			transform.LookAt(new Vector3(0,0,0));
		}
		
	}  