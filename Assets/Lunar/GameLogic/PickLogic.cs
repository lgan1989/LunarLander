using UnityEngine;
using System;
using System.Collections;

public class PickLogic : C2Component
{

	protected override void HandleNotification (C2Notification notification)
	{
		string type = notification.Type;
		if(type.Equals(C2MessageType.Notification_Item_Collided)) {
			Type msgSrcType = notification.MsgSrcType;		
			NetworkPlayer player = (NetworkPlayer)notification.getData("NetworkPlayer");
			this.NotifyItemPicked(player , msgSrcType.ToString());
			this.RequestUpdatePlayer(player , msgSrcType.ToString());
		}
	}
	
	
	private void NotifyItemPicked(NetworkPlayer player , string item) {
		C2Notification notification = new C2Notification(item + "Picked");
		notification.putData("NetworkPlayer" , player);
		this.SendNotification(notification);
	}
	
	private void RequestUpdatePlayer(NetworkPlayer player , string item){
		
		C2Request request;

		switch (item){
		case "Coin":
			request = new C2Request(C2MessageType.Request_UpdateCoin);
			request.putData("NetworkPlayer" , player);
			this.SendRequest(request);
			break;
		case "EnergyBlock":
			request = new C2Request(C2MessageType.Request_UpdateEnergy);
			request.putData("NetworkPlayer" , player);
			this.SendRequest(request);
			break;
		case "Accelerator":
			request = new C2Request(C2MessageType.Request_UpdateSpeed);
			request.putData("NetworkPlayer" , player);
			this.SendRequest(request);
			break;			
		case "Shield":
			request = new C2Request(C2MessageType.Request_UpdateShield);
			request.putData("NetworkPlayer" , player);
			this.SendRequest(request);
			break;			
		default:
			break;
		}
	}
}

