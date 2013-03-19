using UnityEngine;
using System.Collections;

public class Shield : Item
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
			  Color  textureColor = renderer.material.GetColor("_Color");
       		  textureColor.a =  (duration - this.lasttime)/duration;
			  this.gameObject.renderer.material.SetColor("_Color" , textureColor);
			}
		
		}		
	
		void Awake(){
			transform.up  =  transform.position - (GameObject.Find("Sphere").transform.position);
		}		
} 

