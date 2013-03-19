using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C2Component : C2Brick
{

	protected override void HandleRequest (C2Request request){
		
	}
	
	protected override void HandleNotification (C2Notification notification){
		
	}
	
	protected void SendRequest(C2Request request) {
		foreach (C2Port port in this.tops){
			
			if (testConnectionMode){
				if (port.PassRequest(request)){
						Debug.Log(name + " successfully sent request " + request.Type + " to " + port.TopBrick.name);
				}
				else{
					if (testConnectionMode)
						Debug.Log(name + " failed to sent request " + request.Type + " to " + port.TopBrick.name);
				}
			}
			else{
				port.PassRequest(request);
			}
		}
	}
	
	protected void SendNotification(C2Notification notification) {

		foreach (C2Port port in this.bottoms){
			if (testConnectionMode){
				if (port.PassNotification(notification)){
					
					Debug.Log(name + " successfully sent notification " + notification.Type + " to " + port.ButtomBrick.name);
				}
				else{
					Debug.Log(name + " failed to sent notification " + notification.Type + " to " + port.ButtomBrick.name);
				}
			}
			else{
				port.PassNotification(notification);
			}
		}
	}

}

