using UnityEngine;
using System.Collections;

	public class Obstacle : Collidable
	{
		private static float duration = 20.0f;
		void Update () 
		{
			transform.RotateAroundLocal(transform.position,Time.deltaTime * 5);
			this.Lasttime += Time.deltaTime;
			if (this.Lasttime > duration){
				this.DestroySelf();
			}	
		}

		void Awake() 
		{
			transform.LookAt(new Vector3(0,0,0));
		}
		
	}  