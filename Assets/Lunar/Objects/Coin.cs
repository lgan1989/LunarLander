using UnityEngine;
using System.Collections;

	public class Coin : Collidable
	{
	
		private static float duration = 30.0f;
	
	
		void Update () 
		{
			transform.RotateAroundLocal(transform.position,Time.deltaTime * 5);
			this.Lasttime += Time.deltaTime;
			
			if (this.Lasttime > duration){
				this.DestroySelf();
			}
			else{
			  Color textureColor = renderer.material.color;
       		  textureColor.a =  (duration - this.lasttime)/duration;
			  this.gameObject.renderer.material.SetColor("_Color" , textureColor);
			}
		
		}
	
	
	
		void Awake() 
		{
			transform.LookAt(new Vector3(0,0,0));
		}
		
	}    
	


