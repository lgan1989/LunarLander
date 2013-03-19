using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class C2Connector : C2Brick
{
	
	public C2Connector(){
		this.acceptedReqTypes = new List<string>();
		this.acceptedNotiTypes = new List<string>();
	}
	
	protected override void HandleRequest (C2Request request)
	{
		
		foreach(C2Port topPort in this.tops) 
		{
			if (testConnectionMode){
				if (topPort.PassRequest(request)){
					Debug.Log(name + " successfully sent request " + request.Type + " to " + topPort.TopBrick.name);
				}
				else{
					Debug.Log(name + " failed to sent request " + request.Type + " to " + topPort.TopBrick.name);
				}
			}
			else{
				topPort.PassRequest(request);
			}
		}	
	}
	
	protected override void HandleNotification (C2Notification notification)
	{
		
		foreach(C2Port bottomPort in this.bottoms) 
		{
			if (testConnectionMode){
				if (bottomPort.PassNotification(notification)){
					Debug.Log(name + " successfully sent notification " + notification.Type + " to " + bottomPort.ButtomBrick.name);
				}
				else{
					Debug.Log(name + " failed to sent notification " + notification.Type + " to " + bottomPort.ButtomBrick.name);
				}
			}
			else{
				bottomPort.PassNotification(notification);
			}
		}	
	}
	
}

