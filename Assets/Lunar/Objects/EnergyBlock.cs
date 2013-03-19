using UnityEngine;
using System.Collections;

public class EnergyBlock : Item
{
	    public float duration = 30.0f;
		void Update () 
		{
			transform.RotateAroundLocal(transform.position,Time.deltaTime * 5);
			this.Lasttime += Time.deltaTime;
			if (this.Lasttime > duration){
				this.DestroySelf();
			}
			else{
			  Color  textureColor = renderer.material.GetColor("_TintColor");
       		  textureColor.a =  (duration - this.lasttime)/duration;
			  this.gameObject.renderer.material.SetColor("_TintColor" , textureColor);
			}
		
		}
} 
