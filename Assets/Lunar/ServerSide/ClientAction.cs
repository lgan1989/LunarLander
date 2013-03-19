using UnityEngine;
using System.Collections;

public class ClientAction : C2Component
{
	
	public ClientAction(){
		RegisterAcceptedReq (C2MessageType.Request_ClientAction);
	}
	
	protected override void HandleRequest (C2Request request)
	{
		
		if (request.Type.Equals(C2MessageType.Request_ClientAction)){
			
			C2Notification notification = new C2Notification(C2MessageType.Notification_Input);
	        notification.putData("VInput", (float)request.getData("VInput"));
	        notification.putData("HInput", (float)request.getData("HInput"));
			notification.putData("NetworkPlayer" , (NetworkPlayer)request.getData("NetworkPlayer"));

	        this.SendNotification(notification);
		}
	}
	

}

